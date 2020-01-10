using System;
using System.Threading;
using System.Text.Json;
using System.Collections.Generic;
using Core;


namespace Registry
{
    public class ExThread
    {
        private readonly Communicator Comm = new Communicator();

        // Non-static method
        public void SenderThread(string message)
        {
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

        public void WorkerThread(Queue<string> MainTask)
        {
            while (true)
            {
                bool exists = MainTask.TryDequeue(out string task);
                if (exists)
                {
                    Message parsedtask = Json.Deserialize(task);
                    Console.WriteLine(parsedtask.Action);
                    Console.WriteLine(parsedtask.Content.ApplicationName);
                    Console.WriteLine(parsedtask.Content.Queue);

                    foreach (string field in parsedtask.Content.RoutingKeys["User"])
                    {
                        Console.WriteLine(field);
                    }
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

        private static void AddToMainTask(string task)
        {
            MainTask.Enqueue(task);
            Console.WriteLine("New task was queued, new count is: " + MainTask.Count.ToString());
        }

        static void Main(string[] args)
        {
            string message;

            if (args.Length > 0)
            {
                message = args[0];
            }
            else
            {
                message = "{\"Action\": \"newapp\",\"Content\":{\"RoutingKeys\":{\"User\": [\"FirstName\", \"LastName\", \"Phone\"]},\"ApplicationName\": \"testapp\",\"Queue\": \"testapp\"}}";
            }

            ExThread obj = new ExThread();

            Thread sender = new Thread(() => obj.SenderThread(message));
            sender.Name = "Sender";
            Thread receiver = new Thread(() => obj.ReceiverThread(AddToMainTask));
            receiver.Name = "Receiver";
            Thread worker = new Thread(() => obj.WorkerThread(MainTask));
            worker.Name = "worker";

            sender.Start();
            receiver.Start();
            worker.Start();
        }
    }
}