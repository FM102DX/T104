namespace T104.Store.Engine.Environment
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }

    public class MyCat: IMyCat
    {
        public string MyCatName { get; set; }

        public string MyCatColor { get; set; }
    }
    public interface IMyCat
    {
        public string MyCatName { get; set; }

        public string MyCatColor { get; set; }
    }
}
