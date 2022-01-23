using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsExercises.Solutions.Fundamentals.GraphTheoryTraversalAndShortestPath
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public bool IsManager { get => Employees.Any(); }

        public List<Employee> Employees { get; set; }

        public int Salary
        {
            get
            {
                var salary = Employees.Select(e => e.Salary).Sum();
                if (salary == 0)
                {
                    return 1;
                }

                return salary;
            }
        }
    }

    public class Salaries
    {
        private static List<Employee> _employees = new List<Employee>();

        public static void CalculateSalaries()
        {
            var numberOfEmployees = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfEmployees; i++)
            {
                _employees.Add(
                    new Employee
                    {
                        EmployeeId = i,
                        Employees = new List<Employee>()
                    });
            }

            for (int i = 0; i < numberOfEmployees; i++)
            {
                var employee = _employees.FirstOrDefault(e => e.EmployeeId == i);
                var suboordinates = Console.ReadLine();
                for (int j = 0; j < suboordinates.Length; j++)
                {
                    if (suboordinates[j] == 'Y')
                    {
                        var suboordinateEmployee = _employees.First(e => e.EmployeeId == j);
                        employee.Employees.Add(suboordinateEmployee);
                    }
                }
            }

            var isGraphAcyclic = IsGraphAcyclic();
            if (!isGraphAcyclic)
            {
                Console.WriteLine("Cyclic");
                return;
            }

            Console.WriteLine(_employees.Select(e => e.Salary).Sum());
        }

        private static bool IsGraphAcyclic()
        {
            try
            {
                foreach (var employee in _employees)
                {
                    DFS(employee, employee);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private static void DFS(Employee curStartingPoint, Employee originalStartingPoint)
        {
            if (!curStartingPoint.Employees.Any())
            {
                return;
            }

            foreach (var child in curStartingPoint.Employees)
            {
                if (child == originalStartingPoint)
                {
                    throw new InvalidOperationException("Acyclic: No");
                }

                DFS(child, originalStartingPoint);
            }
        }
    }
}
