using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorErpInterviewTask.Application.Models
{
    public class UserLogin
    {
        [Required]
        [StringLength(255)]
        public required string Username { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

    }
}
