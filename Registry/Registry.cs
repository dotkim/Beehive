using System.Threading;
using Core;


namespace Registry
{
    public class ExThread
    {
        private Communicator com = new Communicator();

        // Non-static method 
        public void mythread1()
        {
            while (true)
            {
                Thread.Sleep(10000);
                com.Send("testmelding :)");
            }
        }

        public void mythread2()
        {
            com.Receiver();
        }
    }

    class Registry
    {
        static void Main(string[] args)
        {
            ExThread obj = new ExThread();
            Thread thr = new Thread(obj.mythread1);
            Thread thr2 = new Thread(obj.mythread2);
            thr.Start();
            thr2.Start();
        }
    }
}
