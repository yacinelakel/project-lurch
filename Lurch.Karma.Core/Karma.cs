using System;
using System.Collections.Generic;

namespace Lurch.Karma.Core
{
    // src : https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects#how-to-persist-value-objects-in-the-database-with-ef-core-20
    public class Karma : ValueObject
    {
        public readonly int Amount;

        public Karma()
        {
            Amount = 0;
        }

        public Karma(int amount)
        {
            Amount = amount;
        }

        public Karma Add(int value)
        {
            return new Karma(Amount + value);
        }

        public bool Equals(Karma other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
        }

        public static Karma operator +(Karma left, Karma right)
        {
            return new Karma(left.Amount + right.Amount);
        }

        public static Karma operator +(Karma left, int right)
        {
            return new Karma(left.Amount + right);
        }

        public static Karma operator +(int left, Karma right)
        {
            return right + left;
        }

        public static bool operator ==(Karma left, Karma right)
        {
            return EqualOperator(left, right);
        }


        public static bool operator !=(Karma left, Karma right)
        {
            return !EqualOperator(left, right);
        }
    }
}