using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.DTO
{
    public class UserRegistrationDTO
    {
        [Required]
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [Required]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [Required]
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [JsonPropertyName("confirmpassword")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [JsonPropertyName("phonenumber")]
        public string PhoneNumber { get; set; }
    }
}
