using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Extensions
{
    public static class StringExtensions
    {
        public static string TrimAndReplace(this string value) => value.Trim().Replace(" ", "").Replace(Environment.NewLine, "");

    }
}
