using System;
using System.Collections.Generic;
using Core;


namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = new InitializeApplication();
            Communicator com = new Communicator();

            com.Send("testmelding :)");
            Console.WriteLine("sender test");
            Console.ReadLine();
        }
    }
}
