namespace MB.Services.Catalog.Settings
{
    public interface IDatabaseSettings
    {
        string ProductCollectionName { get; set; }
        string CategoryCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
