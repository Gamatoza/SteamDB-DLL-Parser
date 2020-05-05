using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;

namespace DLLParcer.Core.SteamDB
{
    class HabraParser : IParser<string[]>
    {
        public HabraParser()
        {
        }

        public string[] Parse(IHtmlDocument document)
        {
            var list  = new List<string>();
            var items = document.QuerySelectorAll("tr").Where(item => item.ClassName != null && item.ClassName.Contains("app"));

            foreach(var item in items)
            {
                list.Add(item.TextContent);
            }

            return list.ToArray();
        }
    }
}
