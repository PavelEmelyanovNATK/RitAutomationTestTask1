using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RitAutomationTestTask1.Domain.Models;

namespace RitAutomationTestTask1.Domain.Services.MapDbDao
{
    public interface IMapDbDao
    {
        Machine GetMachine(Guid id);

        IEnumerable<MachineMarker> GetMachineMarkers();

        void UpdateMachinePosition(Guid id, double lat, double lng);
    }
}
