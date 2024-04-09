using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicat_Casino
{
    public class User
    {
        public string Nick { get; set; }
        public int Tokens { get; set; }
        public User(string Nick, int Tokens)
        {
            this.Nick = Nick;
            this.Tokens = Tokens;
        }
    }
    public static class konta
    {
        public static User konto = null;
    }
}
