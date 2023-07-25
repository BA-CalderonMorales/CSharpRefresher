using Fundamentals.SolidPrinciples.MainLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.SolidPrinciples.LiskovSubstitutionPrinciple
{
    internal class LSP
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

        #region Liskov Substitution Principle
        /**
         * 
         * Let phi(x) be a property provable about objects x of type T.
         * Then phi(y) should be true for objects y of type S where S is a subtype of T.
         * 
         * "Subtyptes must be substitutable for their base types."
         * 
         * Basic object-oriented design:
         * 
         * Inheritance: Something IS-A something-else
         *      - An eagle IS-A bird
         * Properties: Something HAS-A property
         *      - An address HAS-A city
         * 
         * LSP states that the IS-A relationship is insufficient and should be replaced with:
         * IS-SUBSTITUTABLE-FOR
         * 
         * Classic Rectangle-square Problem:
         * - A rectangle has four sides and four right angles
         * - A square has four equal sides and four right angles
         * - Per geometry, a square IS-A rectangle.
         * 
         * Yet:
         * - Square has an invariant - compare Square.cs and Rectangle.cs
         *      - Its sides must be equal
         * - Rectangle has an invariant - compare Square.cs and Rectangle.cs
         *      - Its sides are independent
         * - This design breaks rectangle's invariant and thus violates LSP
         * 
         * Detecting LSP Violations in your code:
         * 
         * - Type checking with is or as in polymorphic code
         * - Null checks
         * - NotImplementedException
         * 
         * Fixing LSP Violations:
         * - Follow the "Tell, Don't Ask" principle
         * - Minimize null checks with:
         *      - C# Features
         *      - Guard Clauses
         *      - Null object design pattern
         * Follow LSP and be sure to fully implement interfaces
         *      - Don't leave any interface methods unimplemeneted
         * 
         * Tell, Don't Ask
         * 
         *          Data and logic separate
         *          
         * Print Report -> Is Manager? -> Print Employee || Print Manager
         *                      ASK
         *                      
         *          Data and logic together
         *          
         * Print Report -> Manager.Print()...Employee.Print()...X.Print()...
         *                              TELL
         *                              
         * Encapsulating logic is GOOD!
         * 
         * 
         * Key Takeaways:
         * 
         * - Subtypes must be substitutable for their base types (not just whether the code can compile)
         * - Ensure base type invariants are enforced
         * - Look for
         *      - Type checking ("is" or "as")
         *      - Null checking
         *      - NotImplemenentedException (not fully implenting interfaces)
         */
        #endregion
        public static void Lesson(bool goodOrBadOcpFlag = true)
        {
            // LSPViolationExamples();
            // mainly, look at key takeaways section above here
        }

        public static void LSPViolationExamples()
        {
            // TypeChecking();
            // NullChecking();
        }

        public static void TypeChecking()
        {
            /* Bad
            foreach (var employee in employees)
            {
                if (employee is Manager)
                {
                    Hepers.PrintManager(employee as Manager);
                    break;
                }
                Helpers.PrintEmployee(employee);
            }
            */
            
            /* Better
            foreach (var employee in employees)
            {
                employee.Print();
            }
            */

            /* Best
            foreach (var employee in employees)
            {
                Helpers.PrintEmployee(employee);
            }
            */
        }

        public static void NullChecking()
        {
            /* Bad
            foreach (var employee in employees)
            {
                if (employee == null)
                {
                    Console.WriteLine("Employee not found.");
                    break;
                }
                Helpers.PrintEmployee(employee);
            }
            */
        }
    }
}
