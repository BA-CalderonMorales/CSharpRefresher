namespace Fundamentals.Game
{
    class Battle
    {
        public static void StartFight(Warrior war1, Warrior war2)
        {
            while (true)
            {
                if (GetAttackResult(war1, war2) == "Game Over")
                {
                    Console.WriteLine("Game Over");
                    break;
                } 

                if (GetAttackResult(war2, war1) == "Game Over")
                {
                    Console.WriteLine("Game Over");
                    break;
                } 
            }
        }

        public static string GetAttackResult(Warrior warAlpha, Warrior warBravo)
        {
            // Calculate the warrior's attack and block from the other..
            double warAlphaAtkAmt = warAlpha.Attack();
            double warBravoBlkAmt = warBravo.Block();

            double dmg2WarB = warAlphaAtkAmt - warBravoBlkAmt; 

            if (dmg2WarB > 0)
            {
                warBravo.Health = warBravo.Health - dmg2WarB;
            }
            else
            {
                dmg2WarB = 0;
            }
            Console.WriteLine("{0} Attacks {1} and deals {2} damage",
                warAlpha.Name, warBravo.Name, dmg2WarB);
            Console.WriteLine("\t- {0} has {1} health\n",
                warBravo.Name, warBravo.Health);

            if (warBravo.Health <= 0)
            {
                Console.WriteLine("{0} has died and {1} is victorious\n",
                    warBravo.Name, warAlpha.Name);
                return "Game Over";
            }
            return "Fight Again";
        }
    }
    // StartFight - what happens when a fight starts?
    // war1 attacks war2, war2 is damaged and health decreases
    // - get attack result
    // war2 attacks war1, war1 is damaged and health decreases
    // - get attack result
}
