using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public static class Methods
    {
        public static string FirstToUpper(string input)
        {
            string firstLetter = input.Substring(0, 1).ToUpper();
            string restOfWord = input.Substring(1).ToLower();
            return string.Concat(firstLetter, restOfWord);
        }
    }
}
