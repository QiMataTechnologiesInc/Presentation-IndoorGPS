using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QiMata.Indoor.Common
{
    public class Beacon
    {
        public Guid Uuid { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }



        protected bool Equals(Beacon other)
        {
            return Uuid.Equals(other.Uuid) && Major == other.Major && Minor == other.Minor && Coordinate.Equals(other.Coordinate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Beacon) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Uuid.GetHashCode();
                hashCode = (hashCode*397) ^ Major;
                hashCode = (hashCode*397) ^ Minor;
                hashCode = (hashCode*397) ^ Coordinate.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator !=(Beacon beacon1, Beacon beacon2)
        {
            return !beacon1.Equals(beacon2);
        }

        public static bool operator ==(Beacon beacon1, Beacon beacon2)
        {
            return beacon1.Equals(beacon2);
        }



        public Coordinate Coordinate { get; set; }
    }
}
