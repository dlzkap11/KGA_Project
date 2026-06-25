using System;
using System.Collections.Generic;
using System.Text;

namespace Poketmon
{
    public abstract class Skills
    {
        // 기술명, 위력, 타입, 명중률
        public string name {  get; protected set; }
        protected int damage;
        protected int pp;
        protected int accuracy;

        public abstract void skill();
    }


    public class Badoong : Skills
    {
        public Badoong()
        {
            name = "발버둥";
            damage = 50;
            pp = 1;
            accuracy = 100;
        }


        public override void skill()
        {
            Console.WriteLine($"{name}을 썼다.");
        }
    }
}
