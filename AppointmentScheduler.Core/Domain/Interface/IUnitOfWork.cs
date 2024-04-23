using AppointmentScheduler.Core.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointments { get; }

        Task Save();
    }
}
