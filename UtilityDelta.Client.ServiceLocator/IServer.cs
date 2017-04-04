namespace UtilityDelta.Client.ServiceLocator
{
    public interface IServer
    {
        string Password { get; }
        string Server { get; }
        string Username { get; }
    }
}