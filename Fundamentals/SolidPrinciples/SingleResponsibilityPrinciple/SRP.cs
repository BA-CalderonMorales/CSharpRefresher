using Fundamentals.SolidPrinciples.RatingEngineFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.SolidPrinciples.SingleResponsibilityPrinciple
{
    public class SRP
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

        #region SRP - Single Responsibility Principle
        /**
         * Robert C. Martin (Uncle Bob).
         * 
         * - Each software module should have on and only one reason to change
         * - Module: class or single function
         * - Reason to change: ?
         * 
         * The individual classes o methods in our applications define what the application
         * does and how it does it.
         * 
         * Classes should encapsulate doing a particular task in a particular way.
         * 
         * Multipurpose tools don't perform as well as dedicated tools.
         * Dedicated tools are easier to use.
         * A problem with one part of a multipurpose tool can impact all parts.
         * 
         * What is a responsibility?
         * 
         * - Answer to the question of how something is done.
         * - e.g. persistence, logging, validation, business logic
         * 
         * SRP suggests that modules should have only one reason to change.
         * - e.g. validation rules or the way validation is performed may need to be updated in the futuer.
         * 
         * Responsibilities change at different times for different reasons.
         * Each one is an axis of change.
         * 
         * Axes of Change:
         * - e.g. CIO may require a change to persistence bc of company-wide change to metrics
         * - e.g. CSO may require a more robust logging framework to help detect attackers.
         * - e.g. COO may require an update to validation rules.
         * 
         * Tight coupling
         * - Binds two (or more) details together in a way that's difficult to change.
         * 
         * Loose coupling
         * - Offers a modular way to choose which details are involved in a particular operation.
         * 
         * Separation of concerns
         * - Programs should be separated into distinct sections, each addressing a separate concern,
         * or set of information that affects the program. Specific implementation of high level code
         * can be thought of as the low level "plumbing code". 
         * - Keep plumbing code separate from high level business logic.
         * - Extreme example:
         * A refridgerator containing food/items that shouldn't be stored in a fridge.
         * for example, meat can be stored in the fridge, but raid bug spray should probably be stored
         * in the basement (lol).
         * Yet, the fridge remains a concept of storing food, where the basement can remain a concept
         * of storing things that need to be stored at a cool temperature.
         * 
         * Class elements that belong together are cohesive.
         * - Classes that have many responsibilities, tend to have less cohesion.
         * - e.g.
         * 
         *                                      class
         *         |-------------------------------|-----------------------------------|
         *         |                               |                                   |
         *      Field A                         Field B                             Field C
         *         |                               |                                   |
         *         |-------------------------------|-----------------------------------|
         *                         |               |
         *                      Method 1        Method 2
         *                                         |
         *                                      Method 3
         *                                      (private)
         *                                      
         *  This class is not very cohesive because its methods don't share much data or
         *  behavior with one another.
         *  
         *  - Refactoring with SRP (single responsibility principle) in mind:
         *  
         *                      class 1                                         class 2
         *                         |                                               |
         *      Field A------------|--------------Field C                       Field B
         *        |                                 |                              | 
         *        |------------=Method 1-------------                           Method 2
         *                                                                         |
         *                                                                      Method 3
         *                                                                      (private)
         * Example:
         * An insurance rating service
         */
        public static void Lesson(bool goodOrBadSrpFlag = true)
        {
            Console.WriteLine("Ardalis Insurance Rating System Starting...");
            var engine = new RatingEngine();
            engine.Rate(goodOrBadSrpFlag); // disregards srp
        }
        #endregion

        #region Key Takeaways
        /**
         * Practice Pain Driven Development
         * Each class should have a single responsibility, or reason to change
         * Strive for high cohesion and loose coupling
         * Keep classes small, focused, and testable
         */
        #endregion
    }
}
