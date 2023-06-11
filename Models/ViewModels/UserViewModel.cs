using CrmAssistant.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CrmAssistant.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string Email { get; set; }
        public string? Password { get; set; }

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
        public Address? Address { get; set; }

        public List<Country>? Countries { get; set; }

        public IEnumerable<int> HobbiesIds { get; set; }

        public List<SelectListItem> Hobbies { get; set; } = new List<SelectListItem>();
    }
}
