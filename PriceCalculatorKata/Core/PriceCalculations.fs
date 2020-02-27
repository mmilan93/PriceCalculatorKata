namespace Core

module PriceCalculations =

    type Tax = Tax of int
    type Discount = Discount of int
    type UpcDiscount = {
        discount: Discount
        upc: ProductUpc
    }
    
    type PriceCalculationDto = {
        upc: ProductUpc
        basePrice: ProductPrice
        taxAmount: ProductPrice
        universalDiscountAmount: ProductPrice option
        upcDiscountAmount: ProductPrice option
        totalDiscountAmount: ProductPrice option
        calculatedPrice: ProductPrice
    }

    let initializePriceCalculationDto upc basePrice =
        { upc = upc;
          basePrice = basePrice
          taxAmount = ProductPrice.create 0M 
          universalDiscountAmount = None
          upcDiscountAmount = None
          totalDiscountAmount = None
          calculatedPrice = ProductPrice.create 0M }

    let applyPercentage percentage productPrice =
        ProductPrice.create (ProductPrice.value productPrice * (decimal percentage)/100M)

    let calculateTax (Tax tax) dto =
        { dto with taxAmount = applyPercentage tax dto.basePrice }
        
    let calculateUniversalDiscount discount dto =
        { dto with universalDiscountAmount = match discount with
                                             | Some (Discount disc) -> Some (applyPercentage disc dto.basePrice)
                                             | None -> None}

    
    let calculateUpcDiscount (upcDiscount: UpcDiscount option) dto =
        { dto with upcDiscountAmount = match upcDiscount with
                                       | Some disc -> 
                                            let (Discount upcDiscountValue) = disc.discount
                                            if disc.upc = dto.upc 
                                                then Some (applyPercentage upcDiscountValue dto.basePrice) 
                                                else None
                                       | None -> None}

    let calculateTotalDiscount dto =
        { dto with totalDiscountAmount = match dto.universalDiscountAmount, dto.upcDiscountAmount with
                                         | Some (ProductPrice universalDiscAmount), Some (ProductPrice upcDiscAmount) 
                                            -> Some (ProductPrice (universalDiscAmount + upcDiscAmount))
                                         | Some universalDiscAmount, None 
                                            -> Some universalDiscAmount
                                         | None, Some upcDiscAmount
                                            -> Some upcDiscAmount
                                         | None, None
                                            -> None}

    let calculateFinalPrice dto =
        let totalDiscountAmount = match dto.totalDiscountAmount with
                                    | Some (ProductPrice amount) -> amount
                                    | None -> 0M
        { dto with calculatedPrice = ProductPrice.create (ProductPrice.value dto.basePrice
                                                          + ProductPrice.value dto.taxAmount
                                                          - totalDiscountAmount) }

    let calculatePrice tax universalDiscount upcDiscount (product: Product) =
        initializePriceCalculationDto product.upc product.basePrice
            |> calculateTax tax
            |> calculateUniversalDiscount universalDiscount
            |> calculateUpcDiscount upcDiscount
            |> calculateTotalDiscount
            |> calculateFinalPrice
