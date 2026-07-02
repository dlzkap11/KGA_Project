using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    public class Monster
    {
        public string Name;
        public int Level;
        public string Attribute;

        public Monster(string name, int level, string attribute)
        {
            Name = name;
            Level = level;
            Attribute = attribute;
        }

    }


    public static class SortWork
    {

        public static void SortByName(List<Monster> monsters)
        {
            monsters.Sort((x, y)=> x.Name.CompareTo(y.Name));
        }

        public static void SortByLevel(List<Monster> monsters)
        {
            // 오름차순 아마도
            //monsters.Sort((x, y) => x.Level.CompareTo(y.Level));
            //monsters.Sort((x, y) => x.Level - y.Level);

            //내림차순
            monsters.Sort((x, y) => y.Level - x.Level);
        }

        public static void SortByAttribute(List<Monster> monsters)
        {
            monsters.Sort((x, y) => x.Attribute.CompareTo(y.Attribute));
        }

    }
}
