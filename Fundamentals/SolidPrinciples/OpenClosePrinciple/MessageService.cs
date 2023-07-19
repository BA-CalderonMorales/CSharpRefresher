using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.SolidPrinciples.OpenClosePrinciple
{
    public class MessageService
    {
        private string _message { get; set; }
        public string Message { get; set; } = "Hello world!";
        public MessageService() { }
        public void LogMessage(string message = "Goodbye world!")
        {
            if (message != null && message != "Goodbye world!")
            {
                Message = message;
            }
            Console.WriteLine(Message);
        }
    }
}
