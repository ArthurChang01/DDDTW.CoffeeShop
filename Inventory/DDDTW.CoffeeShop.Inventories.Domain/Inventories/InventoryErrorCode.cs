using DDDTW.CoffeeShop.CommonLib.BaseClasses;

namespace DDDTW.CoffeeShop.Inventories.Domain.Inventories
{
    public class InventoryErrorCode : Enumeration
    {
        public static readonly InventoryErrorCode InventoryShortage = new InventoryErrorCode(0, nameof(InventoryShortage));
        public static readonly InventoryErrorCode OverUpperBound = new InventoryErrorCode(1, nameof(OverUpperBound));
        public static readonly InventoryErrorCode QtyIsNegative = new InventoryErrorCode(2, nameof(QtyIsNegative));
        public static readonly InventoryErrorCode InventoryItemIsNull = new InventoryErrorCode(3, nameof(InventoryItemIsNull));
        public static readonly InventoryErrorCode ConstraintIsEmpty = new InventoryErrorCode(4, nameof(ConstraintIsEmpty));
        public static readonly InventoryErrorCode AmountIsNegative = new InventoryErrorCode(5, nameof(AmountIsNegative));
        public static readonly InventoryErrorCode ConstraintValueIncorrect = new InventoryErrorCode(6, nameof(ConstraintValueIncorrect));

        public InventoryErrorCode(int id, string name) : base(id, name)
        {
        }
    }
}