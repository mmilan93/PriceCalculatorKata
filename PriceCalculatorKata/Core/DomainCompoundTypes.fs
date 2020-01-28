namespace Core

module Product =

    type Name = Name of string
    type UPC = UPC of int

    type ProductType = {
        name: Name
        upc: UPC
        basePrice: ProductPrice
    }