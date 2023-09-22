using DataWebApp.Data.DbModels;
using Microsoft.AspNetCore.Authorization;
using Ps.EfCoreRepository.SqlServer;
using System.Security.Claims;

namespace DataWebApp.Api.Auth
{
    public class AppAuthorizationHandler : AuthorizationHandler<AppAuthorizationRequirement>
    {
        public AppAuthorizationHandler(IRepository repository)
        {
            Repository = repository;
        }
        public IRepository Repository { get; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppAuthorizationRequirement requirement)
        {
            var emailId = context.User.FindFirst(c => c.Type == ClaimTypes.Email);

            if (emailId == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            Claim employeeIdClaim = context.User.Claims.FirstOrDefault(x => x.Type.ToLower() == "employeeid");

            if (employeeIdClaim == null)
            {
                var claims = new List<Claim>
                {
                    new Claim("EmployeeId", "123456")
                };
                var appIdentity = new ClaimsIdentity(claims);
                context.User.AddIdentity(appIdentity);
            }

            var emp = Repository.GetSingle<Employee>(x => x.Email.ToLower() == emailId.Value.ToLower());
            context.Succeed(requirement);
            //if (emp == null)
            //{
            //    context.Fail();
            //}
            //else
            //{
            //    context.Succeed(requirement);
            //}
            return Task.CompletedTask;
        }
    }
}
