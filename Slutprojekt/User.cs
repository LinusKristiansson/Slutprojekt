using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    //Abstract för att man inte ska kunna instansiera klassen.
    abstract class User
    {
        private string name;
        public User(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get { return this.name; }
        }
    }
}
