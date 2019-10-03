using System;
using System.Collections.Generic;
using System.Text;

namespace Lurch.Karma.Core
{
    public class User : Entity
    {
        public User(int id)
        {
            Id = id;
            Karma = new Karma();
        }

        // EF
        protected User()
        {
        }

        public User(int id, int karmaAmount)
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
