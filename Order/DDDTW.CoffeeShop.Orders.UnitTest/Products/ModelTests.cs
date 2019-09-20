using DDDTW.CoffeeShop.Orders.Domain.Products.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace DDDTW.CoffeeShop.Orders.UnitTest.Products
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture()]
    public class ModelTests
    {
        [Test]
        public void CreateProduct()
        {
            Product product = new Product();

            product.Id.ToString().Should().Be($"prd-{DateTimeOffset.Now:yyyyMMdd}-0");
            product.Name.Should().BeEmpty();
            product.Description.Should().BeEmpty();
            product.Price.Should().Be(new ProductPrice());
            product.FinalPrice.Should().Be(new ProductPrice().FinalPrice);
            product.Status.Should().Be(ProductStatus.Inactive);
            product.CreatedDate.ToString("yyyyMMdd").Should().Be(DateTimeOffset.Now.ToString("yyyyMMdd"));
        }

        [Test]
        public void CreateProductWithParameter()
        {
            var product = this.GetProduct(2, "prd", "desc", 10, 1, ProductStatus.Active);

            product.Id.ToString().Should().Be($"prd-{DateTimeOffset.Now:yyyyMMdd}-2");
            product.Name.Should().Be("prd");
            product.Description.Should().Be("desc");
            product.Price.Price.Should().Be(10);
            product.Price.Discount.Should().Be(1);
            product.FinalPrice.Should().Be(new ProductPrice(10).FinalPrice);
            product.Status.Should().Be(ProductStatus.Active);
            product.CreatedDate.ToString("yyyyMMdd").Should().Be(DateTimeOffset.Now.ToString("yyyyMMdd"));
        }

        [Test]
        public void ChangeName()
        {
            var product = this.GetProduct(name: "prd1");
            var newName = "prd3";
            var cmd = new ChangeName(newName);

            product.ChangeName(cmd);

            product.Name.Should().Be(newName);
        }

        [Test]
        public void ChangeDescription()
        {
            var product = this.GetProduct(desc: "desc1");
            var newDesc = "desc6";
            var cmd = new ChangeDescription(newDesc);

            product.ChangeDescription(cmd);

            product.Description.Should().Be(newDesc);
            product.ModifiedDate?.ToString("yyyyMMdd").Should().Be(DateTimeOffset.Now.ToString("yyyyMMdd"));
        }

        [TestCase(11, 0.38, 4.18)]
        [TestCase(320, 0.8, 256)]
        public void ChangePrice(decimal price, decimal discount, decimal finalPrice)
        {
            var product = this.GetProduct(price: price, discount: discount);
            var cmd = new ChangePrice(price, discount);

            product.ChangePrice(cmd);

            product.Price.Price.Should().Be(price);
            product.Price.Discount.Should().Be(discount);
            product.FinalPrice.Should().Be(finalPrice);
            product.ModifiedDate?.ToString("yyyyMMdd").Should().Be(DateTimeOffset.Now.ToString("yyyyMMdd"));
        }

        [Test]
        public void Enable()
        {
            var product = this.GetProduct(status: ProductStatus.Inactive);

            product.Enable();

            product.Status.Should().Be(ProductStatus.Active);
            product.ModifiedDate?.ToString("yyyyMMdd").Should().Be(DateTimeOffset.Now.ToString("yyyyMMdd"));
        }

        [Test]
        public void Disable()
        {
            var product = this.GetProduct(status: ProductStatus.Active);

            product.Disable();

            product.Status.Should().Be(ProductStatus.Inactive);
            product.ModifiedDate?.ToString("yyyyMMdd").Should().Be(DateTimeOffset.Now.ToString("yyyyMMdd"));
        }

        private Product GetProduct(long seqNo = 0, string name = "name", string desc = "desc",
            decimal price = 1, decimal discount = 1,
            ProductStatus status = ProductStatus.Active,
            DateTimeOffset? createdDate = null, DateTimeOffset? modifiedDate = null)
        {
            var id = new ProductId(seqNo);
            var prdPrice = new ProductPrice(price, discount);

            return new Product(id, name, desc, prdPrice, status,
                createdDate ?? DateTimeOffset.Now, modifiedDate);
        }
    }
}