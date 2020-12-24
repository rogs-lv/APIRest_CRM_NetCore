using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    /// <summary>
    /// Represent the data for request login
    /// </summary>
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
