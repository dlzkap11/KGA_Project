class ZombieGame
{
    // ══════════════════════════════════════════
    // Step 1. 상수 + 열거형
    // ══════════════════════════════════════════

    // TODO: 게임에서 절대 바뀌지 않는 수치들을 상수로 선언하세요
    // 플레이어의 최대 HP, 공격력, 방어력
    // 스테이지당 좀비 수, 회복량, 최소 데미지
    const int PlayerMaxHp = 50;
    const int PlayerDamage = 20;
    const int PlayerDefense = 10;

    const int ZoobieCount = 5;
    const int HealAmount = 5;
    const int MinDamage = 1;

    // TODO: 플레이어의 상태를 열거형으로 선언하세요
    // 생존 / 위험(HP 30 이하) / 사망
    enum Status
    {
        Alive,
        Danger,
        Dead,
    }

    // TODO: 좀비의 등급을 열거형으로 선언하세요
    // 일반 / 달리기(공격력 높음) / 탱커(HP 높음)
    enum Rating
    {
        Normal,
        Run,
        Tank,
    }

    // ══════════════════════════════════════════
    // Step 2. 인터페이스 (추상화)
    // ══════════════════════════════════════════

    // TODO: 데미지를 받을 수 있는 대상의 약속을 인터페이스로 선언하세요
    // HP 조회, 생존 여부 확인, 데미지를 받는 기능이 필요합니다
    public interface IDamageable
    {
        //Hp 조회
        int Hp { get; }
        //public int GetHp();
        //생존 여부 확인
        bool IsAlive { get; }
        //데미지 받기
        public void TakeDamage(int damage);
    }

    // TODO: 회복할 수 있는 대상의 약속을 인터페이스로 선언하세요
    public interface IHealable
    {
        public void Heal(int amount);
        // amount가 상수로 있어서 굳이 변수를 안받아도 된다. -> 힐 메소드에서 상수로 바로 힐링하기
    }

    // TODO: 상태를 출력할 수 있는 대상의 약속을 인터페이스로 선언하세요
    public interface IPrintable
    {
        public void PrintInfo();
    }

    // ══════════════════════════════════════════
    // Step 3. Zombie 클래스
    // IDamageable, IPrintable을 구현합니다
    // ══════════════════════════════════════════

    class Zombie : IDamageable, IPrintable
    {
        // TODO: 이름, HP, 최대HP, 공격력, 등급 프로퍼티를 선언하세요
        // 외부에서 읽을 수는 있지만 직접 바꿀 수는 없어야 합니다
        public string Name { get; private set; }
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }
        public int Damage { get; private set; }
        public Rating Rank { get; private set; }

        // TODO: 생존 여부를 반환하는 읽기 전용 프로퍼티를 선언하세요
        public bool IsAlive { get; private set; }
        //public bool IsAlive => Hp > 0; // 아래에서 굳이 건드릴 필요가 없어짐 (알아 바뀜)

        // TODO: 생성자를 작성하세요
        // 등급에 따라 HP와 공격력이 달라집니다
        // 탱커: HP 80, 공격력 8 / 달리기: HP 30, 공격력 20 / 일반: HP 50, 공격력 12
        public Zombie(string name, int rank)//int rank --> Rating rank
        {
            Rank = (Rating)rank;
            Name = name;

            switch (Rank)
            {
                case Rating.Normal:
                    MaxHp = 50;
                    Damage = 12;
                    break;
                case Rating.Run:
                    MaxHp = 30;
                    Damage = 20;
                    break;
                case Rating.Tank:
                    MaxHp = 80;
                    Damage = 8;
                    break;
            }

            Hp = MaxHp;
            IsAlive = true;
        }

        // TODO: 데미지를 받는 기능을 구현하세요
        // HP가 0 아래로 내려가지 않도록 처리하세요
        public void TakeDamage(int damage)
        {
            Hp = Math.Max(Hp - damage, 0);
            IsAlive = Hp > 0; //public bool IsAlive => Hp > 0; 이거로하면 없어도 됨, event 사용가능 이것저것 해보기
        }

        // TODO: 상태를 출력하는 기능을 구현하세요
        // 예시: [탱커] 썩은손 | HP: 80/80 | 공격력: 8
        public void PrintInfo()
        {
            //Console.WriteLine($"[{Rank}] {Name} | HP: {Hp}/{MaxHp} | 공격력: {Damage}");

            if (Rank == Rating.Normal)
            {
                Console.WriteLine($"[일반] {Name} | HP: {Hp}/{MaxHp} | 공격력: {Damage}");
            }
            else if (Rank == Rating.Run)
            {
                Console.WriteLine($"[뛰좀] {Name} | HP: {Hp}/{MaxHp} | 공격력: {Damage}");
            }
            else
            {
                Console.WriteLine($"[탱커] {Name} | HP: {Hp}/{MaxHp} | 공격력: {Damage}");
            }
        }

    }


    // ══════════════════════════════════════════
    // Step 4. Player 클래스 (핵심 — 이벤트 발행자)
    //
    // 델리게이트/이벤트 구조:
    //   Player 안에 event를 선언합니다
    //   HP가 바뀌거나 좀비를 처치할 때 event를 발행합니다
    //   외부(Main)에서 event에 람다식으로 구독합니다
    //
    // event를 쓰는 이유:
    //   외부에서 직접 Invoke할 수 없어야 하고
    //   += / -= 로만 구독/해지할 수 있어야 하기 때문입니다
    // ══════════════════════════════════════════

    class Player : IDamageable, IHealable, IPrintable
    {
        // TODO: 이름, HP, 최대HP, 공격력, 방어력 프로퍼티를 선언하세요
        public string Name { get; private set; }
        private int hp;
        public int Hp
        {
            get => hp;
            set
            {
                int prevHp = hp;
                hp = Math.Clamp(value, 0, MaxHp);
                if (hp != prevHp)
                {
                    OnHpChanged?.Invoke(prevHp, hp);
                }
                if (hp == 0)
                {
                    Status = Status.Dead;
                }
                else if (hp <= 30)
                {
                    Status = Status.Danger;
                }
                else
                {
                    Status = Status.Alive;
                }
            }
        }
        public int MaxHp { get; private set; }
        public int Damage { get; private set; }
        public int Defense { get; private set; }

        // TODO: 생존 여부 프로퍼티를 선언하세요
        public bool IsAlive => Hp > 0;

        // TODO: 현재 상태를 반환하는 프로퍼티를 선언하세요
        // HP 조건에 따라 Dead / Danger / Alive 중 하나를 반환합니다
        public Status Status { get; private set; } //hp 조건에 따라서 바꾸려면 get에서 설정하면 된다 -> Hp set에 있는 내용

        // TODO: 이벤트 두 개를 선언하세요
        //
        // 첫 번째: HP가 바뀔 때 발행하는 이벤트
        //   구독자에게 이전 HP와 현재 HP를 전달해야 합니다
        //   Action<int, int> 형태로 선언하세요 (이전HP, 현재HP)
        public event Action<int, int> OnHpChanged;

        // 두 번째: 좀비를 처치했을 때 발행하는 이벤트
        //   구독자에게 처치한 좀비 이름을 전달해야 합니다
        //   Action<string> 형태로 선언하세요
        public event Action<string> OnZombieKilled;

        // TODO: 생성자를 작성하세요
        public Player(string name)
        {
            
            Name = name;
            MaxHp = PlayerMaxHp;
            hp = MaxHp;
            Damage = PlayerDamage;
            Defense = PlayerDefense;
            //Status = Status.Alive;
        }

        // TODO: 데미지를 받는 기능을 구현하세요
        // 방어력만큼 데미지를 줄이되 최소 MIN_DAMAGE는 보장합니다
        // HP가 바뀐 뒤 OnHpChanged 이벤트를 발행하세요
        //   발행 방법: OnHpChanged?.Invoke(이전HP, 현재HP)
        //   ?.Invoke 를 쓰는 이유: 구독자가 없으면(null) 오류 없이 무시됩니다
        // 예시: 준헌이(가) 3 피해를 받음 (HP: 97/100)
        public void TakeDamage(int damage)
        {
            //int prevHp = Hp;
            int finalDamage = Math.Max(damage - Defense, MinDamage);
            Console.WriteLine($"{Name}이 {finalDamage}만큼 피해를 받았다.");
            Hp = Hp - finalDamage;
            //OnHpChanged?.Invoke(prevHp, Hp);
        }

        // TODO: 회복 기능을 구현하세요
        // 최대 HP를 초과하지 않도록 처리하세요
        // 회복 후에도 OnHpChanged 이벤트를 발행하세요
        // 예시: 회복! 준헌 HP: 70/100
        public void Heal(int amount)
        {
            //int prevHp = Hp;
            Console.WriteLine($"{Name}이 {amount}만큼 회복했다.");
            Hp = Hp + amount;
            //OnHpChanged?.Invoke(prevHp, Hp);
        }

        // TODO: 좀비를 처치했을 때 호출할 메서드를 작성하세요
        // OnZombieKilled 이벤트를 발행합니다
        // 전투 함수에서 좀비가 죽었을 때 이 메서드를 호출하세요
        public void KillZombie(string name)
        {
            OnZombieKilled?.Invoke(name); // 좀비가 죽었을 때 외부에서 뭔가 할 것이다
            //Console.WriteLine($"{name}를 처치했다.");
        }

        // TODO: 상태 출력 기능을 구현하세요
        // 예시: [플레이어] 준헌 | HP: 100/100
        // Danger 상태면 끝에 위험 표시를 추가하세요
        public void PrintInfo()
        {
            Console.Write($"[{Name}] | HP: {Hp}/{MaxHp} | 공격력: {Damage} | 방어력: {Defense}");
            if (Status == Status.Danger)
                Console.WriteLine($" !위험!");
            else
                Console.WriteLine();
        }
    }


    // ══════════════════════════════════════════
    // Step 5. 전투 함수들
    // ══════════════════════════════════════════

    // TODO: 플레이어가 좀비를 공격하는 함수를 작성하세요
    // 탱커 좀비는 데미지가 절반입니다
    // 좀비가 처치되면 player.NotifyZombieKilled(좀비이름)를 호출하세요
    // 예시: 준헌 → 썩은손 공격! 12 피해
    static void AttackZombie(Player player, Zombie zombie)
    {
        int damage = zombie.Rank == Rating.Tank ? player.Damage / 2 : player.Damage;
        Console.WriteLine($"{player.Name}이 {zombie.Name}을 공격! {damage} 피해");
        zombie.TakeDamage(damage);

        if (zombie.Hp <= 0)
        {
            player.KillZombie(zombie.Name);
        }
    }

    // TODO: 좀비가 플레이어를 공격하는 함수를 작성하세요
    // 죽은 좀비는 공격하지 않습니다
    // 예시: 썩은손 → 준헌 공격!
    static void AttackPlayer(Player player, Zombie zombie)
    {
        if (!zombie.IsAlive)
            return;

        Console.WriteLine($"{zombie.Name}이 {player.Name}을 공격!");
        player.TakeDamage(zombie.Damage);
    }

    // TODO: 인덱스를 받아서 좀비를 생성하는 함수를 작성하세요
    // 인덱스 나머지로 등급을 결정하세요 (0:탱커 / 1:달리기 / 2:일반)
    // 이름 목록: 썩은손, 피투성이, 절름발이, 괴물, 검은눈
    static Zombie CreateZombie(int index)
    {
        string[] name = { "썩은손", "피투성이", "절름발이", "괴물", "검은눈" };

        Zombie zombie = new Zombie(name[index % name.Length], (index % 3));
        //names[index % name.Length] 혹시라도 5보다 높은 index값이 들어올 때를 대비
        return zombie;
    }

    // TODO: 전투 결과를 출력하는 함수를 작성하세요
    // 승리면 클리어 메시지와 생존 HP를 출력하세요
    // 패배면 게임 오버 메시지를 출력하세요
    static void BattleResult(bool isClear, Player player)
    {
        if (isClear)
            Console.WriteLine($"승리! \n 남은 HP: {player.Hp} / {player.MaxHp}");
        else
            Console.WriteLine("패배!");
    }

    // TODO: 전투 전체 흐름을 담당하는 함수를 작성하세요
    // 반환값은 승리 여부(bool)입니다
    // 흐름:
    //   좀비 배열을 만들고 채운 뒤 전체 상태를 출력합니다
    //   플레이어가 살아있는 동안 반복합니다
    //     살아있는 좀비가 없으면 승리를 반환합니다
    //     살아있는 첫 번째 좀비와 1:1 전투를 진행합니다
    //     플레이어가 위험 상태면 자동으로 회복합니다
    //   반복이 끝나면 패배를 반환합니다
    static bool Battle(Player player)
    {
        List<Zombie> list = new List<Zombie>();

        for (int i = 0; i < ZoobieCount; i++)
        {
            list.Add(CreateZombie(i));
            list[i].PrintInfo();
        }

        Console.WriteLine($"야생의 좀비들이 나타났다.");

        while (player.IsAlive)
        {
            if (list.Count == 0)
                return true;

            Zombie current = list[0];

            AttackZombie(player, current);
            Thread.Sleep(1000);

            if (current.Hp <= 0)
            {
                //Console.WriteLine($"{current.Name} 좀비를 처치했다.");


                list.RemoveAt(0);
                if (list.Count > 0)
                    Console.WriteLine($"{list[0].Name} 좀비가 다가온다.");
                continue;
            }

            AttackPlayer(player, current);

            if (player.Status == Status.Danger)
            {
                player.Heal(HealAmount);
            }

            if (!player.IsAlive)
                return false;

            Thread.Sleep(1000);
        }

        return false;
    }

    // ══════════════════════════════════════════
    // Step 7. 통합 실행
    

    // 이벤트 구독은 전부 여기서 합니다
    // Player 안에 UI/로그 코드를 넣지 않습니다
    // Player는 "이벤트를 발행"하고
    // Main이 "어떻게 반응할지"를 결정합니다
    // ══════════════════════════════════════════


    static void Main()
    {
        // TODO: 게임 타이틀을 출력하세요
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=================좀보 게임=================");

        // TODO: 플레이어 이름을 입력받으세요
        // 비어있으면 "생존자"로 설정하세요
        string name;
        Console.Write("플레이어 이름을 입력하세요 :");
        name = Console.ReadLine();
        //string.IsNullOrEmpty(name);  string이 비어있는지 확인 비어있으면 true 아니면 false
        if (name == null)
            name = "생존자";

        // TODO: Player 객체를 생성하세요
        Player player = new Player(name);

        // TODO: OnHpChanged 이벤트에 람다식으로 두 가지 반응을 구독하세요
        Action<int, int> hpBarHandler = (oldHp, newHp) =>
        {
            float ratio = (float)newHp / player.MaxHp;
            int filled = (int)(ratio * 10);
            string bar = new string('█', filled) + new string('░', 10 - filled);
            Console.WriteLine($"[UI] [{bar}] {newHp}/{player.MaxHp}");

            /*
            int filled = (int)Math.Round((double)newHp / player.MaxHp * 10);
            filled = Math.Clamp(filled, 0, 10);
            int empty = 10 - filled;
            Console.WriteLine($"[UI] [{new string('@', filled)}{new string('O', empty)}] {newHp}/{player.MaxHp}");
            */
        };
        player.OnHpChanged += hpBarHandler;


        Action<int, int> battleLogHandler = (oldHp, newHp) =>
        {
            Console.WriteLine($"[LOG] {player.Name} HP: {oldHp} -> {newHp}");
        };
        player.OnHpChanged += battleLogHandler;

        // 첫 번째 구독 — HP 막대 그래프 출력
        //   HP 비율을 계산해서 10칸짜리 막대로 출력합니다
        //   예시: [UI] [██████░░░░] 60/100
        //   힌트: new string('█', 채워진칸) + new string('░', 빈칸)
        //   player.OnHpChanged += (oldHp, newHp) => { ... }
        //
        // 두 번째 구독 — 전투 로그 출력
        //   HP 변화 내역을 출력합니다
        //   예시: [LOG] 준헌 HP: 100→70
        //   player.OnHpChanged += (oldHp, newHp) => { ... }


        // TODO: OnZombieKilled 이벤트에 람다식으로 구독하세요
        //   처치 내역을 출력합니다
        //   예시: [킬로그] 썩은손 처치!
        //   player.OnZombieKilled += (name) => { ... }
        player.OnZombieKilled += (zombieName) =>
        {
            Console.WriteLine($"[킬로그] {zombieName} 처치!");
        }; // 해지가 불가하기에 나중에 해지하려면 변수에 따로 저장해야함

        // TODO: 시작 메시지를 출력하세요
        // 예시: 준헌, 좀비 5마리를 처치하라!
        Console.WriteLine($"{player.Name}, 좀비 {ZoobieCount}마리를 처치하라!");

        // TODO: 전투를 실행하고 결과를 받으세요
        bool isClear = Battle(player);

        // TODO: 전투 결과를 출력하세요
        BattleResult(isClear, player);

        // TODO: HP 막대 그래프 구독을 해지하세요
        //
        // 주의: 람다식은 변수에 저장하지 않으면 해지할 수 없습니다
        //   해지가 필요한 구독은 람다식을 미리 변수에 저장한 뒤 등록하세요
        //   예시:
        //     Action<int, int> hpBarHandler = (oldHp, newHp) => { ... }
        //     player.OnHpChanged += hpBarHandler
        //     ... 나중에 ...
        //     player.OnHpChanged -= hpBarHandler
        player.OnHpChanged -= hpBarHandler;
        player.OnHpChanged -= battleLogHandler;
    }


}