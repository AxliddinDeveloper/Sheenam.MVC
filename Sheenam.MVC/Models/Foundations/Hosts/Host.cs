using System.ComponentModel.DataAnnotations;
using System;

namespace Sheenam.MVC.Models.Foundations.Hosts
{
    public class Host
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
