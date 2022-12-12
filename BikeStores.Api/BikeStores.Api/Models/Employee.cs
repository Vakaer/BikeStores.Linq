using System;
using System.Collections.Generic;

namespace BikeStores.Api.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseEmpSupvNavigation = new HashSet<Employee>();
        }

        public string EmpId { get; set; } = null!;
        public string? EmpName { get; set; }
        public DateTime? DtOfJoin { get; set; }
        public string? EmpSupv { get; set; }

        public virtual Employee? EmpSupvNavigation { get; set; }
        public virtual ICollection<Employee> InverseEmpSupvNavigation { get; set; }
    }
}
