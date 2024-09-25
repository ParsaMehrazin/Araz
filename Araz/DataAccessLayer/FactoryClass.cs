using System;

namespace Models
{
    class FactoryClass
    {
        static void Main(string[] args)
        {
            var factory = new SavingAcctFactory() as ICredit;
            var citi = factory.GetSave("ali");
            Console.WriteLine(citi);
        }

    }

    public abstract class ISavingAccount
    {
        public decimal Balance { get; set; }
    }

    public class CitiSavingAcct : ISavingAccount
    {
        public CitiSavingAcct()
        {
            Balance = 5000;
        }
    }

    public class NationalSavingAcct : ISavingAccount
    {
        public NationalSavingAcct()
        {
            Balance = 4000;
        }
    }

    interface ICredit
    {
        ISavingAccount GetSave(string No);
    }

    public class SavingAcctFactory : ICredit
    {
        public ISavingAccount GetSave(string No)
        {
            if (No.Contains("a"))
            {
                return new CitiSavingAcct();
            }
            else
            {
                return new NationalSavingAcct();

            }
        }
    }

}
