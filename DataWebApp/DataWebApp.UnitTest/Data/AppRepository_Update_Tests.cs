using Ps.EfCoreRepository.SqlServer;

namespace DataWebApp.UnitTest.Data
{
    public partial class AppRepositoryTests
    {
        [Fact]
        public void Update_WhenCalledWithOneObject_ReturnsNothingButDataWillbeUpdatedInDb()
        {

            Mock<ILogger<Repository>> mockLogging = new Mock<ILogger<Repository>>();

            var dbOptions = TestDatabaseHelper.CreateTestDatabase();

            using (var testDbContext = new TestDbContext(dbOptions))
            {
                Employee emp = new Employee { EmployeeId = 1, UserId = "CORP\\e999999", FirstName = "TestUpdated", LastName = "Updated", CreatedBy = DateTime.Now.ToString() };

                Repository repository = new Repository(testDbContext, mockLogging.Object);
                repository.Update(emp);

                Employee empTest = repository.GetSingle<Employee>(1);

                Assert.Equal("TestUpdated", empTest.FirstName);
            }
        }

        [Fact]
        public async Task UpdateAsync_WhenCalledWithOneObject_ReturnsNothingButDataWillbeUpdatedInDb()
        {

            Mock<ILogger<Repository>> mockLogging = new Mock<ILogger<Repository>>();

            var dbOptions = TestDatabaseHelper.CreateTestDatabase();

            using (var testDbContext = new TestDbContext(dbOptions))
            {
                Employee emp = new Employee { EmployeeId = 1, UserId = "CORP\\e999999", FirstName = "TestUpdatedAsync", LastName = "Updated", CreatedBy = DateTime.Now.ToString() };

                Repository repository = new Repository(testDbContext, mockLogging.Object);
                await repository.UpdateAsync(emp);

                Employee empTest = repository.GetSingle<Employee>(1);

                Assert.Equal("TestUpdatedAsync", empTest.FirstName);
            }
        }
    }
}
