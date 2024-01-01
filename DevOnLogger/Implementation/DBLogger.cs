using DevOnLogger.Interface;
using DevOnLogger.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOnLogger.Implementation
{
    /// <summary>
    /// DBLogger: Implements ICustomLogger which Sink messages to database table.
    /// </summary>
    public class DBLogger : ICustomLogger
    {
        ApplicationDBContext context;

        /// <summary>
        /// Creates Database context based on ConnectionString provided.
        /// This constructor checks if :pgTable exists or not, and creates table if doesn't exists.
        /// </summary>
        /// <param name="ConnectionString"></param>
        public DBLogger(string ConnectionString)
        {
            try
            {
                var dbContextOptions = new DbContextOptionsBuilder().UseSqlServer(ConnectionString);

                context = new ApplicationDBContext(dbContextOptions.Options);

                var conn = context.Database.GetDbConnection();
                if (conn.State.Equals(ConnectionState.Closed)) conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"SELECT 1 FROM INFORMATION_SCHEMA.TABLES   WHERE  TABLE_NAME = 'LogTable'";

                    if (command.ExecuteScalar() == null)
                    {
                        RelationalDatabaseCreator databaseCreator =
                         (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                        databaseCreator.CreateTables();
                    }

                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        /// <summary>
        /// LogMessage: Insert Client log messages to database table
        /// </summary>
        /// <param name="LogMessage"></param>
        public async Task LogMessageAsync (string LogMessage)
        {
            try
            {
                context.Database.SetCommandTimeout(5);
                if (context != null)
                {
                    await context.LogTable.AddAsync(CreateLogEntity(LogMessage));

                       context.SaveChanges ();
                    return;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while updating log to Database sink.", e);
            }
        }

        public void LogMessage(string LogMessage)
        {
            try
            {
                if (context != null)
                {
                    context.LogTable.Add(CreateLogEntity(LogMessage));

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while updating log to Database sink.", e);
            }
        }

        /// <summary>
        /// CreateLogEntity: bind data to LogTable object
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <returns></returns>
        private LogTable CreateLogEntity(string LogMessage)
        {
            return new Models.LogTable()
            {
                Message = LogMessage,
                OnDate = DateTime.Now
            };
        }

    }
}
