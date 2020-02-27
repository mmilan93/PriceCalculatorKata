using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class UpcDiscountDto
    {
        public UpcDiscountDto(int upc, int discount)
        {
            Upc = upc;
            Discount = discount;
        }

        public int Upc { get; }
        public int Discount { get; }
    }
}
