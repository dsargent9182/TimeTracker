using System.Data;
using Microsoft.Data.SqlClient;

namespace DataLayer.Context
{
	public class DapperContext
	{
		private readonly string _connectionString;

		public DapperContext(string connectionString)
		{
			_connectionString = connectionString;
		}
		public IDbConnection CreateConnection()
		{
			var conn = new SqlConnection(_connectionString);
			return conn;
		}
	}
}
