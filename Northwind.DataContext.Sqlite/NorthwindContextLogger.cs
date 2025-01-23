using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace Northwind.EntityModels
{
    public class NorthwindContextLogger
    {
        public static void WriteLine(string message) {
            string path = Path.Combine(GetFolderPath(SpecialFolder.DesktopDirectory), "northwindlog.txt");

            StreamWriter file = File.AppendText(path);
            file.Write(message);
            file.Close();
        }
    }
}
