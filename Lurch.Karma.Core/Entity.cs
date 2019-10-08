﻿using System;

namespace Lurch.Karma.Core
{
    // src: https://github.com/dotnet-architecture/eShopOnContainers/blob/1b7200791931f33c94206822a69644ca820bb0dc/src/Services/Ordering/Ordering.Domain/SeedWork/Entity.cs
    public abstract class Entity
    {
        int? _requestedHashCode;
        int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            private set
            {
                _Id = value;
            }
        }

        protected Entity()
        {

        }

        protected Entity(int id)
        {
            Id = id;
        }


        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}