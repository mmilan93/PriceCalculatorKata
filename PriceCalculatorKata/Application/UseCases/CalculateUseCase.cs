using Application.Dtos;
using Core;
using Microsoft.FSharp.Core;

namespace Application.UseCases
{
    public class CalculateUseCase
    {
        public CalculatedProductDto Calculate(string name, int upc, decimal productPrice, int tax, int? universalDiscount, UpcDiscountDto upcDiscount)
        {
            var universalDiscountOption = universalDiscount.HasValue
                                                    ? FSharpOption<PriceCalculations.Discount>.Some(PriceCalculations.Discount.NewDiscount(universalDiscount.Value))
                                                    : FSharpOption<PriceCalculations.Discount>.None;
            var upcDiscountOption = upcDiscount != null
                                                    ? FSharpOption<PriceCalculations.UpcDiscount>.Some(new PriceCalculations.UpcDiscount(
                                                                                                                                PriceCalculations.Discount.NewDiscount(upcDiscount.Discount),
                                                                                                                                ProductUpc.NewProductUpc(upcDiscount.Upc)))
                                                    : FSharpOption<PriceCalculations.UpcDiscount>.None;

            Say.hello("from Say module");
            var pr = new Product(
                            ProductName.NewProductName(name),
                            ProductUpc.NewProductUpc(upc),
                            ProductPriceModule.create(productPrice));
            var priceCalculationDto = PriceCalculations.calculatePrice(
                                                        PriceCalculations.Tax.NewTax(tax),
                                                        universalDiscountOption,
                                                        upcDiscountOption,
                                                        pr);

            return new CalculatedProductDto(priceCalculationDto.upc.Item,
                                            name,
                                            ProductPriceModule.value(priceCalculationDto.basePrice),
                                            ProductPriceModule.value(priceCalculationDto.taxAmount),
                                            FSharpOption<ProductPrice>.get_IsSome(priceCalculationDto.universalDiscountAmount)
                                                ? ProductPriceModule.value(priceCalculationDto.universalDiscountAmount.Value) as decimal?
                                                : null,
                                            FSharpOption<ProductPrice>.get_IsSome(priceCalculationDto.upcDiscountAmount)
                                                ? ProductPriceModule.value(priceCalculationDto.upcDiscountAmount.Value) as decimal?
                                                : null,
                                            FSharpOption<ProductPrice>.get_IsSome(priceCalculationDto.totalDiscountAmount)
                                                ? ProductPriceModule.value(priceCalculationDto.totalDiscountAmount.Value) as decimal?
                                                : null,
                                            ProductPriceModule.value(priceCalculationDto.calculatedPrice));
        }
    }
}
