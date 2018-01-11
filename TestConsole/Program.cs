using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDb.DAL.Repositories;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var rep = new UserRepository();
            var users = rep.GetHashAndSalt("admin");
        }
    }
}
