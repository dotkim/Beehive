using System;
using System.IO;
using System.Threading;
using System.Text.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using Core;


namespace Registry
{
    public class ExThread
    {
        private readonly Communicator Comm = new Communicator();

        // Non-static method
        public void SenderThread(Queue<string> SenderTask)
        {
            string message = File.ReadAllText(@"C:\Github\Sauron\Registry\testapp.json");
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
                    //check db
                }
            }
        }
    }

    public class MessageContent
    {
        public Dictionary<string, List<string>> RoutingKeys { get; set; }
        public string Queue { get; set; }
        public string ApplicationName { get; set; }
    }

    public class Message
    {
        public string Action { get; set; }
        public MessageContent Content { get; set; }
    }

    public static class Json
    {
        public static Message Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Message>(json);
        }
    }

    class Registry
    {
        private static Queue<string> MainTask = new Queue<string>();
        private static Queue<string> SenderTask = new Queue<string>();

        private static void AddToMainTask(string task)
        {
            MainTask.Enqueue(task);
            Console.WriteLine("New task was queued, new count is: " + MainTask.Count.ToString());
        }

        static void Main()
        {
            ExThread obj = new ExThread();

            Thread sender = new Thread(() => obj.SenderThread(SenderTask));
            sender.Name = "Sender";
            Thread receiver = new Thread(() => obj.ReceiverThread(AddToMainTask));
            receiver.Name = "Receiver";
            Thread worker = new Thread(() => obj.WorkerThread(MainTask, SenderTask));
            worker.Name = "worker";

            sender.Start();
            receiver.Start();
            worker.Start();
        }
    }
}