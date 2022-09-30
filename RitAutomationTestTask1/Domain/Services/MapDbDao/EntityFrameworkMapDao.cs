using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RitAutomationTestTask1.Domain.Models;
using RitAutomationTestTask1.Domain.Services.MapDbContext;
using RitAutomationTestTask1.Domain.Utils;

namespace RitAutomationTestTask1.Domain.Services.MapDbDao
{
    internal class EntityFrameworkMapDao : IAsyncMapDao
    {
        private readonly MapDbModel _context = new MapDbModel();

        public Machine GetMachine(Guid id)
        {
            return _context.Machines.Find(id)?.ToDTO();
        }

        public async Task<Machine> GetMachineAsync(Guid id)
        {
            return (await _context.Machines.FindAsync(id))?.ToDTO();
        }

        public IEnumerable<MachineMarker> GetMachineMarkers()
        {
            return _context.MachinePositions.ToArray().Select(mp => mp.ToDTO());
        }

        public async Task<IEnumerable<MachineMarker>> GetMachineMarkersAsync()
        {
            return (await _context.MachinePositions.ToArrayAsync()).Select(mp => mp.ToDTO());
        }

        public void UpdateMachinePosition(Guid id, double lat, double lng)
        {
            var machine = _context.Machines.Find(id);

            if (machine == null) return;

            machine.MachinePosition.Latitude = lat;
            machine.MachinePosition.Longitude = lng;

            _context.SaveChanges();
        }

        public async Task UpdateMachinePositionAsync(Guid id, double lat, double lng)
        {
            var machine = await _context.Machines.FindAsync(id);

            if (machine == null) return;

            machine.MachinePosition.Latitude = lat;
            machine.MachinePosition.Longitude = lng;

            await _context.SaveChangesAsync();
        }
    }
}
