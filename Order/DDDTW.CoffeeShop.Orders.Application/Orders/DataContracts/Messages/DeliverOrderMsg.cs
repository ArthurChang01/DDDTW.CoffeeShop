﻿using MediatR;

namespace DDDTW.CoffeeShop.Orders.Application.Orders.DataContracts.Messages
{
    public class DeliverOrderMsg : IRequest<Unit>
    {
        public DeliverOrderMsg(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}