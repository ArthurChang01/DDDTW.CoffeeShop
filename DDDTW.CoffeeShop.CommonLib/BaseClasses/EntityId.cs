using System;
using System.Collections.Generic;
using DDDTW.CoffeeShop.CommonLib.Interfaces;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public abstract class EntityId : ValueObject<EntityId>, IEntityId
    {
        protected EntityId()
        {
            this.SeqNo = 0;
            this.OccuredDate = DateTimeOffset.Now;
        }

        protected EntityId(long seqNo, DateTimeOffset occuredDate)
        {
            if (seqNo < 0)
                throw new ArgumentException("SeqNo can not be negative digital");

            this.SeqNo = seqNo;
            this.OccuredDate = occuredDate;
        }

        public long SeqNo { get; private set; }

        public DateTimeOffset OccuredDate { get; private set; }

        public abstract string Abbr { get; }

        public override string ToString()
        {
            return $"{this.Abbr}-{this.OccuredDate:yyyyMMdd}-{this.SeqNo}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.SeqNo;
            yield return this.OccuredDate;
            yield return this.Abbr;
        }
    }
}