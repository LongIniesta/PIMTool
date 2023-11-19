using PIMTool.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PIMTool.Core.Domain.Entities
{
    public partial class Group : IEntity
    {
        public Group()
        {
            Projects = new HashSet<Project>();
        }

        public decimal Id { get; set; }
        public decimal GroupLeaderId { get; set; }
        public byte[] Version { get; set; } = null!;

        public virtual Employee GroupLeader { get; set; } = null!;
        public virtual ICollection<Project> Projects { get; set; }
    }
}
