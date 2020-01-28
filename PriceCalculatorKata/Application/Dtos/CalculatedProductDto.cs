using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class CalculatedProductDto
    {
        public CalculatedProductDto(int upc,
                                    string name,
                                    decimal basePrice,
                                    decimal taxAmount,
                                    decimal discountAmount,
                                    decimal calculatedPrice)
        {
            Upc = upc;
            Name = name;
            BasePrice = basePrice;
            TaxAmount = taxAmount;
            DiscountAmount = discountAmount;
            CalculatedPrice = calculatedPrice;
        }

        public int Upc { get; }
        public string Name { get; }
        public decimal BasePrice { get; }
        public decimal TaxAmount { get; }
        public decimal DiscountAmount { get; }
        public decimal CalculatedPrice { get; }
    }
}
