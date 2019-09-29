using System;
using System.Collections.Generic;
using Core;


namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Communicator com = new Communicator();
            com.Send("Hello World!");
            List<string> messages = com.Receiver();
            Console.WriteLine(messages.Count);
        }
    }
}
