using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models
{
    public enum UserType
    {
        Admin = 0,
        User = 1
    }
    public class User
    {
        [Required]
        [StringLength(25)]
        public string UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public UserType UserType { get; set; }
        public List<Solved> Solved { get; set; }
    }
}
