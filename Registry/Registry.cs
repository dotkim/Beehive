using System;
using System.Threading;
using System.Collections.Generic;

namespace Registry
{
    class Registry
    {
        private static Queue<string> MainTask = new Queue<string>();
        private static Queue<string> SenderTask = new Queue<string>();

        private static void AddToMainTask(string task)
        {
            MainTask.Enqueue(task);
            Console.WriteLine("New task was queued, new count is: " 
                + MainTask.Count.ToString());
        }

        static void Main()
        {
            Threads obj = new Threads();

            Thread sender = new Thread(()
                => obj.SenderThread(SenderTask))
            {
                Name = "Sender"
            };

            Thread receiver = new Thread(()
                => obj.ReceiverThread(AddToMainTask))
            {
                Name = "Receiver"
            };

            Thread worker = new Thread(()
                => obj.WorkerThread(MainTask, SenderTask))
            {
                Name = "worker"
            };

            sender.Start();
            receiver.Start();
            worker.Start();
        }
    }
}