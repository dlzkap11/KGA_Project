namespace AlgorithmTest
{
    internal class Program
    {
        struct Node
        {
            public int Row;
            public int Col;
            public Node(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }

        }

        // 왼 오 아 위
        public static int[] dirR = { -1, 1, 0, 0 };
        public static int[] dirC = { 0, 0, 1, -1 };

        /*
       1. 특정 거리 이내에 도달 가능한 칸 세기
                같은 미로에서, 시작점 (0,0)으로부터 이동 거리가 K 이하인 칸이 몇 개인지 세는 함수를 만드세요.
                입력

                   Map: 
                       0 0 0 0 0
                       0 1 1 0 0
                       0 1 0 0 0
                       0 0 0 1 0
                       0 0 0 0 0

                   시작점: (0,0)
                   K = 4

                   출력 예시
                   거리 4 이하로 도달 가능한 칸 수: 11개
       */

        public static int[,] Map1 =
        {   //Map[y, x]
            { 0,0,0,0,0},
            { 0,1,1,0,0},
            { 0,1,0,0,0},
            { 0,0,0,1,0},
            { 0,0,0,0,0}
        };
        static int Solution1(int startR, int startC, int k)
        {
            int answer = 0;

            // 시작지점부터 한칸씩 이동한다.
            // 이동거리가 k가 되면 멈춤
            int mapRow = Map1.GetLength(0);
            int mapCol = Map1.GetLength(1);

            int[,] dist = new int[mapRow, mapCol];

            // 초기화
            for (int i = 0; i < mapRow; i++)
            {
                for (int j = 0; j < mapCol; j++)
                {
                    dist[i, j] = -1;
                }
            }
            dist[startR, startC] = 0;
            answer++; // 처음칸도 세기

            Queue<Node> queue = new Queue<Node>();
            // 시작 위치에서 한칸씩 상하좌우로 이동한다
            // 이동을 못하는 곳은 다음으로 넘기기

            queue.Enqueue(new Node(startR, startC));


            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();

                int currentR = currentNode.Row;
                int currentC = currentNode.Col;

                // 이미 k만큼 이동했으면 어차피 다음 칸을 볼 이유가 없으니 멈춘다
                if (dist[currentR, currentC] >= k)
                    break;

                for (int i = 0; i < 4; i++)
                {
                    int moveRow = currentR + dirR[i];
                    int moveCol = currentC + dirC[i];

                    if (moveRow < 0 || moveRow >= mapRow || moveCol < 0 || moveCol >= mapCol)
                        continue;

                    if (Map1[moveRow, moveCol] == 1 || dist[moveRow, moveCol] != -1)
                        continue;


                    dist[moveRow, moveCol] = dist[currentR, currentC] + 1;


                    if (dist[moveRow, moveCol] <= k)
                    {
                        answer++;
                        queue.Enqueue(new Node(moveRow, moveCol));
                    }
                    else
                        break;



                }
            }

            return answer;
        }


        /*
        2. 가장 가까운 출구 찾기
                 던전에 출구가 여러 개 있습니다. 
                 시작점에서 가장 가까운 출구까지의 최단 거리와 그 출구의 좌표를 구하세요.
        
                    입력
        
                    Map: 
                        0 0 0 0 0
                        0 1 1 0 0
                        0 1 0 0 0
                        0 0 0 1 0
                        0 0 0 0 0
        
                    시작점: (0,0)
                    출구 목록: (4,4), (3,4), (0,4)
        
                    출력 예시
                    가장 가까운 출구: (0,4)
                    거리: 4
        */
        public static int[,] Map2 =
        {   //Map[y, x]
            { 0,0,0,0,0},
            { 0,1,1,0,0},
            { 0,1,0,0,0},
            { 0,0,0,1,0},
            { 0,0,0,0,0}
        };

        static int Solution2(int startR, int startC, int[,] exits)
        {
            int mapRow = Map2.GetLength(0);
            int mapCol = Map2.GetLength(1);

            int[,] dist = new int[mapRow, mapCol];

            // 초기화
            for (int i = 0; i < mapRow; i++)
            {
                for (int j = 0; j < mapCol; j++)
                {
                    dist[i, j] = -1;
                }
            }
            dist[startR, startC] = 0;
            Queue<Node> queue = new Queue<Node>();
            // 시작 위치에서 한칸씩 상하좌우로 이동한다
            // 이동을 못하는 곳은 다음으로 넘기기

            queue.Enqueue(new Node(startR, startC));

            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();

                int currentR = currentNode.Row;
                int currentC = currentNode.Col;

                for (int i = 0; i < 4; i++)
                {
                    int moveRow = currentR + dirR[i];
                    int moveCol = currentC + dirC[i];

                    if (moveRow < 0 || moveRow >= mapRow || moveCol < 0 || moveCol >= mapCol)
                        continue;

                    if (Map2[moveRow, moveCol] == 1 || dist[moveRow, moveCol] != -1)
                        continue;

                    dist[moveRow, moveCol] = dist[currentR, currentC] + 1;
                    queue.Enqueue(new Node(moveRow, moveCol));



                }
            }

            Node minNode = new Node(1, 1);
            int min = int.MaxValue;
            for (int t = 0; t < exits.GetLength(0); t++)
            {
                int exitR = exits[t, 0];
                int exitC = exits[t, 1];

                if (dist[exitR, exitC] < min)
                {
                    min = dist[exitR, exitC];
                    minNode = new Node(exitR, exitC);
                }
            }

            Console.WriteLine($"가장 가까운 던전 : ({minNode.Row},{minNode.Col})");
            return min;
        }
        /*
        3. 독립된 밭 구역 개수 세기
                농장에 작물이 심어진 칸(1)과 빈 땅(0)이 섞여 있습니다. 
                서로 붙어있는 작물 칸들을 하나의 "밭 구역"으로 볼 때, 
                독립된 밭 구역이 몇 개인지 세는 함수를 만드세요. 
                입력 (1=작물, 0=빈 땅)
        
                   1 1 0 0 1
                   0 1 0 0 1
                   0 0 0 1 1
                   1 0 0 1 0
                   1 1 0 0 1
        
                   출력 예시
                   독립된 밭 구역 개수: 4개
       */
        public static int[,] Map3 =
        {   //Map[y, x]
            { 1,1,0,0,1},
            { 0,1,0,0,1},
            { 0,0,0,1,1},
            { 1,0,0,1,0},
            { 1,1,0,0,1}
        };

        // 일단 작물이 심어졌는지 확인 없으면 다음 칸으로 이동
        // 있으면 해당 지점부터 한칸씩 탐색하면서 작물을 찾기
        // 찾으면 방문true
        // 더 이상 찾을 수 없으면 answer++ 밭 하나
        // 다음 칸으로 이동
        // 위를 반복

        // DFS로 하면 바로 가능
        static int Solution3()
        {
            int answer = 0;

            int mapRow = Map3.GetLength(0);
            int mapCol = Map3.GetLength(1);

            int[,] dist = new int[mapRow, mapCol];

            // 초기화
            for (int i = 0; i < mapRow; i++)
            {
                for (int j = 0; j < mapCol; j++)
                {
                    dist[i, j] = -1;
                }
            }

            Queue<Node> queue = new Queue<Node>();
            // 시작 위치에서 한칸씩 상하좌우로 이동한다
            // 이동을 못하는 곳은 다음으로 넘기기

            //queue.Enqueue(new Node(0, 0));

            for (int i = 0; i < mapRow; i++)
            {
                for (int j = 0; j < mapCol; j++)
                {
                    queue.Enqueue(new Node(i, j));
                    while (queue.Count > 0)
                    {
                        Node currentNode = queue.Dequeue();

                        int currentR = currentNode.Row;
                        int currentC = currentNode.Col;

                        //작물이 있고 방문한적이 없는 경우
                        if (Map3[currentR, currentC] == 1 && dist[currentR, currentC] != 0)
                        {
                            dist[currentR, currentC]++;
                            answer++;
                        }
                        else
                        {
                            break;
                        }

                        for (int t = 0; t < 4; t++)
                        {
                            int moveRow = currentR + dirR[t];
                            int moveCol = currentC + dirC[t];

                            if (moveRow < 0 || moveRow >= mapRow || moveCol < 0 || moveCol >= mapCol)
                                continue;

                            if (Map3[moveRow, moveCol] == 0)
                                continue;
                            if (dist[moveRow, moveCol] != -1)
                                continue;

                            dist[moveRow, moveCol] = dist[moveRow, moveCol] + 1;
                            queue.Enqueue(new Node(moveRow, moveCol));
                            //answer--;
                        }
                    }
                    
                }
            }
            for (int i = 0; i < mapRow; i++)
            {
                for (int j = 0; j < mapCol; j++)
                {
                    Console.Write($"{dist[i, j],2}");
                }
                Console.WriteLine();
            }


            return answer;
        }
        /*
        4. 지형 비용을 고려한 최소 체력 소모 경로
                 지형마다 이동 비용이 다른 맵입니다. 
                 시작점에서 도착점까지 최소 체력 소모량을 구하세요.
        
                    입력 (숫자 = 이동 비용, 0=벽)
        
                    1 1 1 1 1
                    1 5 5 5 1
                    1 1 1 1 1
        
                    시작점: (0,0), 도착점: (2,4)
        
                    출력 예시
                    체력 소모량 : 6

         */
        public static int[,] Map4 =
        {   //Map[y, x]
            { 1,1,1,1,1},
            { 1,5,5,5,1},
            { 1,1,1,1,1}
        };
        static void Solution4()
        {

        }


        static void Main(string[] args)
        {
            int[,] e =
            {
                {4,4},
                {3,4},
                {0,4}
            };

            Console.WriteLine($"문제 1: {Solution1(0, 0, 4)}");
            Console.WriteLine();
            Console.WriteLine($"문제 2: {Solution2(0, 0, e)}");
            Console.WriteLine();
            Console.WriteLine($"문제 3: {Solution3()}");
        }
    }
}
