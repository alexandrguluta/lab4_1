using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationUser
    {
        [Column("ApplicationUserID")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "User name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for a name is 60 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "User address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for an address is 60 characters")]
        public string Address { get; set; }
        public string Country { get; set; }
        public string PhoneNumber {  get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
