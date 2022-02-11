namespace TcOpenHammer.Grafana.API.Model.Mongo
{
    public class DatabaseSettings
    {
        public string ProductionCollectionName { get; set; }
        public string StationModesCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Station001RecipeHistoryCollection { get; set; }
    }

}
