namespace Core

open System
    
type ProductPrice = private ProductPrice of decimal

module ProductPrice =
    let value (ProductPrice price) = price 

    let create (price: decimal) = 
        ProductPrice (Math.Round(price, 2))