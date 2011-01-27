using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NCraft.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var directory = @"..\..\..\NCraft.Test\TestFiles\";

                //var file = @"bigtest.nbt.gz";
                //var file = @"level.dat";
                var file = @"c.0.0.dat";

                var path = Path.Combine(directory, file);
                //var path = Path.Combine(directory, "World1", "1a", "1p", "c.-i.-3.dat");


                var tag = NbtFile.Load(path);

                System.Console.WriteLine(tag.ToString());
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
