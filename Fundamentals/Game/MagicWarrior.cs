namespace Fundamentals.Game
{
    class MagicWarrior : Warrior
    {
        int teleportChance = 0;
        CanTeleport teleportType = new CanTeleport();

        public MagicWarrior(string name = "Warrior",
            double health = 0,
            double attackMax = 0,
            double blockMax = 0,
            int teleportChance = 0) : base(name, health, attackMax, blockMax)
        {
            this.teleportChance = teleportChance;
        }

        public override double Block()
        {
            Random rnd = new Random();
            int randomDodge = rnd.Next(1, 100);

            if (randomDodge < this.teleportChance)
            {
                Console.WriteLine($"{Name} {teleportType.Teleport()}\n");
                return 10000;
            }
            else
            {
                return base.Block();
            }
        }
    }
}
