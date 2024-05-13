namespace EFCoreAssignmentDay1.Configs
{
    public class DatabaseConnections
    {
        public string ConnectionString { get; set; } = "Server=localhost;Database=MyApiDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";
    }
}
