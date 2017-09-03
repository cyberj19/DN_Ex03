using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    static class Program
    {
        public static void Main()
        {
            //todo: make sure throwing the right exceptions. Argument exception for trying to fuel an electric car? Mybe Bad Parsing?
            Garage garage = new Garage();
            garage.Run();
        }
    }
}
