using System.Collections;
using System.Net.Http.Headers;

namespace Fundamentals.LINQ
{
    public class LINQ
    {
        public static void Lesson()
        {
            InternalLesson1();
            InternalLesson2();
            InternalLesson3();
            InternalLesson4();
            InternalLesson5();
            InternalLesson6();
            InternalLesson7();
        }

        private static void InternalLesson1()
        {
            string[] dogs =
            {
                "K 9",
                "Brian Griffin",
                "Scooby Doo",
                "Old Yellow",
                "Rin Tin Tin",
                "Benji",
                "Charlie B. Barkin",
                "Lassie",
                "Snoopy"
            };

            var dogSpaces = from dog in dogs
                            where dog.Contains(" ")
                            orderby dog descending
                            select dog;

            foreach (string dog in dogSpaces)
            {
                Console.WriteLine(dog);
            }
        }

        public static int[] QueryIntArray()
        {
            int[] nums = { 5, 10, 15, 20, 25, 30, 35 };

            var gt20 = from num in nums
                       where num > 20
                       orderby num
                       select num;
            foreach (int num in gt20)
            {
                Console.WriteLine(num);
            }

            Console.WriteLine($"Get Type : {gt20.GetType()}");

            var listGT20 = gt20.ToList<int>();
            var arrayGT20 = gt20.ToArray();

            nums[0] = 40;
            foreach (int num in gt20)
            {
                Console.WriteLine(num);
            }

            return arrayGT20;
        }

        private static void InternalLesson2()
        {
            int[] intArray = QueryIntArray();
            foreach (int num in intArray)
            {
                Console.WriteLine(num);
            }
        }

        private static void InternalLesson3()
        {
            ArrayList farmAnimals = new ArrayList()
            {
                new Animal{
                    Name = "Heidi",
                    Height = .8,
                    Weight  = 18
                },
                new Animal{
                    Name = "Shrek",
                    Height = 4,
                    Weight  = 130
                },
                new Animal{
                    Name = "Congo",
                    Height = 3.8,
                    Weight  = 90
                }
            };

            var farmAnimalsEnum = farmAnimals.OfType<Animal>();

            var smAnimals = from animal in farmAnimalsEnum
                            where animal.Weight <= 90
                            orderby animal.Name
                            select animal;
            foreach (var animal in smAnimals)
            {
                Console.WriteLine("{0} weights {1}lbs",
                    animal.Name, animal.Weight);
            }
        }

        private static void InternalLesson4()
        {
            ArrayList animalList = new ArrayList()
            {
                new Animal{
                    Name = "German Shephard",
                    Height = 25,
                    Weight  = 77
                },
                new Animal{
                    Name = "Chihuahua",
                    Height = 7,
                    Weight  = 4.4
                },
                new Animal{
                    Name = "Saint Bernard",
                    Height = 30,
                    Weight  = 200
                }
            };

            // must convert to enumerable prior to executing sql-like commands from LINQ library
            var animalListEnum = animalList.OfType<Animal>();

            var bigDogs = from dog in animalListEnum
                          where (dog.Weight > 70) &&
                          (dog.Height > 25)
                          orderby dog.Name
                          select dog;
                
            foreach (var animal in bigDogs)
            {
                Console.WriteLine("{0} weights {1}lbs",
                    animal.Name, animal.Weight);
            }
        }
        private static void InternalLesson5()
        {
            ArrayList animalList = new ArrayList()
            {
                new Animal{
                    Name = "German Shephard",
                    Height = 25,
                    Weight  = 77,
                    AnimalID = 1
                },
                new Animal{
                    Name = "Chihuahua",
                    Height = 7,
                    Weight  = 4.4,
                    AnimalID = 2 
                },
                new Animal{
                    Name = "Saint Bernard",
                    Height = 30,
                    Weight  = 200,
                    AnimalID = 3 
                },
                new Animal{
                    Name = "Pug",
                    Height = 12,
                    Weight  = 16,
                    AnimalID = 1 
                },
                new Animal{
                    Name = "Beagle",
                    Height = 15,
                    Weight  = 23,
                    AnimalID = 2 
                }
            };

            Owner[] owners = new[]
            {
                new Owner
                {
                    Name = "Doug Parks",
                    OwnerID = 1
                },
                new Owner
                {
                    Name = "Sally Smith",
                    OwnerID = 2
                },
                new Owner
                {
                    Name = "Paul Brooks",
                    OwnerID = 3
                }
            };


            // must convert to enumerable prior to executing sql-like commands from LINQ library
            var animalListEnum = animalList.OfType<Animal>();

            var nameHeight = from a in animalListEnum
                             select new
                             {
                                 a.Name,
                                 a.Height
                             };

            foreach (var nh in nameHeight)
            {
                Console.WriteLine("{0} has a height of {1} inches",
                    nh.Name, nh.Height);
            }

            Array arrNameHeight = nameHeight.ToArray();
            foreach (var i in arrNameHeight)
            {
                Console.WriteLine(i.ToString());
            }
        }

        private static void InternalLesson6()
        {
            Animal[] animals = new[]
            {
                new Animal{
                    Name = "German Shephard",
                    Height = 25,
                    Weight  = 77,
                    AnimalID = 1
                },
                new Animal{
                    Name = "Chihuahua",
                    Height = 7,
                    Weight  = 4.4,
                    AnimalID = 2 
                },
                new Animal{
                    Name = "Saint Bernard",
                    Height = 30,
                    Weight  = 200,
                    AnimalID = 3 
                },
                new Animal{
                    Name = "Pug",
                    Height = 12,
                    Weight  = 16,
                    AnimalID = 1 
                },
                new Animal{
                    Name = "Beagle",
                    Height = 15,
                    Weight  = 23,
                    AnimalID = 2 
                }
            };

            Owner[] owners = new[]
            {
                new Owner
                {
                    Name = "Doug Parks",
                    OwnerID = 1
                },
                new Owner
                {
                    Name = "Sally Smith",
                    OwnerID = 2
                },
                new Owner
                {
                    Name = "Paul Brooks",
                    OwnerID = 3
                }
            };

            var innerjoin =
                from animal in animals
                join owner in owners on animal.AnimalID
                equals owner.OwnerID
                select new
                {
                    OwnerName = owner.Name,
                    AnimalName = animal.Name
                };

            foreach (var i in innerjoin)
            {
                Console.WriteLine("{0} owns {1}",
                    i.OwnerName, i.AnimalName);
            }
        }
        private static void InternalLesson7()
        {
            Animal[] animals = new[]
            {
                new Animal{
                    Name = "German Shephard",
                    Height = 25,
                    Weight  = 77,
                    AnimalID = 1
                },
                new Animal{
                    Name = "Chihuahua",
                    Height = 7,
                    Weight  = 4.4,
                    AnimalID = 2 
                },
                new Animal{
                    Name = "Saint Bernard",
                    Height = 30,
                    Weight  = 200,
                    AnimalID = 3 
                },
                new Animal{
                    Name = "Pug",
                    Height = 12,
                    Weight  = 16,
                    AnimalID = 1 
                },
                new Animal{
                    Name = "Beagle",
                    Height = 15,
                    Weight  = 23,
                    AnimalID = 2 
                }
            };

            Owner[] owners = new[]
            {
                new Owner
                {
                    Name = "Doug Parks",
                    OwnerID = 1
                },
                new Owner
                {
                    Name = "Sally Smith",
                    OwnerID = 2
                },
                new Owner
                {
                    Name = "Paul Brooks",
                    OwnerID = 3
                }
            };

            var groupJoin =
                from owner in owners
                orderby owner.OwnerID
                join animal in animals on owner.OwnerID
                equals animal.AnimalID into ownerGroup
                select new
                {
                    Owner = owner.Name,
                    Animals = from owner2 in ownerGroup
                              orderby owner2.Name
                              select owner2
                };

            int totalAnimals = 0;

            foreach (var ownerGroup in groupJoin)
            {
                Console.WriteLine(ownerGroup.Owner);
                foreach (var animal in ownerGroup.Animals)
                {
                    totalAnimals++;
                    Console.WriteLine("* {0}", animal.Name);
                }
            }
        }
    }
}
