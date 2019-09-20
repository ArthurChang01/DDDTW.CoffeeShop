using DDDTW.CoffeeShop.CommonLib.BaseClasses;
using DDDTW.CoffeeShop.Orders.Domain.Products.Commands;
using DDDTW.CoffeeShop.Orders.Domain.Products.DomainEvents;
using DDDTW.CoffeeShop.Orders.Domain.Products.Exceptions;
using DDDTW.CoffeeShop.Orders.Domain.Products.Policies;
using DDDTW.CoffeeShop.Orders.Domain.Products.Specifications;
using System;

namespace DDDTW.CoffeeShop.Orders.Domain.Products.Models
{
    public class Product : AggregateRoot<ProductId>
    {
        #region Constructors

        public Product()
        {
            this.Id = new ProductId();
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.Price = new ProductPrice();
            this.Status = ProductStatus.Inactive;
            this.CreatedDate = DateTimeOffset.Now;
        }

        public Product(ProductId id, string name, string desc, ProductPrice price, ProductStatus status,
            DateTimeOffset createdDate, DateTimeOffset? modifiedDate = null)
        {
            this.Id = id;
            this.Name = name;
            this.Description = desc;
            this.Price = price;
            this.Status = status;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;

            ProductPolicy.Verify(this);

            this.ApplyEvent(new ProductCreated(this.Id, this.Name, this.Description, this.Price.Price, this.Price.Discount,
                this.CreatedDate, this.ModifiedDate));
        }

        #endregion Constructors

        #region Properties

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ProductPrice Price { get; private set; }

        public decimal FinalPrice => this.Price.FinalPrice;

        public ProductStatus Status { get; private set; }

        public DateTimeOffset CreatedDate { get; private set; }

        public DateTimeOffset? ModifiedDate { get; private set; }

        #endregion Properties

        #region Public Methods

        public void ChangeName(ChangeName cmd)
        {
            if (new NameSpec(cmd.Name).IsSatisfy() == false)
                throw new NameIsEmptyException();

            this.Name = cmd.Name;
            this.ModifiedDate = DateTimeOffset.Now;

            this.ApplyEvent(new ProductNameChanged(this.Id, this.Name, this.ModifiedDate.Value));
        }

        public void ChangeDescription(ChangeDescription cmd)
        {
            if (new DescriptionSpec(cmd.Description).IsSatisfy() == false)
                throw new DescriptionIsEmptyException();

            this.Description = cmd.Description;
            this.ModifiedDate = DateTimeOffset.Now;

            this.ApplyEvent(new ProductDescriptionChanged(this.Id, this.Description, this.ModifiedDate.Value));
        }

        public void ChangePrice(ChangePrice cmd)
        {
            this.Price = new ProductPrice(cmd.Price, cmd.Discount);
            this.ModifiedDate = DateTimeOffset.Now;

            this.ApplyEvent(new ProductPriceChanged(this.Id, this.Price.Price, this.Price.Discount, this.ModifiedDate.Value));
        }

        public void Enable()
        {
            this.Status = ProductStatus.Active;
            this.ModifiedDate = DateTimeOffset.Now;

            this.ApplyEvent(new ProductEnabled(this.Id, this.ModifiedDate.Value));
        }

        public void Disable()
        {
            this.Status = ProductStatus.Inactive;
            this.ModifiedDate = DateTimeOffset.Now;

            this.ApplyEvent(new ProductDisabled(this.Id, this.ModifiedDate.Value));
        }

        #endregion Public Methods
    }
}