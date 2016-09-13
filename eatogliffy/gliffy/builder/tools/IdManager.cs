using EA;
using System.Collections.Generic;

namespace EaToGliffy.Gliffy.Builder.Tools
{
    /// <summary>
    /// Global manager of Gliffy element IDs
    /// </summary>
    public static class IdManager
    {
        private static int idCounter = 0;
        private static Dictionary<string, int> keyStore = new Dictionary<string, int>();
        private static Repository eaRepository;

        /// <summary>
        /// Add reference of a repository. 
        /// </summary>
        /// <param name="repository">an EA repository</param>
        public static void Initialize(Repository repository)
        {
            eaRepository = repository;
        }

        /// <summary>
        /// Get the current counter value
        /// </summary>
        public static int Counter
        {
            get
            {
                return idCounter;
            }
        } 

        /// <summary>
        /// Create or get Gliffy ID of an EA element
        /// </summary>
        /// <param name="eaId">Unique ID of an EA element</param>
        /// <returns>ID used in Gliffy diagram to represent that element</returns>
        public static int GetId(string eaId)
        {
            if(keyStore.ContainsKey(eaId))
            {
                return keyStore[eaId];
            }
            else
            {
                int val = GetId();
                keyStore.Add(eaId, val);
                return val;
            }
        }

        /// <summary>
        /// Create and return the next applicable ID
        /// </summary>
        /// <returns>Next free ID</returns>
        public static int GetId()
        {
            return idCounter++;
        }

        /// <summary>
        /// Create or get Gliffy index of an EA element identified ny its index
        /// </summary>
        /// <param name="index">EA element index</param>
        /// <returns>ID representing the EA element in Gliffy</returns>
        public static int GetIdByIndex(int index)
        {
            return GetId(eaRepository.GetElementByID(index).ElementGUID);
        }

        /// <summary>
        /// Reset inner counter
        /// </summary>
        public static void Reset()
        {
            idCounter = 0;
            keyStore.Clear();
        }
    }
}
