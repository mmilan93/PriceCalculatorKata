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
                                    decimal? universaldiscountAmount,
                                    decimal? upcDiscountAmount,
                                    decimal? totalDiscountAmount,
                                    decimal calculatedPrice)
        {
            Upc = upc;
            Name = name;
            BasePrice = basePrice;
            TaxAmount = taxAmount;
            UniversalDiscountAmount = universaldiscountAmount;
            UpcDiscountAmount = upcDiscountAmount;
            TotalDiscountAmount = totalDiscountAmount;
            CalculatedPrice = calculatedPrice;
        }

        public int Upc { get; }
        public string Name { get; }
        public decimal BasePrice { get; }
        public decimal TaxAmount { get; }
        public decimal? UniversalDiscountAmount { get; }
        public decimal? UpcDiscountAmount { get; }
        public decimal? TotalDiscountAmount { get; }
        public decimal CalculatedPrice { get; }
    }
}
