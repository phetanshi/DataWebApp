using Ps.EfCoreRepository.SqlServer;

namespace DataWebApp.UnitTest.Data
{
    public partial class AppRepositoryTests
    {
        [Fact]
        public void Insert_WhenCalledWithOneObject_ReturnsInsertedRecordWithAutogenaratedPrimaryKeyValue()
        {

            Mock<ILogger<Repository>> mockLogging = new Mock<ILogger<Repository>>();

            var dbOptions = TestDatabaseHelper.CreateTestDatabase();

            using (var testDbContext = new TestDbContext(dbOptions))
            {
                Employee emp = new Employee { UserId = "CORP\\e888888", FirstName = "NewTest", LastName = "Test", CreatedBy = DateTime.Now.ToString() };

                Repository repository = new Repository(testDbContext, mockLogging.Object);
                Employee result = repository.Create(emp);

                Assert.Equal(4, result.EmployeeId);
            }
        }
        [Fact]
        public void Insert_WhenCalledWithListOfObjects_ReturnsInsertedRecordWithAutogenaratedPrimaryKeyValue()
        {

            Mock<ILogger<Repository>> mockLogging = new Mock<ILogger<Repository>>();

            var dbOptions = TestDatabaseHelper.CreateTestDatabase();

            using (var testDbContext = new TestDbContext(dbOptions))
            {
                List<Employee> empList = new List<Employee>
                {
                    new Employee { UserId = "CORP\\e888888", FirstName = "NewTest", LastName = "One", CreatedBy = DateTime.Now.ToString() },
                    new Employee { UserId = "CORP\\e777777", FirstName = "NewTest", LastName = "Two", CreatedBy = DateTime.Now.ToString() }
                };

                Repository repository = new Repository(testDbContext, mockLogging.Object);
                List<Employee> result = repository.Create(empList);

                Assert.Equal(4, result[0].EmployeeId);
                Assert.Equal(5, result[1].EmployeeId);
            }
        }

        [Fact]
        public async Task InsertAsync_WhenCalledWithOneObject_ReturnsInsertedRecordWithAutogenaratedPrimaryKeyValue()
        {

            Mock<ILogger<Repository>> mockLogging = new Mock<ILogger<Repository>>();

            var dbOptions = TestDatabaseHelper.CreateTestDatabase();

            using (var testDbContext = new TestDbContext(dbOptions))
            {
                Employee emp = new Employee { UserId = "CORP\\e888888", FirstName = "NewTest", LastName = "Test", CreatedBy = DateTime.Now.ToString() };

                Repository repository = new Repository(testDbContext, mockLogging.Object);
                Employee result = await repository.CreateAsync(emp);

                Assert.Equal(4, result.EmployeeId);
            }
        }
        [Fact]
        public async Task InsertAsync_WhenCalledWithListOfObjects_ReturnsInsertedRecordWithAutogenaratedPrimaryKeyValue()
        {

            Mock<ILogger<Repository>> mockLogging = new Mock<ILogger<Repository>>();

            var dbOptions = TestDatabaseHelper.CreateTestDatabase();

            using (var testDbContext = new TestDbContext(dbOptions))
            {
                List<Employee> empList = new List<Employee>
                {
                    new Employee { UserId = "CORP\\e888888", FirstName = "NewTest", LastName = "One", CreatedBy = DateTime.Now.ToString() },
                    new Employee { UserId = "CORP\\e777777", FirstName = "NewTest", LastName = "Two", CreatedBy = DateTime.Now.ToString() }
                };

                Repository repository = new Repository(testDbContext, mockLogging.Object);
                List<Employee> result = await repository.CreateAsync(empList);

                Assert.Equal(4, result[0].EmployeeId);
                Assert.Equal(5, result[1].EmployeeId);
            }
        }
    }
}
