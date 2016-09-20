using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCCore.Extensions
{
    public static class CommonExtensions
    {
        public static string JsonToStringFormat(this string json)
        {
            if(string.IsNullOrWhiteSpace(json)) { return string.Empty; }

            return json.Replace("{", "{{").Replace("}", "}}");
        }
    }
}
