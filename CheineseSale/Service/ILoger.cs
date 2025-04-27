namespace CheineseSale.Service
{
    public interface ILoger
    {
        void Log(string message);
        void LogError(string message);
        void LogWarning(string message);
    }
}
