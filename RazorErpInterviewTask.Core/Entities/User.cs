using RazorErpInterviewTask.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RazorErpInterviewTask.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

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
        public Role Role { get; set; }

    }
}
