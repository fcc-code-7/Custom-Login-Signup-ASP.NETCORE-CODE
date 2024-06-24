using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altivix.Entities
{
    public class ProjectTask
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskNotes { get; set; }
        public DateTime Date { get; set; }
        public DateTime CompletedDate { get; set; }

        // Navigation property
        public virtual Projects? Project { get; set; }
    }
}
