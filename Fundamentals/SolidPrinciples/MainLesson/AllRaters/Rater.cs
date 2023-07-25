using Fundamentals.SolidPrinciples.MainLesson.Interfaces;
using Fundamentals.SolidPrinciples.MainLesson.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.SolidPrinciples.MainLesson.AllRaters
{
    public abstract class Rater
    {
        public ILogger Logger { get; set; } = new ConsoleLogger();
        protected readonly IRatingUpdate RatingUpdate;

        public Rater(IRatingUpdate ratingUpdate)
        {
            RatingUpdate = ratingUpdate;
        }

        public abstract void Rate(Policy policy);
    }
}
