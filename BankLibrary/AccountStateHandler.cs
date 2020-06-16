using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    // Используется для создания событий
    public delegate void AccountStateHandler(object sender, AccountEvenArgs e);
    
    public class AccountEvenArgs
    {

        //Message
        public string Message { get; private set; }

        // Сумма, на которую изменился счет
        public decimal Sum { get; private set; }

        public AccountEvenArgs(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
