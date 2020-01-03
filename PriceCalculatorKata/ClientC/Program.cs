using Application.UseCases;
using System;

namespace ClientC
{
    class Program
    {
        static void Main(string[] args)
        {
            Req01();
            Console.WriteLine("Hello World!");
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
            var tax = decimal.Parse(Console.ReadLine());

            var priceWithTax = new CalculateWithTaxUseCase().Calculate(name, upc, basePrice, tax);
            Console.WriteLine($"Product price reported as ${basePrice} before tax and ${priceWithTax} after {tax}% tax");
        }
    }
}
