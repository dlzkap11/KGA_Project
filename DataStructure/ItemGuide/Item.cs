using System;
using System.Collections.Generic;
using System.Text;

namespace ItemGuide
{

    public class Item
    {
        // 이름, 뭐 설명
        public int Id;
        public string Name;
        public string Explan;
        public float Weight;
        public int MaxDur;

        public Item(int id, string name, string explan, float weight)
        {
            Id = id;
            Name = name;
            Explan = explan;
            Weight = weight;
            MaxDur = 10;
        }

    }

    public class ItemData
    {
        public Item _Item;

        public int Durability = 10;
    }

}
