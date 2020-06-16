using System;

namespace BankLibrary
{
    public interface IAccount
    {
        // Положить деньги на счет
        void Put(decimal sum);
        // Взять деньги со счета
        decimal Withdraw(decimal sum);
    }
}
