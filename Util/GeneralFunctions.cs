using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentService.Util
{
    public class GeneralFunctions
    {
        public static string GenerateStudentNumber() 
        {
            int min = 100000000;
            int max = 999999999;
            Random random = new Random();
            return random.Next(min, max).ToString();
        }
    }
}
