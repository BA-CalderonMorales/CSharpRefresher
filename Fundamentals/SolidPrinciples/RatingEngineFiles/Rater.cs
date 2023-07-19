using Fundamentals.SolidPrinciples.SingleResponsibilityPrinciple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.SolidPrinciples.RatingEngineFiles
{
    public class Rater
    {
        public RatingEngine Engine { get; set; }
        public ConsoleLogger Logger { get; set; }

        public Rater(RatingEngine engine, ConsoleLogger logger)
        {
            Engine = engine;
            Logger = logger;
        }

        public virtual void Rate(Policy policy)
        {
            Logger.Log("Uknown policy type.");
        }
    }
}
