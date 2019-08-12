using System;
using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.Interfaces;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class Policy<T> : IPolicy<T>
        where T : IAggregateRoot
    {
        protected readonly T aggregateRoot;

        public Policy(T aggregateRoot)
        {
            this.aggregateRoot = aggregateRoot;
        }

        protected readonly List<Exception> exceptions = new List<Exception>();

        public abstract bool IsValid();

        public Exception GetWrapperException => new AggregateException(exceptions);
    }
}