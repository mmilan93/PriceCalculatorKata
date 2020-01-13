using Core;

namespace Application.UseCases
{
    public class CalculateWithTaxUseCase
    {
        public decimal Calculate(string name, int upc, decimal productPrice, decimal tax)
        {
            Say.hello("adasd");
            var pr = new Product.Product(
                                        Product.Name.NewName(name),
                                        Product.UPC.NewUPC(upc),
                                        ProductPriceModule.create(productPrice));
            var calculatedPrice = PriceCalculations.calculatePrice(
                                                        PriceCalculations.Tax.NewTax(tax),
                                                        pr.basePrice);
            return ProductPriceModule.value(calculatedPrice);
        }
    }
}
