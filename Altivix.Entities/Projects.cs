using Altivix.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altivix.Entities
{
    public class Projects
    {
        public int ID { get; set; }
        public string? ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Duration { get; set; }
        public string? ClientID { get; set; }
        public string? AssignedTo { get; set; }
        public string? Status { get; set; }

        // Navigation properties
        public virtual AppUser? Client { get; set; }
        public virtual AppUser? Employee { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
        public virtual ICollection<ProjectGallery> ProjectGalleries { get; set; } = new List<ProjectGallery>();
    }
}
