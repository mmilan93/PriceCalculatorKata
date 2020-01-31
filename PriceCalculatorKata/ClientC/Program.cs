using Application.UseCases;
using System;

namespace ClientC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int option = 0;
            do
            {
                Console.WriteLine("Insert opton:");
                if (!int.TryParse(Console.ReadLine(), out option))
                    break;

                switch (option)
                {
                    case 1:
                    case 2:
                        CalculateWithTaxAndDiscount();
                        break;
                    case 3:
                        Report();
                        break;
                    default:
                        Console.WriteLine("option is not existing!");
                        break;
                }
            } while (option != -1);
        }

        static void CalculateWithTaxAndDiscount()
        {
            Console.WriteLine("name:");
            var name = Console.ReadLine();
            Console.WriteLine("upc:");
            var upc = int.Parse(Console.ReadLine());
            Console.WriteLine("basePrice:");
            var basePrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("tax:");
            var tax = int.Parse(Console.ReadLine());
            Console.WriteLine("discount:");
            var discount = int.Parse(Console.ReadLine());

            var calculatedProductDto = new CalculateUseCase().Calculate(name, upc, basePrice, tax, discount);
            Console.WriteLine($"Tax amount = ${calculatedProductDto.TaxAmount}; Discount amount = ${calculatedProductDto.DiscountAmount}");
            Console.WriteLine($"Price before = ${calculatedProductDto.BasePrice}, price after = ${calculatedProductDto.CalculatedPrice}");
        }

        static void Report()
        {
            Console.WriteLine("name:");
            var name = Console.ReadLine();
            Console.WriteLine("upc:");
            var upc = int.Parse(Console.ReadLine());
            Console.WriteLine("basePrice:");
            var basePrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("tax:");
            var tax = int.Parse(Console.ReadLine());

            string discountExistsAnswer = "";
            int? discount = null;
            do
            {
                Console.WriteLine("discount exists (y/n):");
                discountExistsAnswer = Console.ReadLine();
                if (discountExistsAnswer == "y")
                    discount = int.Parse(Console.ReadLine());
                else if (discountExistsAnswer == "n")
                    discount = null;
            } while (discountExistsAnswer != "y" && discountExistsAnswer != "n");

            var calculatedProductDto = new CalculateUseCase().Calculate(name, upc, basePrice, tax, discount);
            var discountLabel = calculatedProductDto.DiscountAmount.HasValue
                                    ? $"[applied discount: ${calculatedProductDto.DiscountAmount.Value}]"
                                    : "";
            Console.WriteLine($"New price: ${calculatedProductDto.CalculatedPrice} {discountLabel}");
        }
    }
}
