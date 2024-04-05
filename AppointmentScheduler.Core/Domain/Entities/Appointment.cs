using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public List<string> Attendees { get; set; }
        [StringLength(30)]
        public string CreatedBy { get; set; }
    }
}
