using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared
{
    [Serializable]
    public class Player
    {
        public Guid playerID;
        public string GamerTag;
        public string UserName;
        public string FirstName;
        public string SecondName;
        public int XP;
        public string clientID;
        public int scale;
    }

}