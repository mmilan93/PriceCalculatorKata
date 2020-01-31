using Application.Dtos;
using Core;
using Microsoft.FSharp.Core;

namespace Application.UseCases
{
    public class CalculateUseCase
    {
        public CalculatedProductDto Calculate(string name, int upc, decimal productPrice, int tax, int? discount)
        {
            var discountOption = discount.HasValue
                                                    ? FSharpOption<PriceCalculations.Discount>.Some(PriceCalculations.Discount.NewDiscount(discount.Value))
                                                    : FSharpOption<PriceCalculations.Discount>.None;

            Say.hello("from Say module");
            var pr = new Product.ProductType(
                                        Product.Name.NewName(name),
                                        Product.UPC.NewUPC(upc),
                                        ProductPriceModule.create(productPrice));
            var priceCalculationDto = PriceCalculations.calculatePrice(
                                                        PriceCalculations.Tax.NewTax(tax),
                                                        discountOption,
                                                        pr);

            return new CalculatedProductDto(priceCalculationDto.upc.Item,
                                            name,
                                            ProductPriceModule.value(priceCalculationDto.basePrice),
                                            ProductPriceModule.value(priceCalculationDto.taxAmount),
                                            FSharpOption<Core.ProductPrice>.get_IsSome(priceCalculationDto.discountAmount)
                                                ? ProductPriceModule.value(priceCalculationDto.discountAmount.Value) as decimal?
                                                : null,
                                            ProductPriceModule.value(priceCalculationDto.calculatedPrice));
        }
    }
}
