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

    class Ion
    {
        public string abbr;
        public string name;
        public int charge;
        public string ionType;



        public Ion()
        {
            this.abbr = "";
            this.name = "";
            this.charge = 0;
            this.ionType = "";

        }

        public Ion(string abbr, string name, int charge, string ionType)
        {
            this.abbr = abbr;
            this.name = name;
            this.charge = charge;
            this.ionType = ionType;

        }
    }
}
