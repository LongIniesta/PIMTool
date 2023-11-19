using PIMTool.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PIMTool.Core.Domain.Entities
{
    public partial class Project : IEntity
    {
        public Project()
        {
            Employees = new HashSet<Employee>();
        }

        public decimal Id { get; set; }
        public decimal GroupId { get; set; }
        public decimal ProjectNumber { get; set; }
        public string Name { get; set; } = null!;
        public string Customer { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte[] Version { get; set; } = null!;

        public virtual Group Group { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
