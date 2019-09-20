using DDDTW.CoffeeShop.CommonLib.Interfaces;
using DDDTW.CoffeeShop.Orders.Application.Products.Applications;
using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Messages;
using DDDTW.CoffeeShop.Orders.Application.Products.DataContracts.Results;
using DDDTW.CoffeeShop.Orders.Application.Products.DomainService;
using DDDTW.CoffeeShop.Orders.Domain.Products.Interfaces;
using DDDTW.CoffeeShop.Orders.Domain.Products.Models;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDTW.CoffeeShop.Orders.UnitTest.Products
{
    [TestFixture()]
    public class ServiceTests
    {
        private readonly Product product;

        private readonly ITranslator<ProductId, string> idTranslator;
        private readonly IProductRepository mockRepository;

        public ServiceTests()
        {
            this.product = new Product(new ProductId(), "name", "desc",
                new ProductPrice(10), ProductStatus.Active, DateTimeOffset.Now);

            this.idTranslator = new IdTranslator();

            this.mockRepository = Substitute.For<IProductRepository>();
            this.mockRepository.GenerateProductId().Returns(new ProductId());
        }

        [Test]
        public async Task GetProducts()
        {
            var msg = new GetProductMsg(1, 1);
            var expected = new List<ProductRst>() { new ProductRst(this.product) };
            this.mockRepository.Get(Arg.Any<int>(), Arg.Any<int>())
                .Returns(new List<Product>() { this.product });

            var svc = new GetProductsSvc(this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().BeEquivalentTo(expected.AsEnumerable());
        }

        [Test]
        public async Task GetProductBy()
        {
            var msg = new GetProductByMsg($"prd-{DateTimeOffset.Now:yyyyMMdd}-0");
            var expected = new ProductRst(this.product);
            this.mockRepository.GetBy(Arg.Any<ProductId>())
                .Returns(this.product);

            var svc = new GetProductBySvc(this.idTranslator, this.mockRepository);
            var actual = await svc.Handle(msg, new CancellationToken());

            actual.Should().Be(expected);
        }
    }
}