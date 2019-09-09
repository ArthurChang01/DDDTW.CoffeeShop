namespace DDDTW.CoffeeShop.Inventory.Domain.Inventories
{
    public enum InventoryErrorCode
    {
        InventoryShortage = 0,
        OverUpperBound = 1,
        QtyIsNegative = 2,
        InventoryItemIsNull = 3,
        ConstraintIsEmpty = 4,
        AmountIsNegative = 5,
        ConstraintValueIncorrect = 6
    }
}