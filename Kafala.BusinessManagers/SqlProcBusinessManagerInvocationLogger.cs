using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Persistence;

namespace Kafala.BusinessManagers
{
    public class SqlProcBusinessManagerInvocationLogger : IBusinessManagerInvocationLogger
    {
        private readonly IConnectionString connectionString;

        public SqlProcBusinessManagerInvocationLogger(IConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Log(Guid invocationId, DateTime startTime, string managerName, string methodName, string parametersJson, string userIdentifier)
        {
            using (var tx = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var connection = new SqlConnection(this.connectionString.Value))
                {
                    connection.Open();

                    using (var sqlCommand = connection.CreateCommand())
                    {
                        sqlCommand.CommandText = "[LogInvocation]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@Key", invocationId);
                        sqlCommand.Parameters.AddWithValue("@ManagerName", managerName);
                        sqlCommand.Parameters.AddWithValue("@MethodName", methodName);
                        sqlCommand.Parameters.AddWithValue("@ParametersData", parametersJson);
                        sqlCommand.Parameters.AddWithValue("@UserId", userIdentifier);
                        sqlCommand.Parameters.AddWithValue("@StartDate", startTime);

                        sqlCommand.ExecuteNonQuery();
                    }
                }

                tx.Complete();
            }
        }
    }
}