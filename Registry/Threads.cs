using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Core;

namespace Registry
{
    public class Threads
    {

        private readonly Communicator Comm = new Communicator();

        public void SenderThread(Queue<string> SenderTask)
        {
            string message = File.ReadAllText(@"C:\Github\Sauron\Registry\etc\testapp.json");
            while (true)
            {
                Comm.Send(message);
                Thread.Sleep(2000);
            }
        }

        public void ReceiverThread(Action<string> cb)
        {
            Comm.Receiver(cb);
        }

        public void WorkerThread(Queue<string> MainTask, Queue<string> SenderTask)
        {
            while (true)
            {
                bool exists = MainTask.TryDequeue(out string task);
                if (!exists) { continue; }

                Message parsedtask;
                parsedtask = Json.Deserialize(task);

                if (parsedtask.Action == "NewApp")
                {
                    string connectionString = File.ReadAllText(@"C:\Github\Sauron\Registry\etc\connectionstrings.txt");

                    string command = "Management.GetApplication";
                    SqlParameter ApplicationName = new SqlParameter("@ApplicationName", SqlDbType.NVarChar, 100)
                    {
                        Value = parsedtask.Content.ApplicationName
                    };

                    DataSet result = SqlHelper.ExecuteAdapter(connectionString, command,
                        CommandType.StoredProcedure, ApplicationName);
                    DataTable dataTable = result.Tables[0];

                    Application application = new Application(dataTable.Rows);

                    foreach (string exchange in parsedtask.Content.RoutingKeys.Keys)
                    {
                        Dictionary<string, List<string>> bindings = application.Bindings;
                        if (bindings.ContainsKey(exchange))
                        {
                            List<string> diff = parsedtask.Content.RoutingKeys[exchange]
                                .Except(bindings[exchange]).ToList();
                            if (diff.Count > 0)
                            {
                                throw new RequestedBindingException(
                                    "There was more requested fields than allowed for the "
                                    + exchange
                                    + " exchange: "
                                    + Json.Serialize(
                                    new Dictionary<string, List<string>> { { exchange, diff } }));
                            }
                        }
                    }
                }
            }
        }
    }
}
