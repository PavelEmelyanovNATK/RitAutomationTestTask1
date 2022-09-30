using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RitAutomationTestTask1.Domain.Models;

namespace RitAutomationTestTask1.Domain.Services.MapDbDao
{
    internal interface IAsyncMapDao : IMapDbDao
    {
        Task<Machine> GetMachineAsync(Guid id);

        Task<IEnumerable<MachineMarker>> GetMachineMarkersAsync();

        Task UpdateMachinePositionAsync(Guid id, double lat, double lng);
    }
}
