namespace Core

module Product =

    open Common

    type Name = Name of string
    type UPC = UPC of int

    type Product = {
        name: Name
        upc: UPC
        basePrice: ProductPrice
    }