using System;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace CrateTrack.Database
{
    public class DbHelper
    {
       private readonly IConfiguration _configuration;
        public DbHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public NpgsqlConnection GetConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString);
        }
    }
}
