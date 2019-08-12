using System;
using System.Collections.Generic;
using System.Text;
using DDDTW.CoffeeShop.CommonLib.Interfaces;
using MediatR;

namespace DDDTW.CoffeeShop.Infrastructures
{
    public class NotificationBase<T> : INotification
        where T : IDomainEvent
    {
        public NotificationBase(T @event)
        {
            this.PayloadEvent = @event;
        }

        public T PayloadEvent { get; private set; }
    }
}