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
    class SmallNumber
    {
        public SmallNumber() {
        }

        //
        // Converts to subscript or superscript using unicode values.
        //
        public string convertToSubscript(int n)
        {
            string subscript;

            switch (n)
            {
                case 0:
                    subscript = "\u2080";
                    break;
                case 1:
                    subscript = "\u2081";
                    break;
                case 2:
                    subscript = "\u2082";
                    break;
                case 3:
                    subscript = "\u2083";
                    break;
                case 4:
                    subscript = "\u2084";
                    break;
                case 5:
                    subscript = "\u2085";
                    break;
                case 6:
                    subscript = "\u2086";
                    break;
                case 7:
                    subscript = "\u2087";
                    break;
                case 8:
                    subscript = "\u2088";
                    break;
                case 9:
                    subscript = "\u2089";
                    break;
                default:
                    subscript = "";
                    break;
            }
            return subscript;
        }
        public string convertToSuperscript(int n)
        {
            string superscript;

            switch (n)
            {
                case 0:
                    superscript = "\u2070";
                    break;
                case 1:
                    superscript = "\u00B9";
                    break;
                case 2:
                    superscript = "\u00B2";
                    break;
                case 3:
                    superscript = "\u00B3";
                    break;
                case 4:
                    superscript = "\u2074";
                    break;
                case 5:
                    superscript = "\u2075";
                    break;
                case 6:
                    superscript = "\u2076";
                    break;
                case 7:
                    superscript = "\u2077";
                    break;
                case 8:
                    superscript = "\u2078";
                    break;
                case 9:
                    superscript = "\u2079";
                    break;
                default:
                    superscript = "";
                    break;
            }
            return superscript;
        }

    }
}
