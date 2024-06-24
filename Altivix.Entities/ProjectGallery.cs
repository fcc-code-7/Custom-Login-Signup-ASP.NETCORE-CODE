using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altivix.Entities
{
    public class ProjectGallery
    {
        public int ID { get; set; }
        public string? ImageURL { get; set; }
        public int ProjectID { get; set; }

        // Navigation property
        public virtual Projects? Project { get; set; }
    }
}
