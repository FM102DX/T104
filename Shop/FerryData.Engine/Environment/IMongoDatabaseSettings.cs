namespace T104.Store.Engine.Environment
{
    public interface IMongoDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
