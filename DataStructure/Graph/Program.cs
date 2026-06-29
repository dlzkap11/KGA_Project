namespace Graph
{
    internal class Program
    {
        const int MAP_SIZE = 8;
        const int MIRO_SIZE = 12;

        static void MapGraph()
        {
            //인접 행렬 그래프
            //2차원 배열 [출발정점, 도착정점]
            //가중치 = int, 아니면 = bool형으로 
            //현재위치 확인 가능
            //이동시 연결된 부분으로만 이동가능 -> 어디로 이동가능한지도 알려주기
            bool[,] maps = new bool[MAP_SIZE, MAP_SIZE];
            maps[0, 1] = true;
            maps[0, 2] = true;
            maps[0, 3] = true;

            maps[1, 0] = true;
            maps[1, 2] = true;
            maps[1, 6] = true;

            maps[2, 0] = true;
            maps[2, 1] = true;
            maps[2, 4] = true;
            maps[2, 5] = true;

            maps[3, 0] = true;
            maps[3, 7] = true;

            maps[4, 2] = true;
            maps[4, 6] = true;
            maps[4, 7] = true;

            maps[5, 2] = true;
            maps[5, 6] = true;
            maps[5, 7] = true;

            maps[6, 1] = true;
            maps[6, 4] = true;

            maps[7, 3] = true;
            maps[7, 4] = true;
            maps[7, 5] = true;

            string[] mapName = { "마을", "숲", "초원", "성", "마왕성", "바다", "던전", "산" };


            int input;
            int player = 0;
            int[] temp = new int[MAP_SIZE];
            while (true)
            {
                int cnt = 0;
                Console.WriteLine($"현재 위치 : {mapName[player]}");

                for (int Dest = 0; Dest < MAP_SIZE; Dest++)
                {
                    if (maps[player, Dest])
                    {
                        Console.Write($"{cnt + 1}){mapName[Dest]} ");
                        temp[cnt] = Dest;
                        cnt++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("어디로 이동하시겠습니까?");
                input = int.Parse(Console.ReadLine());
                if (input >= MAP_SIZE || input <= 0)
                {
                    Console.WriteLine("미확인 지역입니다.");
                    continue;
                }

                // cnt로 나오는 번호와 실제 입력번호의 결과값이 다른문제
                // 마을은 0을 눌러야 갈 수 있는데 cnt는 1로 나오는 경우
                // 마왕성은 4를 눌러야 갈 수 있는데 cnt는 3으로 나오는 경우 등등
                // 마을 위치는 고정인데 cnt는 변화하면서 생기는 문제
                // 현재 위치 숲
                // 1) 마을 2) 초원 3)평원
                // 0 입력하면 마을 1 입력하면 숲(같은위치) 2 초원 3 .... 6 평원

                int dest = input - 1;
                if (!maps[player, temp[dest]])
                {
                    if (player == temp[dest])
                        Console.WriteLine("현재 위치한 장소입니다.");
                    else
                        Console.WriteLine($"{mapName[temp[dest]]}으로는 갈 수 없습니다.");
                }
                else
                {
                    Console.WriteLine($"{mapName[temp[dest]]}으로 이동!");
                    player = temp[dest];
                }
            }

        }


        static void Miro()
        {
            // 미로찾기(12*12)
            // 김미로씨 찾기
            // 벽은 # 이동가능은 ' '
            // 플레이어는 P,
            // 도착지는 E
            // 맵은 랜덤으로 출력하기
            // 플레이어는 상하좌우로 이동가능
            Console.Clear();

            char[,] miro = new char[MIRO_SIZE, MIRO_SIZE];

            for (int i = 0; i < MIRO_SIZE; i++)
            {
                for (int j = 0; j < MIRO_SIZE; j++)
                {
                    miro[i, j] = '#';
                }
            }

            // 랜덤으로 길뚫기
            // 서순
            // 1. 일단 P -> E로 가는 길을 하나 뚫는다.
            // 2. 처음부터 반복문을 돌리면서 이미 뚫린 길' '은 무시 벽'#'은 확률로 길이 뚫림
            // 완성 짠
            // P -> E로 가는 길
            // P(0,0) E(11,11) 한칸씩 가면서 뚫기 오른쪽 또는 아래로 뚫기 만약 오른쪽으로 가다가 최대 사이즈에 닿으면 아래로 아래로 갈 때도 최대 사이즈에 닿으면 오른쪽으로 가기 하면 길 뚫린다
            int dotheg_x = 0, dotheg_y = 0;
            while (true)
            {
                miro[dotheg_y, dotheg_x] = ' ';
                Random rand = new Random();
                int i = rand.Next(0, 2);
                if(i == 0)
                {
                    //0이면 오른쪽
                    if (dotheg_x + 1 >= MIRO_SIZE)
                        continue;
                    dotheg_x++;
                }
                else
                {
                    //1이면 아래로
                    if (dotheg_y + 1 >= MIRO_SIZE)
                        continue;
                    dotheg_y++;
                }
                miro[dotheg_y, dotheg_x] = ' ';

                if (dotheg_x == MIRO_SIZE - 1 && dotheg_y == MIRO_SIZE - 1)
                    break;
            }

            for (int i = 0; i < MIRO_SIZE; i++)
            {
                for (int j = 0; j < MIRO_SIZE; j++)
                {
                    if (miro[i, j] == ' ')
                        continue;

                    Random rand = new Random();
                    int r = rand.Next(0, 2);
                    if (r == 0)
                        miro[i, j] = '#';
                    else
                        miro[i, j] = ' ';

                }
            }


            int player_y = 0;
            int player_x = 0;
            miro[player_y, player_x] = 'P';
            miro[MIRO_SIZE - 1, MIRO_SIZE - 1] = 'E';

            int[,] pos = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };


            while (true)
            {
                Console.Clear();
                for (int i = 0; i < MIRO_SIZE; i++)
                {
                    for (int j = 0; j < MIRO_SIZE; j++)
                    {
                        Console.Write($"{miro[i, j]}");
                    }
                    Console.WriteLine();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    if (player_y - 1 < 0 || player_y - 1 >= MIRO_SIZE)
                        continue;

                    if (miro[player_y - 1, player_x] == '#')
                        continue;
                    else if(miro[player_y - 1, player_x] == 'E')
                    {
                        Console.WriteLine("도착!");
                        break;
                    }
                    miro[player_y, player_x] = ' ';
                    player_y--;
                    miro[player_y, player_x] = 'P';
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    if (player_y + 1 < 0 || player_y + 1 >= MIRO_SIZE)
                        continue;

                    if (miro[player_y + 1, player_x] == '#')
                        continue;
                    else if (miro[player_y + 1, player_x] == 'E')
                    {
                        Console.WriteLine("도착!");
                        break;
                    }

                    miro[player_y, player_x] = ' ';
                    player_y++;
                    miro[player_y, player_x] = 'P';
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (player_x - 1 < 0 || player_x - 1 >= MIRO_SIZE)
                        continue;

                    if (miro[player_y, player_x - 1] == '#')
                        continue;
                    else if (miro[player_y, player_x - 1] == 'E')
                    {
                        Console.WriteLine("도착!");
                        break;
                    }

                    miro[player_y, player_x] = ' ';
                    player_x--;
                    miro[player_y, player_x] = 'P';
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (player_x + 1 < 0 || player_x + 1 >= MIRO_SIZE)
                        continue;

                    if (miro[player_y, player_x + 1] == '#')
                        continue;
                    else if (miro[player_y, player_x + 1] == 'E')
                    {
                        Console.WriteLine("도착!");
                        break;
                    }

                    miro[player_y, player_x] = ' ';
                    player_x++;
                    miro[player_y, player_x] = 'P';
                }

            }

        }


        static void MovePos()
        {

        }


        static void Main(string[] args)
        {
            Console.WriteLine("고르시오");
            Console.WriteLine("1) 길찾기 2) 미로찾기");
            string input = Console.ReadLine();
            if (input == "1")
            {
                MapGraph();
            }
            else if (input == "2")
            {
                Miro();
            }
        }

    }

}
