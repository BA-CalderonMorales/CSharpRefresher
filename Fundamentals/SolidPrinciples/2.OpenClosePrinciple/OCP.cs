using Fundamentals.SolidPrinciples.MainLesson;

namespace Fundamentals.SolidPrinciples.OpenClosePrinciple
{
    public class OCP
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

        #region OCP - Open/Closed Principle
        /**
         * Software entities (classes, modules, functions, etc.) should
         * be open for extension, but closed for modificiation.
         * 
         * - It should be possible to change the behavior of a method without
         * editing its source code.
         * 
         * Open to extension
         * - Easy to add new behavior
         * - Code that is closed to extension, has fixed behavior
         * 
         * Closed to modification
         * - Changes to source or binary code are not required.
         * - The only way to change the behavior of code that is
         * closed to extension is to change the code itself.
         * 
         * Why should code be closed to modification?
         * - less likely to introduce bugs in code we don't touch or redeploy
         * - less likely to break dependent code when we don't have to 
         * deploy updates
         * - Fewer conditionals in code that is open to extension results in
         * simpler code
         * - Bug fixes are ok.
         * 
         * Balance abstraction and concreteness
         * Abstraction adds complexity
         * Predict where variation is needed and apply abstraction as needed
         * 
         * Extremely Concrete:
         * 
         * public class DoOneThing
         * {
         *      public void Execute()
         *      {
         *          Console.WriteLine("Hello world!");
         *      }
         * }
         * 
         * public class DoSomethingElse
         * {
         *      public void SomethingElse()
         *      {
         *          var doThing = new DoOneThing(); // new is glue
         *          doThing.Execute();
         *          // other stuff...
         *      }
         * }
         * 
         * Extremely Extensible:
         * 
         * public clss DoAnything<TArg, TResult>
         * {
         *      private Func<TArg, TResult> _function;
         *      
         *      public DoAnything(Func<Targ, TResult> function)
         *      {
         *          _function = function;
         *      }
         *      
         *      public TResult Execute(TArg a)
         *      {
         *          return _function(a);
         *      }
         * }
         * 
         * public abstract class DoAnything<TResult, TArg>
         * {
         *      public abstract TResult Execute(TArg a);
         * }
         * 
         * How can you predict future changes?
         * 
         * - one approach, start concrete
         * - modify the code the first time or two
         * - by the third modification, consider making the code open to
         * extension, closed to modification
         * 
         * Typical approaches to OCP:
         * 
         * - parameters ( )
         * - inheritance FluffyDog is-a Dog
         * - composition/injection dependency injection
         * 
         * Prefer implementing new features in new classes.
         * - adds functionality without increasing complexity of existing
         * legacy code
         * 
         * Why use a new class?
         * - Design class to sit problem at hand
         * - Nothing in current system depends on it
         * - Can add behavior without touching existing code
         * - Can follow SRP
         * - Can be unit-tested (even if the rest of the app is a big ball of mud)
         * 
         * Packages and Libraries
         * - Closed for modification
         *      - Consumers cannot change package contents (dlls need to be recompiled to mod)
         *      - Should not break consumers when new behavior is added
         * - Open to extension
         *      - Consumers should be able to extend the package to suit their own needs
         *      
         * Key takeaways
         * - Solve the problem first using simple, concrete code
         * - Identify the kinds of changes the application is likely to continue needing
         * - Modify code to be extensible along the axis of change you've identified
         *      - Without the need to modify its source each time...
         */
        #endregion
        public static void Lesson(bool goodOrBadOcpFlag = true)
        {
            // ExtensionTypes();
            Console.WriteLine("Ardalis Insurance Rating System Starting...");

            var engine = new RatingEngine();
            engine.Rate(goodOrBadOcpFlag); // disregards srp
        }

        public static void ExtensionTypes()
        {
            DoOneThing doOneThing = new DoOneThing();
            doOneThing.ExecuteParameterBasedExtension("example");

            DoAnotherThing doAnotherThing = new DoAnotherThing();
            doAnotherThing.ExecuteInheritanceBasedExtension();

            DoOneThing doOneThingCI = new DoOneThing(new MessageService());
            doOneThingCI.ExecuteCompositionInjectionExtension();
        }
        #region Key Takeaways
        #endregion
    }
}
