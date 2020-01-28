namespace Core

module PriceCalculations =

    type Tax = Tax of int
    type Discount = Discount of int
    
    type PriceCalculationDto = {
        upc: Product.UPC
        basePrice: ProductPrice
        taxAmount: ProductPrice
        discountAmount: ProductPrice
        calculatedPrice: ProductPrice
    }

    let initializePriceCalculationDto upc basePrice =
        { upc = upc;
          basePrice = basePrice
          taxAmount = ProductPrice.create 0M 
          discountAmount = ProductPrice.create 0M 
          calculatedPrice = ProductPrice.create 0M }

    let applyPercentage percentage productPrice =
        ProductPrice.create (ProductPrice.value productPrice * (decimal percentage)/100M)

    let calculateTax (Tax tax) dto =
        { dto with taxAmount = applyPercentage tax dto.basePrice }
        
    let calculateDiscount (Discount discount) dto =
        { dto with discountAmount = applyPercentage discount dto.basePrice }

    let calculateFinalPrice dto =
        { dto with calculatedPrice = ProductPrice.create (ProductPrice.value dto.basePrice
                                                          + ProductPrice.value dto.taxAmount
                                                          - ProductPrice.value dto.discountAmount) }

    let calculatePrice tax discount (product: Product.ProductType) =
        initializePriceCalculationDto product.upc product.basePrice
            |> calculateTax tax
            |> calculateDiscount discount
            |> calculateFinalPrice
