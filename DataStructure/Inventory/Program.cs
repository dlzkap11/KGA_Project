namespace Inventory
{
    internal class Program
    {
        public enum UI
        {
            Main,
            Buy,
            Sell,
            BuyConfirm,
            SellConfirm,

        }

        public static List<Item> Inventory = new List<Item>();
        public static Stack<UI> stackUI = new Stack<UI>();

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


            while (stackUI.Peek() == UI.Buy)
            {
                Console.WriteLine("구매할 아이템을 고르세요! \n 1)포션 \n 2)롱소드 \n 3)열쇠 \n 0)뒤로가기");
                input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                    case 2:
                    case 3:
                        stackUI.Push(UI.BuyConfirm); // 구매 확인
                        BuyConfirm(input);
                        break;
                    case 0:
                        Console.Clear();
                        stackUI.Pop();
                        break;
                    default:
                        Console.WriteLine("아이템을 얻지 못했습니다.");
                        break;
                }
                Console.Clear();
            }
        }

        static void BuyConfirm(int num)
        {
            Item[] items = { new Potion(), new Weaphon(), new Key() };

            if (num == 1)
            {
                Console.WriteLine($"{items[num - 1].name} : 사용하면 체력을 회복합니다.");

            }
            else if (num == 2)
            {
                Console.WriteLine($"{items[num - 1].name} : 긴 검입니다.");
            }
            else if (num == 3)
            {
                Console.WriteLine($"{items[num - 1].name} : 사용하면 문을 열 수 있습니다.");
            }

            Console.WriteLine($"정말로 {items[num - 1].name}을 구매하시겠습니까?");
            Console.WriteLine("1)구매 0)취소");

            string input = Console.ReadLine();
            if (input == "1")
            {
                Inventory.Add(items[num - 1]);

            }
            stackUI.Pop();

        }

        static void RemoveItem(List<Item> item)
        {

            while (stackUI.Peek() == UI.Sell)
            {
                Console.WriteLine("판매할 아이템을 고르세요!");
                for (int i = 0; i < item.Count; i++)
                {
                    Console.WriteLine($"{i + 1}){item[i].name}");

                }

                Console.WriteLine("나가려면 [나가기]를 입력하세요.");

                // 이름을 입력 받아서 아이템을 버리는 방법
                string input2 = Console.ReadLine();

                if (input2 == "나가기")
                {

                    stackUI.Pop();
                    Console.Clear();
                }
                else
                {
                    stackUI.Push(UI.SellConfirm);
                    SellConfirm(input2);
                }


                Console.Clear();
            }

            // 번호를 받아서 아이템을 버리는 방법
            /*
            int input = int.Parse(Console.ReadLine());
            if(input - 1 < 0 || input - 1 >= item.Count)
            {
                Console.WriteLine("아이템을 버리지 못 했다.");
                Console.WriteLine();
                return;
            }
            item.RemoveAt(input - 1);
            */
        }

        // 판매확인
        static void SellConfirm(string item)
        {
            Item sell = Inventory.Find(x => x.name == item);
            if (sell == null)
            {
                Console.WriteLine("해당 아이템을 찾을 수 없습니다.");
                Thread.Sleep(1000);
                stackUI.Pop();
            }
            else
            {
                Console.WriteLine($"{sell.name}을 정말 판매하시겠습니까?");
                Console.WriteLine("1) 넹 2) 아니용");

                string input = Console.ReadLine();
                if (input == "1")
                {
                    Inventory.Remove(sell);
                }

                stackUI.Pop();
            }

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

        static void Main(string[] args)
        {
            int input = -1;
            stackUI.Push(UI.Main); //메인메뉴 생성
            while (stackUI.Count > 0)
            {
                Console.WriteLine("인벤토리 메뉴 \n 1)아이템 구매 \n 2)아이템 판매 \n 3)인벤토리 확인 \n 4)아이템 사용 \n 0)종료");
                input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        //아이템 얻기
                        Console.Clear();
                        stackUI.Push(UI.Buy);
                        GetItem(Inventory);
                        break;
                    case 2:
                        //아이템 버리기
                        Console.Clear();
                        stackUI.Push(UI.Sell);
                        RemoveItem(Inventory);
                        break;
                    case 3:
                        Console.Clear();
                        //인벤토리 확인
                        InventoryCheck(Inventory);
                        break;
                    case 4:
                        Console.Clear();
                        UseItem(Inventory);
                        break;
                    case 0:
                        stackUI.Pop(); //메인메뉴 닫기
                        Console.WriteLine("종료");
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
    }

    //Item 클래스
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
