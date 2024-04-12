using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.DTO
{
    public class GetAllAppointmentsDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("attendees")]
        public List<string> Attendees { get; set; }
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }
    }
}
