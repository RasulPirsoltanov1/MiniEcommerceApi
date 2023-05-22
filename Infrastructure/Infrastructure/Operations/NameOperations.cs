using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApi.Infrastructure.StaticServices
{
    public static class NameOperations
    {
        public static string CharacterRegulyator(string name)

        {
            name = name
                .Replace("+", "")
                .Replace("-", "")
                .Replace("*", "")
                .Replace("!", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("%", "");
            return name;
        }
    }
}
