namespace Core

open System
    
type ProductPrice = private ProductPrice of decimal
type ProductName = ProductName of string
type ProductUpc = ProductUpc of int

module ProductPrice =
    let value (ProductPrice price) = price 

    let create (price: decimal) = 
        ProductPrice (Math.Round(price, 2))