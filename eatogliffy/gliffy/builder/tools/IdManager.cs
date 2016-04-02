using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eatogliffy.gliffy.builder.tools
{
    public class IdManager
    {
        private static int idCounter = 0;
        private static Dictionary<string, int> keyStore = new Dictionary<string, int>();

        public static int Counter
        {
            get
            {
                return idCounter;
            }
        } 

        public static int GetNextId(string eaId)
        {
            if(keyStore.ContainsKey(eaId))
            {
                return keyStore[eaId];
            }
            else
            {
                int val = ++idCounter;
                keyStore.Add(eaId, val);
                return val;
            }
        }
    }
}
