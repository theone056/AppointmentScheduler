﻿using AppointmentScheduler.Core.Domain.Entities;
using AppointmentScheduler.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Domain.Interface
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {

    }
}
