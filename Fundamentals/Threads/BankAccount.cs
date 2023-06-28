namespace Fundamentals.Threads
{
    class BankAccount
    {
        public Object acctLock = new object();
        double Balance { get; set; }
        string Name { get; set; }
        public BankAccount(double bal)
        {
            Balance = bal;
        }

        public double Withdrawal(double amt)
        {
            if ((Balance - amt) < 0)
            {
                Console.WriteLine($"Sorry ${Balance} in Account");
                return Balance;
            }

            lock (acctLock)
            {
                if (Balance >= amt)
                {
                    Console.WriteLine("Removed {0} and {1} left in account",
                        amt, (Balance - amt));
                    Balance -= amt;
                }
                return Balance;
            }
        }

        public void IssueWithdraw()
        {
            Withdrawal(1);
        }
    }
}
