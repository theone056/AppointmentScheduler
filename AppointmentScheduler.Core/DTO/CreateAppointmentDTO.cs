using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.DTO
{
    public class CreateAppointmentDTO
    {
        [JsonPropertyName("date")]
        [Required]
        public DateTime Date { get; set; }
        [JsonPropertyName("title")]
        [Required]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("attendees")]
        [Required]
        public List<string> Attendees { get; set; }
        [JsonPropertyName("createdBy")]
        [Required]
        public string CreatedBy { get; set; }
    }
}
