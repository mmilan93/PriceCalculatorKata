namespace Core

open System

module Common =

    //[<Measure>]
    //type usd

    type ProductPrice = internal ProductPrice of decimal//<usd>

    let createProductPrice (price: decimal) = 
        ProductPrice (Math.Round(price, 2))