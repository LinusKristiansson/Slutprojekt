using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //BlackJackUser ärver från User.
    class BlackJackUser : User
    {
        private int balance;

        //Eftersom namnet lagras i basklassen, skickas namnet till basklassens constructor med hjälp av base(name)
        public BlackJackUser(int balance, string name) : base(name)
        {
            this.balance = balance;
        }
        public int Balance
        {
            set { this.balance = value; }
            get { return this.balance; }
        }
    }
}

