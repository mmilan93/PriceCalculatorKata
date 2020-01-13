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
                    continue;

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
            var tax = decimal.Parse(Console.ReadLine());

            var priceWithTax = new CalculateWithTaxUseCase().Calculate(name, upc, basePrice, tax);
            Console.WriteLine($"Product price reported as ${basePrice} before tax and ${priceWithTax} after {tax}% tax");
        }
    }
}
