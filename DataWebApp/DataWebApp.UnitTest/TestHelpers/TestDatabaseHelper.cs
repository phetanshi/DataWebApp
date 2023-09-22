namespace DataWebApp.UnitTest.TestHelpers
{
    public class TestDatabaseHelper
    {
        public static DbContextOptions<AppDbContext> CreateTestDatabase()
        {
            var testDbCon = new SqliteConnection("DataSource=:memory:");
            testDbCon.Open();

            DbContextOptions<AppDbContext> dbOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(testDbCon).EnableSensitiveDataLogging().Options;

            using (var testDbContext = new TestDbContext(dbOptions))
            {
                testDbContext.Database.EnsureCreated();
            }

            return dbOptions;
        }
    }
}
