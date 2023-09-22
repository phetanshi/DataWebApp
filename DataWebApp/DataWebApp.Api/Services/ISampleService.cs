using DataWebApp.Data.DbModels;
using DataWebApp.Dtos;

namespace DataWebApp.Api.Services
{
    public interface ISampleService
    {
        Task<bool> DeleteEmployeeAsync(int id);
        Task<List<Employee>> Get();
        Task<EmployeeDto> InsertEmployeeAsync(EmployeeDto empDto);
        Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto empDto);
    }
}
