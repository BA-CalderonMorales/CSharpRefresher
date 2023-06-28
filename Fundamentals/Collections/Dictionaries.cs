namespace Fundamentals.Collections
{
    public class Dictionaries
    {
        public static void Lesson()
        {
            InternalLesson1();
        }

        public static void InternalLesson1()
        {
            Dictionary<string, string> superHeroes = new Dictionary<string, string>();

            superHeroes.Add("Clark Kent", "Superman");
            superHeroes.Add("Bruce Wayne", "Batman");
            superHeroes.Add("Barry Allen", "The Flash");

            superHeroes.Remove("Barry Allen");

            Console.WriteLine("Count : {0}", superHeroes.Count);

            Console.WriteLine("Clark Kent : {0}",
                superHeroes.ContainsKey("Clark Kent"));

            superHeroes.TryGetValue("Clark Kent", out string test);
            Console.WriteLine($"Clark Kent : {test}");

            foreach (KeyValuePair<string, string> hero in superHeroes)
            {
                Console.WriteLine("{0} : {1}",
                    hero.Key, hero.Value);
            }
            superHeroes.Clear();
        }
    }
}
