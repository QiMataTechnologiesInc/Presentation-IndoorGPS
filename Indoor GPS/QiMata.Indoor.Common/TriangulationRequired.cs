using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QiMata.Indoor.Common
{
    public class TriangulationRequired
    {
        public List<InitialDistance> InitialDistances { get; set; }

        public Guid CashRegisterGuid { get; set; }


        public class InitialDistance
        {
            public Guid Beacon1 { get; set; }
            public Guid Beacon2 { get; set; }
            
            public int Distance { get; set; }
        }
    }
}
