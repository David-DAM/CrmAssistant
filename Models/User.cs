using CrmAssistant.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmAssistant.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
        public Address? Address { get; set; }
        public List<Country>? Countries  { get; set; }
    }
}
