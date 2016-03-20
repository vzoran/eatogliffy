using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder
{
    class IdManager
    {
        static int idCounter = 0;

        public static int Counter
        {
            get
            {
                return idCounter;
            }
        } 

        public static int GetNextId()
        {
            return idCounter++;
        }

    }
}
