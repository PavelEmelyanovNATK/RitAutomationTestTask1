using RitAutomationTestTask1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitAutomationTestTask1.Domain.Services.MapDbDao
{
    public class MapDbDao
    {
        //Изменить на название своего сервера
        private const string ServerName = "NHPC\\SQLEXPRESS";
        private readonly string ConnectionString = $"Server={ServerName};Database=MachinesMap;Trusted_Connection=True;";

        public Machine GetMachine(Guid id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    var getMachineCommand = new SqlCommand("SELECT * FROM Machines WHERE Id = @id", connection);
                    var idParameter = new SqlParameter("@id", id);
                    getMachineCommand.Parameters.Add(idParameter);

                    using (var reader = getMachineCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Machine
                            {
                                Id = Guid.Parse(Convert.ToString(reader["Id"])),
                                Name = Convert.ToString(reader["Name"])
                            };
                        }
                    }
                }
                finally
                {
                    if(connection.State == ConnectionState.Open)
                    connection.Close();
                }

                return null;
            }
        }

        public IEnumerable<MachineMarker> GetMachineMarkers()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {

                try
                {
                    connection.Open();

                    var getMachineMarkersCommand = new SqlCommand("SELECT * FROM MachinePositions", connection);

                    using (var reader = getMachineMarkersCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var machineId = Guid.Parse(Convert.ToString(reader["Id"]));

                            yield return new MachineMarker
                            {
                                Machine = GetMachine(machineId),
                                Latitude = Convert.ToDouble(reader["Latitude"]),
                                Longitude = Convert.ToDouble(reader["Longitude"])
                            };
                        }
                    }
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }   
            }
        }

        public void UpdateMachinePosition(Guid id, double lat, double lng)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {

                try
                {
                    connection.Open();

                    var updateMachinePositionCommand = new SqlCommand("UPDATE MachinePositions " +
                        "SET Latitude = @lat, Longitude = @lng WHERE Id = @id", connection);
                    updateMachinePositionCommand.Parameters.Add(new SqlParameter("@lat", lat));
                    updateMachinePositionCommand.Parameters.Add(new SqlParameter("@lng", lng));
                    updateMachinePositionCommand.Parameters.Add(new SqlParameter("@id", id));

                    updateMachinePositionCommand.ExecuteNonQuery();
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public async Task<Machine> GetMachineAsync(Guid id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var getMachineCommand = new SqlCommand("SELECT * FROM Machines WHERE Id = @id", connection);
                    var idParameter = new SqlParameter("@id", id);
                    getMachineCommand.Parameters.Add(idParameter);

                    using (var reader = await getMachineCommand.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Machine
                            {
                                Id = Guid.Parse(Convert.ToString(reader["Id"])),
                                Name = Convert.ToString(reader["Name"])
                            };
                        }
                    }
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

                return null;
            }
        }

        public async Task<IEnumerable<MachineMarker>> GetMachineMarkersAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var getMachineMarkersCommand = new SqlCommand("SELECT * FROM MachinePositions", connection);

                    using (var reader = await getMachineMarkersCommand.ExecuteReaderAsync())
                    {
                        var outList = new LinkedList<MachineMarker>();

                        while (reader.Read())
                        {
                            var machineId = Guid.Parse(Convert.ToString(reader["Id"]));

                            outList.AddLast(new MachineMarker
                            {
                                Machine = await GetMachineAsync(machineId),
                                Latitude = Convert.ToDouble(reader["Latitude"]),
                                Longitude = Convert.ToDouble(reader["Longitude"])
                            });
                        }

                        return outList;
                    }
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        public async Task UpdateMachinePositionAsync(Guid id, double lat, double lng)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {

                try
                {
                    await connection.OpenAsync();

                    var updateMachinePositionCommand = new SqlCommand("UPDATE MachinePositions " +
                        "SET Latitude = @lat, Longitude = @lng WHERE Id = @id", connection);
                    updateMachinePositionCommand.Parameters.Add(new SqlParameter("@lat", lat));
                    updateMachinePositionCommand.Parameters.Add(new SqlParameter("@lng", lng));
                    updateMachinePositionCommand.Parameters.Add(new SqlParameter("@id", id));

                    await updateMachinePositionCommand.ExecuteNonQueryAsync();
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }
    }
}
