namespace Fundamentals.Game
{
    class Game
    {
        public static void Lesson()
        {
            InternalLesson1();
        }

        public static void InternalLesson1()
        {
            Warrior thor = new Warrior("Thor", 100, 26, 10);
            MagicWarrior loki = new MagicWarrior("Loki", 75, 20, 10, 50);

            Battle.StartFight(thor, loki); // should return some sort of result...
        }
    }
}
/*
    Thor attacks Hulk and deals 74 damage 
        - Maximus has 69 health

    Hulk attacks Thor and deals 6 damage
        - Bob has 6 health

    Thor attacks Hulk and deals 48 damage
        - Maximus has 21 health

    Hulk attacks Thor and deals 48 damage
        - Bob has -42 health

    Thor has died and Hulk is victorious

    Game over.
*/
