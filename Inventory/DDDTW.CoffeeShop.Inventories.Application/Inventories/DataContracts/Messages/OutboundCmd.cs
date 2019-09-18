﻿using DDDTW.CoffeeShop.Inventories.Domain.Inventories.Models;
using MediatR;

namespace DDDTW.CoffeeShop.Inventories.Application.Inventories.DataContracts.Messages
{
    public class OutBoundMsg : IRequest<Inventory>
    {
        public string Id { get; set; }

        public int Amount { get; set; }
    }
}