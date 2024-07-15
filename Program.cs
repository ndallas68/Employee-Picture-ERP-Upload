using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePictureUpload
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                throw new Exception("Not enough arguments supplied");
            }
            EmployeePictureUpload employeePictureUpload = new EmployeePictureUpload(args[0], args[1], args[2], args[3]);
            employeePictureUpload.GetImages();

        }

    }
}


