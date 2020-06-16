using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        // Событие возникающие про выводе денег
        protected internal event AccountStateHandler Withdrawed;
        // Событие возникающие при добавление на счет 
        protected internal event AccountStateHandler Added;
        // Событие возникающая при открытие счета
        protected internal event AccountStateHandler Opened;
        // Событие возикающая при закрытие счета
        protected internal event AccountStateHandler Closed;
        // Событие возникающая при начисление процентов
        protected internal event AccountStateHandler Calculated;

        static int counter = 0;
        protected int _days = 0; // время с открытия счета

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }

        // текущая сумма на счету
        public decimal Sum { get; private set; }
        // процент начислений
        public int Percentage { get; private set; }
        // уникальный индетификатор счета 
        public int Id { get; private set; }

        // вызов событий
        private void CallEvent(AccountEvenArgs e, AccountStateHandler handler)
        {
            if (e != null)
            {
                handler?.Invoke(this, e);
            }
        }
         // вызов отделных событий. Для каждого события определяется свой виртуальный метод
        protected virtual void OnOpened(AccountEvenArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEvenArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        
        protected virtual void OnAdded(AccountEvenArgs e)
        {
            CallEvent(e, Added);
        }

        protected virtual void OnClosed(AccountEvenArgs e)
        {
            CallEvent(e, Closed);
        }

        protected virtual void OnCalculated(AccountEvenArgs e)
        {
            CallEvent(e, Calculated);
        }

        //зачисление средств на счет
        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEvenArgs("На счет поступило " + sum, sum));
        }

        // метод сняти я со счета, возвращается сколько снято со счета
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEvenArgs($"Сумма {sum} снята со счета {Id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEvenArgs($"Недостаточно денег на счете {Id}", 0));
            }
            return result;
        }
        // открытие счета
        protected internal virtual void Open ()
        {
            OnOpened(new AccountEvenArgs($"Открыт новый счет! Id счета: {Id}", Sum));
        }

        // закрытие счета
        protected internal virtual void Close()
        {
            OnClosed(new AccountEvenArgs($"Счет {Id} закрыт. Итоговая сумма: {Sum}", Sum));
        }

        protected internal virtual void IncrementDays()
        {
            _days++;
        }
        // начисление процентов
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEvenArgs($"Начислены проценты в размере: {increment}", increment));
        }
    }
}
