using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using MachineDTO = RitAutomationTestTask1.Domain.Models.Machine;
using MachineMarkerDTO = RitAutomationTestTask1.Domain.Models.MachineMarker;
using RitAutomationTestTask1.Domain.Models.EntityFrameworkModels;

namespace RitAutomationTestTask1.Domain.Utils
{
    public class EntityFrameworkMapper
    {
        public MachineDTO MachineToDTO(Machine self) => new MachineDTO { Id = self.Id, Name = self.Name };

        public MachineMarkerDTO PositionToDTO(MachinePosition self) => new MachineMarkerDTO { Latitude = self.Latitude, Longitude = self.Longitude, Machine = MachineToDTO(self.Machine) };
    }

    public static class EntityFrameworkMapperExtensions
    {
        public static MachineDTO ToDTO(this Machine self) => new MachineDTO { Id = self.Id, Name = self.Name };

        public static MachineMarkerDTO ToDTO(this MachinePosition self) => new MachineMarkerDTO { Latitude = self.Latitude, Longitude = self.Longitude, Machine = self.Machine.ToDTO() };
    }
}
