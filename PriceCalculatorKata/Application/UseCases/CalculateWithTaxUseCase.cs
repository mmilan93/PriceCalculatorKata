using Application.Dtos;
using Core;

namespace Application.UseCases
{
    public class CalculateWithTaxUseCase
    {
        public CalculatedProductDto Calculate(string name, int upc, decimal productPrice, int tax, int discount)
        {
            Say.hello("adasd");
            var pr = new Product.ProductType(
                                        Product.Name.NewName(name),
                                        Product.UPC.NewUPC(upc),
                                        ProductPriceModule.create(productPrice));
            var priceCalculationDto = PriceCalculations.calculatePrice(
                                                        PriceCalculations.Tax.NewTax(tax),
                                                        PriceCalculations.Discount.NewDiscount(discount),
                                                        pr);
            return new CalculatedProductDto(priceCalculationDto.upc.Item,
                                            name,
                                            ProductPriceModule.value(priceCalculationDto.basePrice),
                                            ProductPriceModule.value(priceCalculationDto.taxAmount),
                                            ProductPriceModule.value(priceCalculationDto.discountAmount),
                                            ProductPriceModule.value(priceCalculationDto.calculatedPrice));
        }
    }
}
