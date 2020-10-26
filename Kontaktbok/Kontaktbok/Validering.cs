using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktbok
{
    class Validering
    {

        public Kontakt SättTypAv(ConsoleKeyInfo kontakt)
        {

            if (kontakt.Key == ConsoleKey.D1)
                return Kontakt.Vänner;
            else if (kontakt.Key == ConsoleKey.D2)
                return Kontakt.Familj;
            else if (kontakt.Key == ConsoleKey.D3)
                return Kontakt.Arbete;
            else if (kontakt.Key == ConsoleKey.D4)
                return Kontakt.Skola;
            else
                return Kontakt.Okänt;

        }

    }
}
