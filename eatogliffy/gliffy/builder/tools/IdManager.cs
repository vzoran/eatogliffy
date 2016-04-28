using EA;
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
        private static Repository eaRepository;

        public static void Initialize(Repository repository)
        {
            eaRepository = repository;
        }

        public static int Counter
        {
            get
            {
                return idCounter;
            }
        } 

        public static int GetId(string eaId)
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

        public static int GetIdByIndex(int index)
        {
            return GetId(eaRepository.GetElementByID(index).ElementGUID);
        }
    }
}
