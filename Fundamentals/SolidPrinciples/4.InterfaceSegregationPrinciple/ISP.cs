using Fundamentals.SolidPrinciples.MainLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.SolidPrinciples.InterfaceSegregationPrinciple
{
    public class ISP
    {
        #region Define SOLID
        /**
         * SRP - Single Responsibility Principle
         * OCP - Open Closed Principle
         * LSP - Liskov Substitution Principle
         * ISP - Interface Segregation Principle
         * DIP - Dependency Inversion Principle
         * 
         * TDD - Test Driven Development
         * DDD - Domain Driven Development
         * PDD - Pain Driven Development (lol)
         * 
         * PDD:
         * - Avoid premature optimization
         * - If current design is painful to work with,
         *   use principles to guide redesign
         */
        #endregion

        #region ISP - Interface Segregation Principle
        /**
         * Clients should not be forced to depend on methods they do not use
         * 
         * Prefer small, cohesive interfaces to large, "fat" ones.
         * 
         * What does "interface" mean in "ISP"? 
         * 
         * In C#, the interface type/keyword
         * Public (or accessible) interface of a class
         * 
         * What is a Client?
         * 
         * In this context, the client is the code that is interacting with an
         * instance of the interface. It's the calling code.
         * 
         * What's the problem with large interfaces?
         * 
         * well, they force you to implement methods.
         * 
         * 
         * What if all your code needs is to log you in? Should you 
         * have to implement a LARGE interface?
         * 
         * Violating ISP results in classes that depend on things they don't
         * need.
         * 
         * 
         * What if you want to implement your own custom Pluralsight login
         * provider?
         * 
         * If you're using the MembershipProvider base class, you'll need
         * to implement ALL the methods of a large interface.
         * 
         * More dependencies means:
         * 
         * - More coupling
         * - More brittle code
         * - More difficult testing
         * - More difficult deployments
         * 
         * Detecting ISP Violations in your own code.
         * 
         * - Large interfaces
         * - NotImplementedException
         * - Code uses just a small subset of a larger interface
         * 
         * 
         * A poorly-designed interface, only two methods, lacks cohesion
         * 
         * public interface INotificationService
         * {
         *      void SentText(string SmsNumber, string message);
         *      
         *      void SendEmail(string to, string from, string subj, string body);
         * }
         * 
         * For example:
         * 
         * public class SmtpNotificationService : INotificationService
         * {
         *      public void SendEmail(string to, string from, string subj, string body)
         *      {
         *          // actually send email with logic here
         *      }
         *      
         *      public void SendText(string SmsNumber, string message)
         *      {
         *          throw new NotImplementedException();
         *      }
         * }
         * 
         * 
         * SPLIT IT UP
         * 
         * public interface IEmailNotificationService
         * {
         *      void SendEmail(string to, string from, string subj, string body);
         * }
         * 
         * public interface ITextNotificationService
         * {
         *      void SendText(string SmsNumber, string message);
         * }
         * 
         * 
         * What about legacy code that's already coupled to the original interface?
         * 
         * - Multiple interface inheritance
         * 
         * public interface INotificationService :
         *                              IEmailNotificationService,
         *                              ITextNotificationService
         * {
         * }
         * 
         * ISP is related to LSP, Cohesion/SRP, and Pain Driven Development
         * 
         * 
         * Fixing ISP Violations
         * - Break up large interfaces into smaller ones
         *      - Compose fat interfaces from smaller ones for backward compatibility
         * - To address large interfaces you don't control
         *      - Create a small, cohesive interface
         *      - Use the Adapter design pattern so your code can work with the Adapter
         * - Clients should own and define their interfaces
         * 
         * 
         * Where do interfaces live in our apps?
         * - Client code should define and own the interfaces it uses
         * - Interfaces should be declared where both client code and implementatinos can
         * access it.
         * 
         */
        #endregion
        public static void Lesson(bool solidOrNo = true)
        {
            Console.WriteLine("Ardalis Insurance Rating System Starting...");
            var engine = new RatingEngine();
            engine.Rate(solidOrNo);
        }
    }
}
