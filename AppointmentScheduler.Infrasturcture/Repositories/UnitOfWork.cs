using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Infrasturcture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Infrasturcture.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IAppointmentRepository Appointments { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Appointments = new AppointmentRepository(context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
