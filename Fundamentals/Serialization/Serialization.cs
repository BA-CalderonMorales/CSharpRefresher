using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Fundamentals.Serialization
{
    public class Serialization
    {
        public static void Lesson()
        {
            // InternalLesson1();
            // InternalLesson2();
            InternalLesson3();
        }

        private static void InternalLesson1()
        {
            Animal bowser = new Animal("Bowser", 45, 25, 1);
            Stream stream = File.Open("AnimalData.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, bowser);
            stream.Close();

            bowser = null; // to delete our data

            stream = File.Open("AnimalData.dat", FileMode.Open);
            bf = new BinaryFormatter();

            bowser = (Animal)bf.Deserialize(stream);
            stream.Close();

            Console.WriteLine(bowser.ToString());
        }

        private static void InternalLesson2()
        {
            Animal bowser = new Animal("Bowser", 45, 25, 1);
            Stream stream = File.Open("AnimalData.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, bowser);
            stream.Close();

            bowser = null; // to delete our data

            stream = File.Open("AnimalData.dat", FileMode.Open);
            bf = new BinaryFormatter();

            bowser = (Animal)bf.Deserialize(stream);
            stream.Close();

            bowser.Weight = 500;

            Console.WriteLine(bowser.ToString());
            XmlSerializer serializer = new XmlSerializer(typeof(Animal));

            using (TextWriter tw = new StreamWriter(@"C:\Users\bacm6\source\repos\Fundamentals\bowser.xml"))
            {
                serializer.Serialize(tw, bowser);
            }

            bowser = null;

            XmlSerializer deserializer = new XmlSerializer(typeof(Animal));
            TextReader reader = new StreamReader(@"C:\Users\bacm6\source\repos\Fundamentals\bowser.xml");
            object obj = deserializer.Deserialize(reader);
            bowser = (Animal)obj;
            reader.Close();

            Console.WriteLine(bowser.ToString());
        }

        private static void InternalLesson3()
        {
            List<Animal> theAnimals = new List<Animal>
            {
                new Animal("Mario", 60, 30, 2),
                new Animal("Luigi", 55, 24, 1),
                new Animal("Peach", 40, 20, 4)
            };

            using (Stream fs = new FileStream(@"C:\Users\bacm6\source\repos\Fundamentals\animals.xml",
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Animal>));
                serializer.Serialize(fs, theAnimals);
            };
            theAnimals = null;

            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Animal>));

            using (Stream fs2 = File.OpenRead(@"C:\Users\bacm6\source\repos\Fundamentals\animals.xml"))
            {
                theAnimals = (List<Animal>)serializer2.Deserialize(fs2);
            };
            foreach (Animal a in theAnimals)
            {
                Console.WriteLine(a.ToString());
            }
        }
    }
}
