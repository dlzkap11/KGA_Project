using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Inventory
{
    internal class Program
    {
        static void UseItem(List<Item> item)
        {
            int input = -1;
            Console.WriteLine("사용할 아이템을 고르세요!");
            for (int i = 0; i < item.Count; i++)
            {
                Console.WriteLine($"{i + 1}){item[i].name}");
            }


            input = int.Parse(Console.ReadLine());
            if (input - 1 < 0 || input - 1 >= item.Count)
            {
                Console.WriteLine("아이템을 찾지 못 했다.");
                Console.WriteLine();
                return;
            }

            if (item[input - 1].type == Item.Type.Usable)
            {
                item[input - 1].Use();
                item.RemoveAt(input - 1);
            }
            else
                Console.WriteLine($"{item[input - 1].name}은 사용할 수 없다.");
                
        }

        static void GetItem(List<Item> item)
        {
            int input = -1;
            Console.WriteLine("얻을 아이템을 고르세요! \n 1)포션 \n 2)롱소드 \n 3)열쇠");
            input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    item.Add(new Potion());
                    break;
                case 2:
                    item.Add(new Weaphon());
                    break;
                case 3:
                    item.Add(new Key());
                    break;
                default:
                    Console.WriteLine("아이템을 얻지 못했습니다.");
                    break;
            }
            Console.WriteLine();
        }

        static void RemoveItem(List<Item> item)
        {
            int input = -1;
            bool isRemove = false;
            Console.WriteLine("버릴 아이템을 고르세요!");
            for(int i = 0; i < item.Count; i++)
            {
                Console.WriteLine($"{i + 1}){item[i].name}");
               
            }
            
            // 이름을 입력 받아서 아이템을 버리는 방법
            string input2 = Console.ReadLine();

            isRemove = item.Remove(item.Find(x => x.name == input2));
            if (isRemove)
            {
                Console.WriteLine("아이템을 버렸습니다.");
            }
            else
                Console.WriteLine("아이템을 못버렸슴다.");


            // 번호를 받아서 아이템을 버리는 방법
            /*
            input = int.Parse(Console.ReadLine());
            if(input - 1 < 0 || input - 1 >= item.Count)
            {
                Console.WriteLine("아이템을 버리지 못 했다.");
                Console.WriteLine();
                return;
            }
            item.RemoveAt(input - 1);
            */
        }

        static void InventoryCheck(List<Item> items)
        {
            int i = 0;
            Console.WriteLine("인벤토리 창");
            foreach (Item item in items)
            {
                Console.WriteLine($"{++i}){item.name}");
            }
            Console.WriteLine();
        }
        public static List<Item> inventory = new List<Item>();
        static void Main(string[] args)
        {
            //List<Item> inventory = new List<Item>();
            //TODO stack으로 뒤로가기 만들기...
            //stack pop으로 뒤로가고 push로 다음 ui로 넘어가기..를 추가하기
            // 구매 판매 구현

            int input = -1;

            while(input != 0)
            {

                
                Console.WriteLine("인벤토리 메뉴 \n 1)아이템 얻기 \n 2)아이템 버리기 \n 3)인벤토리 확인 \n 4)아이템 사용 \n 0)종료");
                input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        //아이템 얻기
                        Console.Clear();
                        GetItem(inventory);
                        break;
                    case 2:
                        Console.Clear();
                        //아이템 버리기
                        RemoveItem(inventory);
                        break;
                    case 3:
                        Console.Clear();
                        //인벤토리 확인
                        InventoryCheck(inventory);
                        break;
                    case 4:
                        Console.Clear();
                        UseItem(inventory);
                        break;
                    case 0:
                        Console.WriteLine("종료");
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
            

        }
    }

    public class Item
    {
        public enum Type { Equip, Quest, Usable, ETC }
        public string name;
        public Type type;

        public virtual void Use()
        {

        }
    }

    public class Potion : Item
    {
        public Potion()
        {
            name = "포션";
            type = Type.Usable;
        }

        public override void Use()
        {
            Console.WriteLine($"{name}을 사용했다.");
        }

    }

    public class Weaphon : Item
    {
        public Weaphon()
        {
            name = "롱소드";
            type = Type.Equip;
        }
    }

    public class Key : Item
    {
        public Key()
        {
            name = "열쇠";
            type = Type.Usable;
        }

        public override void Use()
        {
            Console.WriteLine($"{name}을 사용했다.");
        }
    }

}
