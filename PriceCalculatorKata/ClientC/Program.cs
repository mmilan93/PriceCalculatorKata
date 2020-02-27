using Application.Dtos;
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
                Console.WriteLine("########################################################");
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
                    case 4:
                        ReportWithSelectiveDiscount();
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

            var calculatedProductDto = new CalculateUseCase().Calculate(name, upc, basePrice, tax, discount, null);
            Console.WriteLine($"Tax amount = ${calculatedProductDto.TaxAmount}; Discount amount = ${calculatedProductDto.UniversalDiscountAmount}");
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

            int? discount = null;
            PrintYesNoQuestion("discount exists?",
                               () => {
                                   discount = int.Parse(Console.ReadLine());
                               },
                               () => {
                                   discount = null;
                               });

            var calculatedProductDto = new CalculateUseCase().Calculate(name, upc, basePrice, tax, discount, null);
            var discountLabel = calculatedProductDto.UniversalDiscountAmount.HasValue
                                    ? $"[applied discount: ${calculatedProductDto.UniversalDiscountAmount.Value}]"
                                    : "";
            Console.WriteLine($"New price: ${calculatedProductDto.CalculatedPrice} {discountLabel}");
        }

        static void ReportWithSelectiveDiscount()
        {
            Console.WriteLine("name:");
            var name = Console.ReadLine();
            Console.WriteLine("upc:");
            var upc = int.Parse(Console.ReadLine());
            Console.WriteLine("basePrice:");
            var basePrice = decimal.Parse(Console.ReadLine());
            Console.WriteLine("tax:");
            var tax = int.Parse(Console.ReadLine());

            int? universalDiscount = null;
            PrintYesNoQuestion("universalDiscount exists?",
                               () => {
                                   Console.WriteLine("discount:");
                                   universalDiscount = int.Parse(Console.ReadLine());
                               },
                               () => {
                                   universalDiscount = null;
                               });

            UpcDiscountDto upcDiscount = null;
            PrintYesNoQuestion("upcDiscount exists?",
                               () => {
                                   Console.WriteLine("discount:");
                                   var innerDiscount = int.Parse(Console.ReadLine());
                                   Console.WriteLine("upc:");
                                   var innerUpc = int.Parse(Console.ReadLine());
                                   upcDiscount = new UpcDiscountDto(innerUpc, innerDiscount);
                               },
                               () => {
                                   universalDiscount = null;
                               });

            var calculatedProductDto = new CalculateUseCase().Calculate(name, upc, basePrice, tax, universalDiscount, upcDiscount);
            var universalDiscountLabel = calculatedProductDto.UniversalDiscountAmount.HasValue
                                            ? $"; universal discount: ${calculatedProductDto.UniversalDiscountAmount.Value}"
                                            : "";
            var upcDiscountLabel = calculatedProductDto.UpcDiscountAmount.HasValue
                                            ? $"; upc discount: ${calculatedProductDto.UpcDiscountAmount.Value}"
                                            : "";
            var totalDiscountLabel = calculatedProductDto.TotalDiscountAmount.HasValue
                                            ? $"; total discount: ${calculatedProductDto.TotalDiscountAmount.Value}"
                                            : "";

            Console.WriteLine($"Tax amount = ${calculatedProductDto.TaxAmount}{universalDiscountLabel}{upcDiscountLabel}");
            Console.WriteLine($"New price: ${calculatedProductDto.CalculatedPrice}{totalDiscountLabel}");
        }

        static void PrintYesNoQuestion(string question, Action yesAction, Action noAction)
        {
            string answer = "";
            do
            {
                Console.WriteLine($"{question} (y/n):");
                answer = Console.ReadLine();
                if (answer == "y")
                    yesAction();
                else if (answer == "n")
                    noAction();
            } while (answer != "y" && answer != "n");
        }
    }
}
