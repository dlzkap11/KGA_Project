using System.Numerics;

namespace Poketmon
{
    internal class Program
    {
        public static Stack<UI> PoketmonUI = new Stack<UI>();
        public static Queue<string> queue = new Queue<string>();

        static Poketmons[] poketmons = { new Poketmons("꼬렛", 4), new Poketmons("부우부", 3), new Poketmons("모다피", 1) };
        static Poketmons EnemyPoketmon = new Poketmons();
        static Player player = new Player();

        public enum UI
        {
            MainMenu,
            Wild,
            Battle,
            Bag,
            Poketmon,
            Run,

        }

        // 가방 <dictionary>?
        // 몬스터볼 리스트, 도구 리스트

        List<int> ball = new List<int>();
        List<int> tool = new List<int>();

        // 가방 내용물 확인

        // 기술 클래스
        // 기술명, 위력, 타입, 명중률


        static void Main(string[] args)
        {
            // 포낏만 전투
            // 뭐가있냐
            // 야생의 포켓몬을 만난다..
            // 싸운다 가방 포켓몬 도망친다.
            // 싸운다 -> 기술 4개 중 고르기
            // 가방 -> 몬스터볼 도구
            // 몬스터볼 -> 몬스터볼 리스트 - 선택 후 사용 결정
            // 도구 -> 도구 리스트 - 선택 후 사용 결정
            // 포켓몬 -> 포켓몬 리스트(최대 6마리) -> 선택하고 교환 결정
            // 도망친다 -> 전투종료

            PoketmonUI.Push(UI.MainMenu);

            // MainMenu 반복(도망치기 전까지)
            while (PoketmonUI.Count > 0)
            {
                UI state = PoketmonUI.Peek();
                switch (state)
                {
                    case UI.MainMenu:
                        MainMenu();
                        break;
                    case UI.Wild:
                        Wild();
                        break;
                    case UI.Battle:
                        Battle();
                        break;
                    case UI.Bag:
                        CheckBag();
                        break;
                    case UI.Poketmon:
                        CheckPoketmon();
                        break;
                    case UI.Run:
                        Run();
                        break;
                }
                Console.Clear();
            }
        }

        
        static void MainMenu()
        {
            Console.WriteLine("플레이어의 이름을 입력하세요.");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                input = "배틀의 천재 심향";
            }
            player.Name = input;
            //Player player = new Player(input);

            while (player.MyPoketmon.Count == 0)
            {
                Console.WriteLine("포켓몬을 고르세요!");
                Console.WriteLine("1)이상해씨 2)꼬부기 3)파이리");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        player.MyPoketmon.Add(new Poketmons("이상해씨", 5));
                        PoketmonUI.Pop();
                        PoketmonUI.Push(UI.Wild);
                        break;
                    case "2":
                        player.MyPoketmon.Add(new Poketmons("꼬부기", 5));
                        PoketmonUI.Pop();
                        PoketmonUI.Push(UI.Wild);
                        break;
                    case "3":
                        player.MyPoketmon.Add(new Poketmons("파이리", 5));
                        PoketmonUI.Pop();
                        PoketmonUI.Push(UI.Wild);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }
        
        // 현재 싸우고있는 포켓몬
        // 적이 hp가 0이 되거나 내가 0이 되어 아무튼 전투가 끝나면 상대 포켓몬은 null로 없애기

        static void Wild()
        {
            // 포켓몬 등장
            Console.WriteLine("!야생의 포켓몬이 나타났다!");
            

            // 랜덤 출현
            EnemyPoketmon = poketmons[0 % 3];
            // 상대 포켓몬 상태
            EnemyPoketmon.PrintInfo();
            Console.WriteLine("===============================");
            // 내 포켓몬 상태
            player.MyPoketmon[0].PrintInfo();
            Console.WriteLine("===============================");

            // 선택지 UI
            Console.WriteLine("1)싸운다    2)가방");
            Console.WriteLine("3)포켓몬    4)도망친다");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    //싸운다
                    PoketmonUI.Push(UI.Battle);
                    break;
                case "2":
                    //가방확인
                    PoketmonUI.Push(UI.Bag);
                    break;
                case "3":
                    //포켓몬 확인
                    PoketmonUI.Push(UI.Poketmon);
                    break;
                case "4":
                    //도망치기(종료)
                    PoketmonUI.Pop();
                    Console.WriteLine("무사히 도망쳤다.");
                    Thread.Sleep(1000);
                    //PoketmonUI.Push(UI.Run);
                    break;
            }
        }


        static void Battle()
        {

        }

        static void CheckBag()
        {

        }

        static void CheckPoketmon()
        {

        }

        static void Run()
        {
            //확률로 도망가기
        }

    }
}
