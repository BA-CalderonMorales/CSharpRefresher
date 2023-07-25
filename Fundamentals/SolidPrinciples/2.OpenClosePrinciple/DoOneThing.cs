using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.SolidPrinciples.OpenClosePrinciple
{
    public class DoOneThing
    {
        private readonly MessageService _messageService;

        public DoOneThing() { }

        public DoOneThing(MessageService messageService)
            => _messageService = messageService;

        public void ExecuteExtremelyConcrete()
        {
            Console.WriteLine("Hello world!");
        }

        public void ExecuteParameterBasedExtension(string message)
        {
            Console.WriteLine(message);
        }

        public virtual void ExecuteInheritanceBasedExtension()
        {
            Console.WriteLine("Hello world!");
        }

        public void ExecuteCompositionInjectionExtension()
        {
            _messageService.LogMessage();
        }
    }
    
    public class DoAnotherThing : DoOneThing
    {
        public DoAnotherThing() { }

        public DoAnotherThing(MessageService messageService) : base(messageService) { }

        public override void ExecuteInheritanceBasedExtension()
        {
            Console.WriteLine("Goodbye world!");
        }
    }
}
