using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste
{
    public static class DummyData
    {
        public class Employee
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Position { get; set; }

            public double Salary { get; set; }

            public DateTime JoinedDate { get; set; }
        }

        public static IEnumerable<Employee> GetEmployeeData()
        {
            return new Employee[]
            {
                new Employee
                {
                    Id = 1,
                    Name = "Jack",
                    Position = "CEO",
                    Salary = 100000,
                    JoinedDate = DateTime.UtcNow.AddYears(-2)
                },
                new Employee
                {
                    Id = 2,
                    Name = "Bill",
                    Position = "Software Engineer",
                    Salary = 90000,
                    JoinedDate = DateTime.UtcNow.AddYears(-1)
                },
                new Employee
                {
                    Id = 3,
                    Name = "Steve",
                    Position = "QA",
                    Salary = 80000,
                    JoinedDate = DateTime.UtcNow.AddMonths(-2)
                }
            };
        }
    }
}
