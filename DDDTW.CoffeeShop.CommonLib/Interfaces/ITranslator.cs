namespace DDDTW.CoffeeShop.CommonLib.Interfaces
{
    public interface ITranslator<Tout, Tin>
    {
        Tout Translate(Tin input);
    }
}