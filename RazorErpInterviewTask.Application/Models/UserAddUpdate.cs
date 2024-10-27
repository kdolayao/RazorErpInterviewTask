using RazorErpInterviewTask.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorErpInterviewTask.Application.Models
{
    public class UserAddUpdate
    {
        [Required]
        [StringLength(255)]
        public required string Username { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [StringLength(255)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public required string Lastname { get; set; }

        [Required]
        [StringLength(255)]
        public required string Company { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
