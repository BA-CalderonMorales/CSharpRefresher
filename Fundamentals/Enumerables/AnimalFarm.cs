using System.Collections;

namespace Fundamentals.Enumerables
{
    public class AnimalFarm : IEnumerable
    {
        private List<Animal> animalList = new List<Animal>(); 

        public AnimalFarm(List<Animal> animalList)
        {
            this.animalList = animalList;
        }

        public AnimalFarm() { }

        public Animal this[int index]
        {
            get { return (Animal)this.animalList[index]; }
            set { this.animalList.Insert(index, value); }
        }

        public int Count
        {
            get
            {
                return this.animalList.Count;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.animalList.GetEnumerator();
        }
    }
}
