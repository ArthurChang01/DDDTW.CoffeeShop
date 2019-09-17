using System;

namespace DDDTW.CoffeeShop.CommonLib.BaseClasses
{
    public class DomainException : Exception
    {
        private readonly string source;
        private readonly Enumeration errorCode;
        private readonly string errorMsg;

        public DomainException(string source, Enumeration errorCode, string errorMsg = null, Exception inner = null)
            : base(errorMsg, inner)
        {
            this.source = source;
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }

        public override string Message => $"Code: {this.source}-{this.errorCode}, Message: {this.errorMsg}";
    }
}