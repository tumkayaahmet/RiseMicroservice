namespace Report.Settings
{
    public interface IDatabaseSettings
    {
        public string ReportCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
