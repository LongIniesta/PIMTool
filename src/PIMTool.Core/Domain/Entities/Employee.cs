using PIMTool.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PIMTool.Core.Domain.Entities
{
    public partial class Employee : IEntity
    {
        public Employee()
        {
            Groups = new HashSet<Group>();
            Projects = new HashSet<Project>();
        }

        public decimal Id { get; set; }
        public string Visa { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public byte[] Version { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
