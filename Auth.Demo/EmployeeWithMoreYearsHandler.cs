using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Demo
{
    public class EmployeeWithMoreYearsHandler : AuthorizationHandler<EmployeeWithMoreYearsRequirement>
    {
        private readonly IEmployeeNumberOfYearsProvider employeeNumberOfYearsProvider;

        public EmployeeWithMoreYearsHandler(IEmployeeNumberOfYearsProvider employeeNumberOfYearsProvider)
        {
            this.employeeNumberOfYearsProvider = employeeNumberOfYearsProvider;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            EmployeeWithMoreYearsRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                return Task.CompletedTask;
            }

            var name = context.User.FindFirst(c => c.Type == ClaimTypes.Name);

            int numberofyears = employeeNumberOfYearsProvider.Get(name.Value);

            if(numberofyears >= requirement.Years)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}