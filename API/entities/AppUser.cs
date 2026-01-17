using System;
using System.ComponentModel.DataAnnotations;

namespace API.entities
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

      
        public required byte[] PasswordHash { get; set; } = Array.Empty<byte>();


        public required byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    }
}