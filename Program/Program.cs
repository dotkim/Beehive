using System;
using System.Collections.Generic;
using Core;


namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            new InitializeApplication();
            Communicator com = new Communicator();

            com.Send("testmelding :)");
            com.Receiver();

            Environment.Exit(100); // done :)
        }
    }
}
