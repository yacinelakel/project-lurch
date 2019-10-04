using System;
using System.Collections.Generic;
using System.Text;

namespace Lurch.Karma.Core
{
    public class User : Entity
    {

        public static User Create(int karmaAmount = 0)
        {
            return new User(0, karmaAmount);
        }

        public User(int id, int karmaAmount) : base(id)
        {
            Karma = new Karma(karmaAmount);
        }

        public Karma Karma { get; private set; }

        public void AddKarma(int value)
        {
            Karma += value;
        }
    }
}
