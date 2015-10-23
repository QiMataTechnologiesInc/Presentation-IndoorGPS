using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QiMata.Indoor.Common
{
    public struct Coordinate
    {
        public bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Coordinate && Equals((Coordinate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }

        public decimal X { get; set; }

        public decimal Y { get; set; }
    }
}
