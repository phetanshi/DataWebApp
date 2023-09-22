using AutoMapper;
using DataWebApp.Data.DbModels;
using DataWebApp.Dtos;
using Microsoft.EntityFrameworkCore;
using Ps.EfCoreRepository.SqlServer;

namespace DataWebApp.Api.Services.Definitions
{
    public class SampleService : ServiceBase, ISampleService
    {
        public SampleService(IRepository repository, ILogger<SampleService> logger, IConfiguration config, IMapper mapper, IHttpContextAccessor context) : base(repository, logger, config, mapper, context)
        {
        }

        public async Task<List<Employee>> Get()
        {
            var result = await Repository.GetListAsync<Employee>();
            return await result.ToListAsync();
        }

        public async Task<EmployeeDto> InsertEmployeeAsync(EmployeeDto empDto)
        {
            Employee emp = new Employee();
            Mapper.Map(empDto, emp);
            SetDefaults(emp);
            await Repository.CreateAsync(emp);
            Mapper.Map(emp, empDto);
            return empDto;
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(EmployeeDto empDto)
        {
            Employee emp = await Repository.GetSingleAsync<Employee>(empDto.EmployeeId);
            Mapper.Map(empDto, emp);
            SetDefaults(emp);
            await Repository.UpdateAsync(emp);
            return empDto;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            await Repository.DeleteAsync<Employee>(id);
            return true;
        }

        private void SetDefaults(Employee emp)
        {
            if (emp != null)
            {
                if (emp.EmployeeId > 0)
                {
                    emp.UpdatedBy = GetLoginUserId();
                    emp.UpdatedDate = DateTime.UtcNow;
                }
                else
                {
                    emp.CreatedBy = GetLoginUserId();
                    emp.CreatedDate = DateTime.UtcNow;
                    emp.UpdatedBy = GetLoginUserId();
                    emp.UpdatedDate = DateTime.UtcNow;
                }
            }
        }
    }
}
