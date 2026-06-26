namespace ItemGuide
{
    internal class Program
    {
        //<ID, 아이템>
        //<string, 아이템>
        // 두개가 있어야 하나? 그냥 ID로 하나 만들고 검색할 때 값의 이름을 찾는 거로 하면 어찌어찌 안되나?
        public static Dictionary<string, Item> Items = new Dictionary<string, Item>();
        public static List<Item> MyInventory = new List<Item>();
        public static List<ItemData> MyInventory2 = new List<ItemData>();

        //이게 되네;; 이러면 리스트 1칸마다 <아이템, 갯수> 인가 근데 에바인듯
        public static List<Dictionary<Item, int>> MyInventory3 = new List<Dictionary<Item, int>>();

        public static Dictionary<ItemData, int> MyInventory4 = new Dictionary<ItemData, int>();
        public static Dictionary<ItemData[], int> MyInventory5 = new Dictionary<ItemData[], int>();

        public static Dictionary<int, int> MyInventory6 = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            // 자 와서 보거라
            // 일단 db에 있는 뭐 아이템종류를 받아와서 딕셔너리에 추가한다. -> db에는 아이템번호 이름 이것저것 있음 이건 기획이 해줄거임ㅇㅇ 아마도
            // 이 후 딕셔너리에서 바뀌면 샥하고 바뀌면 좋겠는 것들은 item생성할 때 딕셔너리에 있는 값을 받아온다 뭐 id라거나 이름 *내구도같은 경우는 객체 각각이니 안받음
            // 그러면 이제 객체를 생성할 때 딕셔너리에 있는 값들이 딱딱하고 나온다
            // 까지가 이해한거같은데 맞나 -> 의문 item 생성할 때 그러니까 생성자에다가 딕셔너리값을받을 수 있나? 해봐야함 -- 해보니 안됨
            // 아니면 애초에 딕셔너리로 추가할 때 값을 지정해서 넣어주기 - 더 좋은 방법이 있는가?
            Items.Add("열쇠", new Item(1, "열쇠", "문을 열 수 있다", 0.3f));
            Items.Add("나뭇가지", new Item(2, "나뭇가지", "길에서 주웠다", 0.1f));
            Items.Add("휴지", new Item(3, "휴지", "마음이 편해진다", 0.1f));
            Items.Add("롱소드", new Item(4, "롱소드", "공격을 할 수 있다", 1.2f));
            Items.Add("포션", new Item(5, "포션", "마시면 체력이 회복된다", 0.6f));
            Items.Add("쿠션", new Item(6, "쿠션", "앉을 때 편해진다", 0.5f));
            Items.Add("방패", new Item(7, "방패", "공격을 막을 수 있다", 1.7f));
            Items.Add("갑옷", new Item(8, "갑옷", "무겁지만 입으면 안전하다", 5.2f));
            Items.Add("신발", new Item(9, "신발", "새 신발이다", 2.2f));
            Items.Add("빵", new Item(10, "빵", "먹으면 배고픔이 회복된다", 0.4f));


            // 딕셔너리에서 참조해서 가져온 데이터
            // 같은 키를 가진 객체들은 데이터를 공유함 -> 이름을 바꾸면 전체가 다 바뀜
            Item key = Items["열쇠"];
            Item sword = Items["롱소드"];
            Item potion = Items["포션"];
            Item breadData = Items["빵"];
            Item key2 = Items["열쇠"];

            key2.MaxDur = 8; //key도 바뀜
            Items["열쇠"].MaxDur = 5; // -> key, key2 다 바뀜

            // 내구도는 객체마다 다름
            ItemData bread = new ItemData();
            ItemData bread2 = new ItemData();
            bread._Item = breadData;
            bread2._Item = breadData;
            bread.Durability = 5;
            bread2.Durability = 3;
            // 모든 빵 이름이 바뀜
            Items["빵"].Name = "돌빵"; // -> 이런건 처음 딕셔너리에 추가하는 부분에서 바꿔주면 전체가 바뀔 수 있다. 전체 관리가 편하다 와! 마참내!

            MyInventory.Add(key);
            MyInventory.Add(sword);
            MyInventory.Add(potion);
            MyInventory.Add(breadData);
            MyInventory.Add(key2);



            MyInventory2.Add(bread);
            MyInventory2.Add(bread2);

            // 그러면 인벤토리에 아이템을 추가할 때 만약 같은 이름(같은 키)를 가진 아이템은 카운트를 늘리는 형태
            // 인벤토리에서 사람이 보는 UI에서는 1칸에 여러개가 들어간것으로 보이지만 실제로는 그렇게 못하지않나?
            // 딕셔러니면 <빵, 숫자> -> 빵이 들어올때마다 숫자 1증가 (없었으면 새로 생성)가능하지않나
            // 인벤토리를 만들 때 딕셔너리로 만들어야하는가? 근데 그러면 뭔가 비효율인 느낌이 든다 -> 삽입삭제가 자주 일어나지않나?
            // 그렇다고 인벤토리를 리스트로 만들고 같은 이름을 매번 탐색하고 있으면 카운트 증가 이런식이면 이거도 뭔가뭔가임
            // ex) 빵 포션 롱소드 -> 롱소드 추가
            //     빵 포션 롱소드x2 이런식

            // 처음 빵을 얻었을 때
            MyInventory4.Add(bread, 1);

            // 이후 빵을 얻으면 +1 -> 카운트에 최대치를 지정하면 그거보다 많아지면 새로운 칸에 빵을 추가해야함
            //                        빵은 한칸 당 40개가 최대라면 41번째 빵은 옆에 칸에 새로 추가
            MyInventory4[bread]++;
            if (MyInventory4[bread] >= 40)
            {
                int temp = Math.Abs(MyInventory4[bread] - 40);
                ItemData bread3 = bread; //새로운 빵 객체 유통기한을 제외하면 딕셔너리에서 공유됨
                MyInventory4.Add(bread3, temp);
            }
            // 빵을 사용하면 -- 만약 0이 되면 키값 삭제 == 인벤토리에서 제외
            MyInventory4[bread]--;
            if (MyInventory4[bread] == 0)
                MyInventory4.Remove(bread);
            // 이러면 빵마다 내구도(유통기한)는 다를 수는 없는듯? 만약 또 그렇게하려면 어떻게 해야하는가
            // 돈스타브를 생각해보면 음식에 유통기한이 시간이 지나면 줄어듬
            // -> 근데 거기다가 음식을 추가하면 늘어남 -> 아마 평균시간으로 정해지는듯
            // 그럼 정말 각각에 유통기한을 보려면 애초에 받을 때 아이템클래스 배열로 받아야하는가?
            // 
            ItemData[] poketmons = { new ItemData(), new ItemData(), new ItemData() };
            for (int i = 0; i < poketmons.Length; i++)
                poketmons[i]._Item = breadData;

            MyInventory5.Add(poketmons, poketmons.Length);

            // 하고나니 드는 의문 그럼 애초에 빵을 인벤토리에 넣을 때 유통기한이 다른 빵은 객체가 다르다
            // 그러면 <ItemData, int>에서 ItemData 자체가 다름 = 키값이 애초에 다르다 = 빵들이 각각 다른 위치에 저장됨
            // 그럼 인벤토리를 딕셔너리로 만드려면 <ItemData, int>
            // 요게 아니라 <string(아이템 이름), int>이나 <int(아이디), int>으로 받아야 되네
            // 그렇게 하면 어 일단 빵은 빵끼리 들어가짐
            // 이제 결국 유통기한은 어떻게 되는가
            // 여기서 이제 처리방식을 정해야하는듯 위에서 말한 돈스타브처럼 같은 칸에 있는 빵에 유통기한을 더해서
            // 평균값을 구하거나 빵마다 유통기한을 또 받아서 따로 저장해놓고 보여주는식?
            static void GetItem(ItemData item)
            {
                //처음 아이템을 얻으면
                if(!MyInventory6.ContainsKey(item._Item.Id))
                    MyInventory6.Add(item._Item.Id, 1);

                // 이후 빵을 얻으면 +1 -> 카운트에 최대치를 지정하면 그거보다 많아지면 새로운 칸에 빵을 추가해야함
                //                        빵은 한칸 당 40개가 최대라면 41번째 빵은 빈 칸에 새로 추가
                MyInventory6[item._Item.Id]++;
                if (MyInventory6[item._Item.Id] >= 40)
                {
                    int temp = Math.Abs(MyInventory6[item._Item.Id] - 40);
                    ItemData newitem = item; //새로운 빵 객체 유통기한을 제외하면 딕셔너리에서 공유됨
                    MyInventory6.Add(newitem._Item.Id, temp); // 여기서 문제 --> 아이디가 같다 -- 그러면 <string, int>로 받고 "빵" -> 새로 들어오는 빵은 "빵빵" 이런식으로 하면?
                    // 이 후 빵체크는 빵 갯수 확인 40개 이상이면 빵빵이 있나 확인 후 없으면 생성하고 빵 넣기, 있으면 빵빵에 넣기 또 만약 빵빵이 가득차면 추가로 빵빵빵 만들기...
                    // 같은 방법으로 사용시 사라지는 것도 가능하긴함

                    // 아니면 뭔가 편법인데 그냥 하나에 계속 채우고 보여지는 부분만 두칸을 먹는 것처럼 하기?
                    // 채우는 건 계속 채우고 만약 40개를 넘어가면 인벤토리 칸을 2개 먹게하기 실제로 인게임에서 보이는건 40 4 이지만 코드상으로는 하나에 뭉쳐져있음
                    //int inventoryHando = 30 // 가방이 30칸
                    //item >= 40 이면 int itemCnt = item / 40  if(item  % 40 != 0) itemCnt++; 만큼 가방칸 가지기
                    // itemCnt 만큼 반복 -> 칸마다 빵 40개씩 넣은 것처럼 보이게 하기
                    // console.WriteLine($"{item.Name} {MaxInventory}개); 마지막은 item % 40 개
                    // 이렇게하면 보이기에는 그렇게 보일 수 있지만 상호작용이 가능한가?에 대한 부분은 모르겄음

                    //결국 빈칸을 찾는다라는게 가능한가? 할당된 공간은 있겠지만 key값을 지정해줘야하는디 중복가능성이 있음 - 불안정

                    //결론 인벤토리는 딕셔너리를 안쓰는게 마따 차라리 하나의 클래스로 만들어서 기능구현을 하는게 낫지않을까?
                    //그러고 인벤토리를 필요로 하는 객체에게 인벤토리 클래스를 사용할 수 있게 해주기
                }
                // 빵을 사용하면 -- 만약 0이 되면 키값 삭제 == 인벤토리에서 제거
                MyInventory6[item._Item.Id]--;
                if (MyInventory6[item._Item.Id] == 0)
                    MyInventory6.Remove(item._Item.Id);
            }


            // 다른 객체 딕셔너리 관련 X
            MyInventory.Add(new Item(1, "중요한 열쇠", "문을 열 수 있다", 0.3f));





            Console.WriteLine("메인 메뉴");
            Console.WriteLine("1) 아이템 도감");
            Console.WriteLine("2) 인벤토리");

            string input = Console.ReadLine();
            while (true)
            {
                switch (input)
                {
                    case "1":
                        ItemDictionary();

                        break;
                    case "2":
                        Inventory();

                        break;
                    default:
                        return;
                }
                
            }
            


        }
        static void ItemDictionary()
        {
            
            int num = 0;
            Console.WriteLine("찾으실 아이템을 입력하세요:");
            string input = Console.ReadLine();

            // ID를 입력시
            if (int.TryParse(input, out num))
            {

                bool isFind = false;
                foreach (Item item in Items.Values)
                {
                    if(item.Id  == num)
                    {
                        Console.WriteLine($"{item.Name}");
                        Console.WriteLine($"{item.Explan} \n무게:{item.Weight}Kg");
                        isFind = true;
                        return;
                    }
                }

                if (!isFind)
                {
                    Console.WriteLine("해당 아이템을 찾을 수 없습니다.");
                    return;
                }

            }

            // 이름으로 검색시
            if (Items.ContainsKey(input))
            {
                Console.WriteLine($"{Items[input].Name}");
                Console.WriteLine($"{Items[input].Explan} \n무게:{Items[input].Weight}Kg");
            }
            else
            {
                Console.WriteLine("해당 아이템을 찾을 수 없습니다.");
            }

            
        }

        static void Inventory()
        {
            int i = 1;

            Console.WriteLine("인벤토리 내용");
            foreach(Item item in MyInventory)
            {
                Console.WriteLine($"{i}){item.Name}");
                Console.WriteLine($"{item.Explan} 내구도: {item.MaxDur}");
                i++;
            }

            i = 1;

            Console.WriteLine("\n인벤토리 내용");
            foreach (ItemData item in MyInventory2)
            {
                Console.WriteLine($"{i}){item._Item.Name}");
                Console.WriteLine($"{item._Item.Explan} 내구도: {item.Durability} / {item._Item.MaxDur}");
                i++;
            }
        }
    }
}
