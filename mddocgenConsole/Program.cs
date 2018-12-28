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
            bool cleanRun = args.Contains("-c");

            mdDocManager.GenerateMdDoc(args[args.Length - 2], args[args.Length - 1], cleanRun);

            Console.WriteLine("End of doc generator");
        }
    }
}
