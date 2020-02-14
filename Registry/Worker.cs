using System;
using System.Collections.Generic;
using System.Text;

namespace Registry
{
    public class Worker
    {
        public Dictionary<string, Action> Actions { get; }

        public Worker()
        {
            Actions = new Dictionary<string, Action> {
                {"NewApp", NewApp}
            };
        }

        public void NewApp()
        {
            
        }
    }
}