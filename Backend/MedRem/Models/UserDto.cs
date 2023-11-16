using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedRem.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}