using System;
using System.Collections.Generic;
using System.Text;

namespace Poketmon
{
    internal class Poketmons
    {
        // 이름, 레벨, HP, ATK, Def, 속도
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Hp {  get; private set; }
        public int MaxHp {  get; private set; }
        public int Atk { get; private set; }
        public int Def { get; private set; }
        public int Speed { get; private set; }

        // 생존여부
        public bool IsAlive => Hp > 0;

        public Poketmons()
        {
            Name = string.Empty;
        }

        public Poketmons(string name, int level)
        {
            Name = name;
            Level = level;
            MaxHp = (level * 2) + 10 ;
            Hp = MaxHp;
            Atk = level * 2;
            Def = level * 2 + 1;
            Speed = level * 3;
        }

        // 기술리스트? 최대 4개
        List<Skills> Skill = new List<Skills>();


        // 상태표기
        public void PrintInfo()
        {
            Console.WriteLine($"[{Name}] Lv:{Level} HP:{Hp}/{MaxHp}");
        }

        // 공격
        public void Attack()
        {
            
        }
        // 피격
        public void TakeDamage()
        {

        }
    }
}
