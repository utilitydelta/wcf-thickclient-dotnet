namespace UtilityDelta.Client.ServiceLocator
{
    public interface IDynamicProxyCache
    {
        T GetProxyInstance<T>();
    }
}