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
                        Req01();
                        break;
                    default:
                        Console.WriteLine("option is not existing!");
                        break;
                }
            } while (option != -1);
        }

        static void Req01()
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

            var calculatedProductDto = new CalculateWithTaxUseCase().Calculate(name, upc, basePrice, tax, discount);
            Console.WriteLine($"Tax amount = ${calculatedProductDto.TaxAmount}; Discount amount = ${calculatedProductDto.DiscountAmount}");
            Console.WriteLine($"Price before = ${calculatedProductDto.BasePrice}, price after = ${calculatedProductDto.CalculatedPrice}");
        }
    }
}
