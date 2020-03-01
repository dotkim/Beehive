using System;
using System.Data;
using System.Data.SqlClient;

namespace Registry
{
    // Most of the class was gotten from https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand?view=netframework-4.8
    static class SqlHelper
    {
        // Set the connection, command, and then execute the command with non query.  
        public static Int32 ExecuteNonQuery(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(commandText, conn)
            {
                // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                // type is only for OLE DB.    
                CommandType = commandType
            };

            cmd.Parameters.AddRange(parameters);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        // Set the connection, command, and then execute the command and only return one value.  
        public static Object ExecuteScalar(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(commandText, conn)
            {
                CommandType = commandType
            };

            cmd.Parameters.AddRange(parameters);

            conn.Open();
            return cmd.ExecuteScalar();
        }

        // Set the connection, command, and then execute the command with query and return the reader.  
        public static SqlDataReader ExecuteReader(String connectionString, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            using SqlCommand cmd = new SqlCommand(commandText, conn)
            {
                CommandType = commandType
            };

            cmd.Parameters.AddRange(parameters);

            conn.Open();
            // When using CommandBehavior.CloseConnection, the connection will be closed when the   
            // IDataReader is closed.  
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public static DataSet ExecuteAdapter(string connectionString, string commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            using SqlCommand cmd = new SqlCommand(commandText, conn)
            {
                CommandType = commandType
            };

            cmd.Parameters.AddRange(parameters);

            SqlDataAdapter adapter = new SqlDataAdapter
            {
                SelectCommand = cmd
            };
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);

            return dataset;
        }
    }
}
