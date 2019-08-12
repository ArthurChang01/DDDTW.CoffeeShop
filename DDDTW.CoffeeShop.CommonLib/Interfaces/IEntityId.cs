using System;

namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface IEntityId
    {
        string Abbr { get; }

        long SeqNo { get; }

        DateTimeOffset OccuredDate { get; }
    }
}