using System.Xml.Linq;
using static TextRPG.Program;


//TODO
//퍼킹 구조체라서 함수로 값 변경을 해도 실제 값이 변하지 않고있음 -> 구조체를 넣고있는 부분을 ref로 바꾸면 가능할듯
namespace TextRPG
{
    internal class Program
    {
        const int MaxInvenSize = 5;
        const int PostionHeal = 30;
        const int MinDamage = 1;
        static Random rand = new Random();

        // 직업
        public struct Player
        {
            public string Name = "";
            public string JobName = "";
            public int Hp = 0;
            public int MaxHp = 0;
            public int Mp = 0;
            public int MaxMp = 0;
            public int AttackPower = 0;
            public int DefensePower = 0;

            public string[] Inventory = new string[MaxInvenSize];

            public Player(string name, string jobName, int maxHp, int maxMp, int attackPower, int defensePower, string weaphon)
            {
                Name = name;
                JobName = jobName;
                MaxHp = maxHp;
                Hp = MaxHp;
                MaxMp = maxMp;
                Mp = MaxMp;
                AttackPower = attackPower;
                DefensePower = defensePower;
                Inventory[0] = weaphon;
                Inventory[1] = "HP 포션";
                for(int i = 2; i < MaxInvenSize; i++)
                {
                    Inventory[i] = "";
                }
            }
        }

        // 몬스터
        public struct Monster
        {
            public string Name;
            public int Hp;
            public int MaxHp;
            public int AttackPower;
            public int DefensePower;

            public Monster(string name, int maxHp, int attackPower, int defensePower)
            {
                Name = name;
                MaxHp = maxHp;
                Hp = MaxHp;
                AttackPower = attackPower;
                DefensePower = defensePower;
            }

        }



        public static void Main(string[] args)
        {

            Console.WriteLine("============================");
            Console.WriteLine("       텍스트 RPG - 캐릭터 관리");
            Console.WriteLine("============================");

            string Name;
            Player player = new Player();
            string Input;

            Console.Write("이름을 입력하세요 : ");
            Name = Console.ReadLine();

            while (true)
            {
                Console.Write("직업을 선택하세요 (1.전사 / 2.마법사 / 3.궁수):");
                Input = Console.ReadLine();
                switch (Input)
                {
                    case "1":
                        Input = "전사";
                        player = new Player(Name,"전사", 120, 60, 30, 15, "낡은 검");
                        break;
                    case "2":
                        Input = "마법사";
                        player = new Player(Name, "마법사", 80, 120, 22, 8, "낡은 지팡이");
                        break;
                    case "3":
                        Input = "궁수";
                        player = new Player(Name, "궁수", 100, 80, 26, 10, "낡은 활");
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력하세요. \n");
                        break;

                }
                if (player.Name != null)
                    break;
            }


            // 플레이어의 초기상태 출력
            PlayerInfo(Name, player);

            // 인벤토리
            InventoryInfo(player);


            // 행동선택
            int input = -1;

            while (input != 0)
            {
                Console.WriteLine("========== 행동을 선택하세요 ==========");
                Console.WriteLine("1. 몬스터 사냥 \n 2. 포션 사용 \n 3. 인벤토리 확인 \n 4. 캐릭터 정보 \n 0. 종료");
                input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        // 몬스터 사냥
                        Console.Clear();
                        FightMonster(player, player.Name, ref player.Hp, ref player.AttackPower, ref player.DefensePower);
                        break;
                    case 2:
                        // 포션 사용
                        Console.Clear();
                        UsePotion(player, ref player.Hp);
                        break;
                    case 3:
                        // 인벤토리 확인
                        Console.Clear();
                        InventoryInfo(player);
                        break;
                    case 4:
                        // 캐릭터 정보
                        Console.Clear();
                        PlayerInfo(Name, player);
                        break;
                    case 0:
                        // 종료
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력하세요. \n");

                        break;
                }
            }
        }
        // 캐릭터 정보
        public static void PlayerInfo(string name, Player player)
        {
            // 플레이어 정보
            Console.WriteLine($"[{name} / {player.JobName}]");
            Console.WriteLine($"HP: {player.Hp}/{player.MaxHp} MP: {player.Mp}/{player.MaxMp} 공격력: {player.AttackPower} 방어력: {player.DefensePower}");
            Console.WriteLine();
        }
        // 포션 사용
        public static void UsePotion(Player player, ref int hp)
        {
            // 인벤토리에 포션이 있으면 하나를 사용한다.
            // 사용시 회복량은 30
            for (int i = 0; i < 5; i++)
            {
                if (player.Inventory[i] != "HP 포션")
                    continue;
                else
                {
                    player.Inventory[i] = "";
                    hp += PostionHeal;
                    if (hp > player.MaxHp)
                    {
                        hp = player.MaxHp;
                    }
                    Console.WriteLine("포션을 사용해 HP를 회복하였습니다.");
                    return;
                }

                
            }
            Console.WriteLine("사용할 수 있는 포션이 없습니다.");

        }
        // 인벤토리 확인
        public static void InventoryInfo(Player player)
        {
            // 인벤토리
            Console.WriteLine("---------- 인벤토리 ----------");
            for (int i = 0; i < player.Inventory.Length; i++)
            {
                if(player.Inventory[i] == "")
                    Console.WriteLine($"[{i}] (빈 슬롯)");
                else
                    Console.WriteLine($"[{i}] {player.Inventory[i]}");
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine();
        }
        // 몬스터 사냥
        public static void FightMonster(Player player, string name, ref int hp, ref int attack, ref int defense)
        {
            int playerAttack;
            int enemyAttack;
            Monster monster;
            monster = CreateMonster();
            Console.WriteLine($"--- {monster.Name} 출현! ---");

            //내 공격 → 고블린 - 17(고블린 남은 HP: 33)
            //고블린 반격 → 내 - 7(내 남은 HP: 73)
            //내 공격 → 고블린 - 17(고블린 남은 HP: 16)
            //고블린 반격 → 내 - 7(내 남은 HP: 66)
            //내 공격 → 고블린 - 17(고블린 남은 HP: -1)
            //고블린을(를) 처치했습니다!

            playerAttack = CalculateDamage(attack, monster.DefensePower);
            enemyAttack = CalculateDamage(monster.AttackPower, defense);
            while (true)
            {

                monster.Hp -= playerAttack;
                Console.WriteLine($"{name} 공격 -> {monster.Name} - {playerAttack}({monster.Name} 남은 HP: {monster.Hp})");
                if (monster.Hp <= 0)
                {
                    Console.WriteLine($"{monster.Name}가 쓰려졌습니다.");

                    if (rand.Next(0, 4) == 0)
                    {
                        GetPostion(player);
                    }
                    return;
                }

                hp -= enemyAttack;
                Console.WriteLine($"{monster.Name} 반격 -> {name} - {enemyAttack}({name} 남은 HP: {hp})");
                if(hp <= 0 )
                {
                    Console.WriteLine($"{name}이(가) 사망하였습니다.");
                    return;
                }

            }



        }

        // 공격력 계산기 (만약 데미지가 0보다 작으면 최소 데미지로 전환)
        public static int  CalculateDamage(int attackPower, int defensePower)
        {
            int damage = attackPower - defensePower;
            if (damage < 0) damage = MinDamage;
            return damage;
        }

        public static void GetPostion(Player player)
        {
            //인벤토리를 확인해서
            for (int i = 0; i < player.Inventory.Length; i++)
            {
                if (player.Inventory[i] == "")
                {
                    player.Inventory[i] = "HP 포션";
                    Console.WriteLine("HP 포션을 획득했습니다!");
                    return;
                }
            }
            //만약 인벤토리에 추가가 가능한 경우 HP 포션을 추가한다.
            Console.WriteLine("인벤토리가 꽉 찼습니다. 더 이상 아이템을 획득할 수 없습니다.");
        }

        public static Monster CreateMonster()
        {
            Monster monster = new Monster();
            int num = rand.Next(0, 3);
            if (num == 0)
                monster = new Monster("슬라임", 30, 8, 2);
            else if (num == 1)
                monster = new Monster("고블린", 50, 15, 5);
            else
                monster = new Monster("오크", 80, 25, 12);

            return monster;
        }
    }
}
