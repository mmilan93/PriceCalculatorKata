namespace Core

module PriceCalculations =

    type Tax = Tax of decimal

    let calculatePrice (Tax tax) basePrice =
        ProductPrice.create (ProductPrice.value basePrice * (1M + tax/100M))