using System;
using System.Threading;
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
                message = "Testmelding";
            }

            ExThread obj = new ExThread();

            Thread sender = new Thread(() => obj.SenderThread(message));
            sender.Name = "Sender";
            Thread receiver = new Thread(() => obj.ReceiverThread(AddToMainTask));
            receiver.Name = "Receiver";

            sender.Start();
            receiver.Start();
        }
    }
}