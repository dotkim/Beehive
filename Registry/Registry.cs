using System.Threading;
using Core;


namespace Registry
{
    public class ExThread
    {
        private Communicator com = new Communicator();

        // Non-static method
        public void SenderThread(string message)
        {
            while (true)
            {
                com.Send(message);
                Thread.Sleep(2000);
            }
        }

        public void ReceiverThread()
        {
            com.Receiver();
        }
    }

    class Registry
    {
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

            Thread thr = new Thread(() => obj.SenderThread(message));
            Thread thr2 = new Thread(obj.ReceiverThread);

            thr.Start();
            thr2.Start();
        }
    }
}
