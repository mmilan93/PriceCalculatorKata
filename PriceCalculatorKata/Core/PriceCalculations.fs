namespace Core

module PriceCalculations =

    type Tax = Tax of int
    type Discount = Discount of int
    
    type PriceCalculationDto = {
        upc: Product.UPC
        basePrice: ProductPrice
        taxAmount: ProductPrice
        discountAmount: ProductPrice option
        calculatedPrice: ProductPrice
    }

    let initializePriceCalculationDto upc basePrice =
        { upc = upc;
          basePrice = basePrice
          taxAmount = ProductPrice.create 0M 
          discountAmount = None
          calculatedPrice = ProductPrice.create 0M }

    let applyPercentage percentage productPrice =
        ProductPrice.create (ProductPrice.value productPrice * (decimal percentage)/100M)

    let calculateTax (Tax tax) dto =
        { dto with taxAmount = applyPercentage tax dto.basePrice }
        
    let calculateDiscount discount dto =
        { dto with discountAmount = match discount with
                                    | Some (Discount disc) -> Some (applyPercentage disc dto.basePrice)
                                    | None -> None}

    let calculateFinalPrice dto =
        let appliedDiscountAmount = match dto.discountAmount with
                                    | Some (ProductPrice amount) -> amount
                                    | None -> 0M
        { dto with calculatedPrice = ProductPrice.create (ProductPrice.value dto.basePrice
                                                          + ProductPrice.value dto.taxAmount
                                                          - appliedDiscountAmount) }

    let calculatePrice tax discount (product: Product.ProductType) =
        initializePriceCalculationDto product.upc product.basePrice
            |> calculateTax tax
            |> calculateDiscount discount
            |> calculateFinalPrice
