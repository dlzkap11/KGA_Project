namespace Sort
{


    public static class Extension
    {

    }
    public class Program
    {

        public struct Node
        {
            public int Row;
            public int Col;
            public Node(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }

        }

        public static int[,] Map =
        {   //Map[y, x]
            { 1,1,0,5,1},
            { 5,1,1,1,10},
            { 1,1,10,0,1},
            { 1,5,1,5,1},
            { 0,1,5,1,1}
        };
        // 왼 오 아 위
        public static int[] directR = { -1, 1, 0, 0 };
        public static int[] directC = { 0, 0, 1, -1 };

        static int[,] Dijkstra(int[,] Map, int startR, int startC)
        {
            // 맵의 크기
            int mapRows = Map.GetLength(0);
            int mapCols = Map.GetLength(1);

            // 시작지점부터의 이동비용를 나타낸 배열
            int[,] dist = new int[mapRows, mapCols];

            // 아직 해당 위치를 확인하지 못했다 == 도달 불가능
            // 무한대. INF로 초기화
            for (int i = 0; i < mapRows; i++)
            {
                for (int j = 0; j < mapCols; j++)
                {
                    dist[i, j] = int.MaxValue;
                }
            }

            // 기존의 INF 지정을 해두었을때, 이게 실제로 도달불가능인지, 내가 아직 방문하지 못했으므로 도달불가능인지
            // 판단하기위해.
            bool[,] isVisited = new bool[mapRows, mapCols];

            // 시작 초기화
            // 시작점은 당연히 0으로 초기화 해준다.
            dist[startR, startC] = 0;

            // 칸 수 만큼 반복하여 모든 칸을 순회해야한다.
            // 하지만, 시작점은 이미 0으로 정해져 있으므로, 반복은 모든 칸 - 1 해준다.
            int totalCells = mapRows * mapCols;

            for (int i = 0; i < totalCells - 1; i++)
            {
                // 다익스트라 알고리즘
                // 아직 방문을 하지 않은 노드 중에서 가장 비용이 작은 노드를 먼저 확인한다.
                Node current = new Node(-1, -1);
                int minDist = int.MaxValue;
                for (int r = 0; r < mapRows; r++)
                {
                    for (int c = 0; c < mapCols; c++)
                    {
                        // 벽이거나 이미 방문한 노드는 제외
                        if (Map[r, c] == 0 || isVisited[r, c])
                            continue;

                        // 만약 현재 확인한 최소 비용보다 지금 확인한 노드의 비용이 작다면
                        if (dist[r, c] < minDist)
                        {
                            minDist = dist[r, c];
                            current = new Node(r, c);
                        }
                    }
                }

                // 후보를 하나도 못찾았을 경우,
                // 전부 벽이거나, 모두 방문완료했거나, 전부 비용 무한대라면
                // 시작점에서 더이상 도달할 수 있는 곳이 없다

                if (current.Row == -1 || dist[current.Row, current.Col] == int.MaxValue)
                    break;

                // 이 칸을 방문했음을 표시
                isVisited[current.Row, current.Col] = true;

                // 방문한 칸부터 이웃한 상하좌우를 이동하면서 확인한다.
                for (int j = 0; j < 4; j++)
                {
                    // 현재 위치에서 상하좌우 확인
                    int moveRow = current.Row + directR[j];
                    int moveCol = current.Col + directC[j];

                    // 범위를 벗어나거나,
                    if (moveRow < 0 || moveRow >= mapRows || moveCol < 0 || moveCol >= mapCols)
                        continue;

                    // 벽이거나,
                    if (Map[moveRow, moveCol] == 0)
                        continue;

                    // 이미 방문했거나,
                    if (isVisited[moveRow, moveCol])
                        continue;

                    // current를 거쳐서 지금 가려고 하는 위치(moveRow,moveCol)로 가는 비용을 계산.
                    int newDist = dist[current.Row, current.Col] + Map[moveRow, moveCol];

                    // 지금 새로 계산한 비용이 기존에 알고있던 비용보다 저렴하면
                    // 현재 계산 비용이 최소 비용으로 들어감
                    if (newDist < dist[moveRow, moveCol])
                    {
                        dist[moveRow, moveCol] = newDist;
                    }
                }
            }
            return dist;
        }


        static int[,] Dijkstra2(int[,] Map, int startR, int startC)
        {
            int mapRows = Map.GetLength(0);
            int mapCols = Map.GetLength(1);

            int[,] dist = new int[mapRows, mapCols];

            bool[,] isVisited = new bool[mapRows, mapCols];

            //무한대 뮤타입
            for (int i = 0; i < mapRows; i++)
            {
                for (int j = 0; j < mapCols; j++)
                    dist[i, j] = int.MaxValue;
            }

            dist[startR, startC] = 0;

            // 각 칸들의 가중치를 구하는 것 = 다 가야함 = 전체돌기
            int totalCells = mapRows * mapCols;

            for (int i = 0; i < totalCells - 1; i++)
            {
                Node current = new Node(-1, -1);
                int minDist = int.MaxValue;

                for (int r = 0; r < mapRows; r++)
                {
                    for (int c = 0; c < mapCols; c++)
                    {
                        if (Map[r, c] == 0 || isVisited[r, c])
                            continue;

                        if (dist[r, c] < minDist)
                        {
                            minDist = dist[r, c];
                            current = new Node(r, c);
                        }
                    }
                }

                if (current.Row == -1 || dist[current.Row, current.Col] == int.MaxValue)
                    break;

                isVisited[current.Row, current.Col] = true;

                for (int j = 0; j < 4; j++)
                {
                    int moveRow = current.Row + directR[j];
                    int moveCol = current.Col + directC[j];

                    if (moveRow < 0 || moveCol < 0 || moveRow >= Map.GetLength(0) || moveCol >= Map.GetLength(1))
                        continue;

                    // 이미방문했으면 continue
                    if (Map[moveRow, moveCol] == 0 || isVisited[moveRow, moveCol])
                        continue;

                    int newDist = Map[moveRow, moveCol] + dist[current.Row, current.Col];

                    if (newDist < dist[moveRow, moveCol])
                    {
                        dist[moveRow, moveCol] = newDist;
                    }
                }

            }
            return dist;
        }


        static void Main(string[] args)
        {
            int[,] where = Dijkstra(Map, 0, 1);
            int[,] where2 = Dijkstra2(Map, 0, 1);

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Console.Write($"{(where[i, j] == int.MaxValue ? -1 : where[i, j]),3}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Console.Write($"{(where2[i, j] == int.MaxValue ? -1 : where2[i, j]),3}");
                }
                Console.WriteLine();
            }
        }
    }
}