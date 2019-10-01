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

            _ = com.Send("testmelding :)");
        }
    }
}
