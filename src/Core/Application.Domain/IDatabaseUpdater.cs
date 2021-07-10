namespace Application.Domain
{
    public interface IDatabaseUpdater
    {
        void UpdateDatabase(string connectionString);
    }
}