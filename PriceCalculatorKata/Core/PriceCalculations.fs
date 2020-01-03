namespace Core

open Common

module PriceCalculations =

    type Tax = Tax of decimal

    let calculatePrice (Tax tax) (ProductPrice basePrice) =
        createProductPrice (basePrice * (1M + tax/100M))