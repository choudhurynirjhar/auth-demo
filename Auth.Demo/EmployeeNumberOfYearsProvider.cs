namespace Auth.Demo
{
    public class EmployeeNumberOfYearsProvider : IEmployeeNumberOfYearsProvider
    {
        public int Get(string value)
        {
            if(value == "test1")
            {
                return 21;
            }
            return 10;
        }
    }
}