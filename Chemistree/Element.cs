using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using System.Data;

namespace Chemistree_GUI_V1
{

    class Element
    {
        public string abbr;
        public string name;
        public string atomicNumber;
        public string periodicGroup;
        public string periodicPeriod;
        public string electronConfiguration;

        public Element()
        {
            this.abbr = "";
            this.name = "";
            this.atomicNumber = "";
            this.periodicGroup = "";
            this.periodicPeriod = "";
            this.electronConfiguration = "";
        }

        public Element(string abbr, string name, string atomicNumber, string periodicGroup, string periodicPeriod, string electronConfiguration)
        {
            this.abbr = abbr;
            this.name = name;
            this.atomicNumber = atomicNumber;
            this.periodicGroup = periodicGroup;
            this.periodicPeriod = periodicPeriod;
            this.electronConfiguration = electronConfiguration;
        }
    }
}
