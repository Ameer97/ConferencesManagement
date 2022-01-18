using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConferencesManagement.Models
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
        public string Code { get; set; }

        public ICollection<Microsoft.AspNetCore.Identity.IdentityUser> Users { get; set; }

        [NotMapped]
        public ICollection<string> UsersIds { get; set; }
    }
}
