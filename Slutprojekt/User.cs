using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //Klass för användare.
    class User
    {
        private int balance;
        private string name;

        public User(int balance, string name)
        {
            this.balance = balance;
            this.name = name;
        }
        //En constructor för användare utifrån de angivna parametrarna.

        public int Balance
        {
            set { this.balance = value; }
            get { return this.balance; }
        }
        //En property med en get och set för användarens pengar.

        public string Name
        {
            get { return this.name; }
        }
        //En property med en get för användarens namn.
    }
}

