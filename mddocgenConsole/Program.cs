using MdDocGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mddocgenConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MdDocManager mdDocManager = new MdDocManager();

            mdDocManager.GenerateMdDoc(args[0], args[1]);

            Console.WriteLine("End of doc generator");
        }
    }
}
