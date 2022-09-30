using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pixir.Not.Control
{
    public static class Extensiones
    {
        public static bool getCurpRegularExpression(this string s)
        {
            Regex regex = new Regex(@"^[A - Z]{ 1 }[AEIOU]{ 1}
            [A-Z]{ 2}
            [0 - 9]{ 2} (0[1 - 9] | 1[0 - 2])(0[1 - 9] | 1[0 - 9] | 2[0 - 9] | 3[0 - 1])
            [HM]{ 1}
(AS | BC | BS | CC | CS | CH | CL | CM | DF | DG | GT | GR | HG | JC | MC | MN | MS | NT | NL | OC | PL | QT | QR | SP | SL | SR | TC | TS | TL | VZ | YN | ZS | NE)
[B - DF - HJ - NP - TV - Z]{ 3}
            [0 - 9A - Z]{ 1}
            [0 - 9]{ 1}$");
            return regex.IsMatch(s);
        }
    }
}
