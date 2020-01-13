using Microsoft.AspNetCore.Authorization;

namespace Auth.Demo
{
    public class EmployeeWithMoreYearsRequirement : IAuthorizationRequirement
    {
        public EmployeeWithMoreYearsRequirement(int years)
        {
            Years = years;
        }

        public int Years { get; set; }
    }
}