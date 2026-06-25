using System;
using System.Collections.Generic;
using System.Text;

namespace Poketmon
{
    internal class Player
    {
        // 이름, 포켓몬 리스트
        public string Name;
        public List<Poketmons> MyPoketmon = new List<Poketmons>();

        public Player(string name)
        {
            Name = name;
        }
        
        public Player()
        {
            Name = "레드";
        }
        
    }
}
