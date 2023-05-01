using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Utils
{
    public class PantheraTokens
    {

        static public Dictionary<string, string> TokensList = new Dictionary<string, string>();

        static public void Add(string name, string text)
        {
            if (TokensList.ContainsKey(name)) TokensList[name] = text;
            else TokensList.Add(name, text);
        }

        static public string Get(string name)
        {
            if (TokensList.ContainsKey(name)) return TokensList[name];
            else return "";
        }

    }
}
