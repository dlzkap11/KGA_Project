using Algorithm;
using System.Data;

namespace Sort
{
    

    public static class Extension
    {
        //모든 정렬은 오름차순을 기준으로

        // 선택정렬
        // 가장 작은 값을 찾고 앞에다가 넣기를 반복해서 정렬하는 방식
        public static void SelectionSort(this IList<int> list)
        {
            int cnt = 0;
            while (cnt < list.Count)
            {
                int minIndex = cnt;
                for (int i = cnt + 1; i < list.Count; i++)
                {
                    if (list[i] < list[minIndex])
                    {
                        minIndex = i;
                    }
                }
                int temp = list[cnt];
                list[cnt] = list[minIndex];
                list[minIndex] = temp;
                cnt++; // 다음 자리로 밀어주기
            }
        }


        //삽입정렬
        // 첫번째 값이랑 두번째 값을 비교해서 더 두번째값이 더크면 첫번째값의 오른쪽 작으면 왼쪽으로 넣으면서 정렬하는 방식
        // 이후 3번째 값이랑 두번째값도 똑같이 비교 후 왼쪽에 있는 값이 크면 다음으로 오른쪽 값이 크면 둘의 위치를 바꾸기
        // 2 6 8 4 3 1
        // 2 6 4 8 3 1 // 여기서 반복할 수 있게 해야함
        // 2 6 4 3 8 1
        // 2 6 4 3 1 8
        
        public static void InsertionSort(this IList<int> list)
        {
            for(int i = 1;  i < list.Count; i++)
            {
                // 오른쪽 값이 왼쪽보다 크면 다음으로
                for (int j = i; j > 0; j--)
                {
                    if (list[j - 1] < list[j])
                    {
                        break;
                    }
                    else
                    {
                        int temp = list[j];
                        list[j] = list[j - 1];
                        list[j - 1] = temp;

                    }
                }
            }
        }
        
        
        //버블정렬
        // 서로 인접한 데이터끼리 비교해서 정렬
        // 순차적으로 돌면서 두 데이터를 비교해서 왼쪽이 크면 바꾸고 아니면 그냥 다음으로 가고 이런식
        // 처음 돌 때 가장 큰 값이 뒤로 가야함
        public static void BubbleSort(this IList<int> list)
        {
            for(int j = 1; j < list.Count; j++)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    if (list[i - 1] > list[i])
                    {
                        int temp = list[i];
                        list[i] = list[i - 1];
                        list[i - 1] = temp;
                    }
                }
            }

        }


        //퀵정렬
        public static void QuickSort(this IList<int> list) => QuickSort(list, 0, list.Count - 1);
        private static void QuickSort(IList<int> list, int start, int end)
        { 
        }
        //힙정렬
        public static void HeapSort(IList<int> list)
        {

        }

        //병합정렬
        //배열 내 값들을 분할해서 각각 정렬하면서 다시 합치는 정렬 방법
        // 일단 쪼개기 -> 더이상 못쪼갤때까지 배열길이가 1이 될 때까지
        // 더이상 못 쪼개면 값들을 비교하면서 정렬
        // 정렬된 리스트를 원래 배열로 보내주기
        public static void MergeSort(this IList<int> list) => MergeSort(list, 0, list.Count - 1);
        private static void MergeSort(IList<int> list, int start, int end)
        {
            
            if (start == end)
                return;

            int middle;
            middle = (start + end) / 2;

            MergeSort(list, start, middle);
            MergeSort(list, end, middle);
            Merge(list, start, middle, end);
        }

        private static void Merge(IList<int> list, int start, int middle, int end)
        {
            List<int> temp = new List<int>();

            int left = 0;
            int right = 0;

            while (left < middle && right < end)
            {
                if (list[start + left] < list[middle + right])
                {
                    temp.Add(list[start + left]);
                    left++;
                }
                else
                {
                    temp.Add(list[middle + right]);
                    right++;
                }
            }

            //left right중 남은 것들 넣기
            
            //while ()
            {
                if (left < middle)
                {
                    temp.Add(list[start + left]);
                    left++;
                }
                else
                {
                    temp.Add(list[middle + right]);
                    right++;
                }
            }
            

            
            for(int i = 0; i < list.Count; i++)
            {
                list[start + i] = temp[i];
            }

        }

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
            { 0,0,0,0,0},
            { 0,1,1,1,0},
            { 0,1,0,0,0},
            { 0,1,0,1,1},
            { 0,1,0,0,0}
        };
        // 왼 오 아 위
        public static int[] directRow = { -1, 1, 0, 0 };
        public static int[] directColumn = { 0, 0, 1, -1 };

        static bool[,] isVisited;

        static int BFSPath(int startR, int startC, int endR, int endC)
        {
            // 시작점에서 도착점까지의 최단거리를 저장해두기
            // 한칸씩 탐색하면서 현재까지의 거리 저장
            // 도착하면 해당 변수의 내용이 최단거리고 그걸 리턴하면 되나?

            

            //맵 크기
            int mapRows = Map.GetLength(0);
            int mapCols = Map.GetLength(1);

            // 최단거리 방문체크 = -1, 못가는 곳(벽) = INF 거리 = 0++
            int[,] dist = new int[mapRows, mapCols];
            for (int i = 0; i < mapRows; i++) 
            {
                for (int j = 0; j < mapCols; j++) 
                {
                    dist[i, j] = -1;
                }
            }

            Queue<Node> bfsQueue = new Queue<Node>();
            bfsQueue.Enqueue(new Node(startR ,startC));
            dist[startR, startC] = 0;

            while(bfsQueue.Count > 0)
            {
                Node currentNode = bfsQueue.Dequeue();

                int currentRow = currentNode.Row;
                int currentCol = currentNode.Col;

                if(currentRow == endR && currentCol == endC)
                    return dist[currentRow, currentCol];

                for(int i = 0; i < 4; i++)
                {
                    int moveRow = currentRow + directRow[i];
                    int moveCol = currentCol + directColumn[i];

                    if (moveRow < 0 || moveCol < 0 || moveRow >= mapRows|| moveCol >= mapCols)
                        continue;
                    if (Map[moveRow, moveCol] == 1 || dist[moveRow, moveCol] != -1)
                        continue;

                    dist[moveRow, moveCol] = dist[currentRow, currentCol] + 1;
                    bfsQueue.Enqueue(new Node(moveRow, moveCol));

                }

            }


            return -1;
        }

        static List<Node> BFSFindPath(int startR, int startC, int endR, int endC)
        {
            // 맵의 크기를 캐싱해둔다.
            int mapRows = Map.GetLength(0);
            int mapCols = Map.GetLength(1);

            //거리를 저장하는 배열
            int[,] dist = new int[mapRows, mapCols];

            // 경로를 역추적하기 위해서 이동 직전에 있었던 좌표를 저장
            Node[,] prevNodes = new Node[mapRows, mapCols];

            // 초기화
            for (int i = 0; i < mapRows; i++)
            {
                for (int j = 0; j < mapCols; j++)
                {
                    dist[i, j] = -1;
                    // 이전에는 최단거리를 저장하는 배열만 초기화 했다면, 이전 경로를 저장하는 배열도 초기화
                    prevNodes[i, j] = new Node(-1, -1);
                }
            }
            Queue<Node> bfsQueue = new Queue<Node>();
            bfsQueue.Enqueue(new Node(startR, startC));
            dist[startR, startC] = 0;

            while (bfsQueue.Count > 0)
            {
                Node currentNode = bfsQueue.Dequeue();
                int currentRow = currentNode.Row;
                int currentCol = currentNode.Col;

                for (int i = 0; i < 4; i++)
                {
                    int moveRow = currentRow + directRow[i];
                    int moveCol = currentCol + directColumn[i];

                    if (moveRow < 0 || moveRow >= mapRows || moveCol < 0 || moveCol >= mapCols)
                        continue;

                    if (Map[moveRow, moveCol] == 1)
                        continue;

                    if (dist[moveRow, moveCol] != -1)
                        continue;

                    dist[moveRow, moveCol] = dist[moveRow, moveCol] + 1;

                    // 이전 경로까지 여기에서 저장을 해준다.
                    // 현재 moveRow, moveCol 같은 경우 current에서 상,하,좌,우로 움직인 상황
                    // 그렇다면, 이동하려고 하는 위치에, 현재 위치 경로를 저장해두면, 경로를 저장해둔것과 같다.
                    prevNodes[moveRow, moveCol] = new Node(currentRow, currentCol);

                    bfsQueue.Enqueue(new Node(moveRow, moveCol));
                }
            }

            // 도착점의 거리가 -1 == 제대로 도달하지 못한경우.
            // null 을 반환하여 제대로된 경로가 아님을 알려주어야함. 
            if (dist[endR, endC] == -1)
                return null;

            // 여기서부터는 prevNodes 라고하는 경로를 저장한 배열을 가지고 경로 추적을 해주면됨.
            // 역추적해서 결과를 담아서 반환할 리스트
            List<Node> path = new List<Node>();

            Node current = new Node(endC, endR);


            // 맵의 끝까지 도달했을때 종료.
            while (current.Row != -1)
            {
                // 역추적은 도착점 -> 시작점 순서로 진행
                // 사람들이 보기엔 시작점 -> 도착점이 자연스러우므로
                // 앞쪽부터 넣어서 마지막이 끝이 되도록
                path.Insert(0, current);

                // 현재 좌표에서 이전좌표로 이동시킨다.
                current = prevNodes[current.Row, current.Col];
            }
            return path;
        }

        static List<Node> BFSFindPath2(int startR, int startC, int endR, int endC)
        {
            //맵 크기
            int mapRows = Map.GetLength(0);
            int mapCols = Map.GetLength(1);

            int[,] dist = new int[mapRows, mapCols];
            for (int i = 0; i < mapRows; i++)
            {
                for (int j = 0; j < mapCols; j++)
                {
                    dist[i, j] = -1;
                }
            }

            Stack<Node> bfsStack = new Stack<Node>();
            List<Node> way = new List<Node>();
            bfsStack.Push(new Node(endR, endC));
            dist[endR, endC] = BFSPath(startR, startC, endR, endC); // 최단거리

            //bfsStack.Push(new Node(startR, startC));
            //dist[startR, startC] = 0;
            while (bfsStack.Count > 0)
            {
                Node currentNode = bfsStack.Pop();
                way.Add(currentNode);
                int currentRow = currentNode.Row;
                int currentCol = currentNode.Col;

                if (currentRow == startR && currentCol == startC)
                {
                    way.Reverse();
                    return way;
                }
                    

                for (int i = 0; i < 4; i++)
                {
                    int moveRow = currentRow + directRow[i];
                    int moveCol = currentCol + directColumn[i];

                    if (moveRow < 0 || moveCol < 0 || moveRow >= mapRows || moveCol >= mapCols)
                        continue;
                    if (Map[moveRow, moveCol] == 1 || dist[moveRow, moveCol] != -1)
                        continue;

                    dist[moveRow, moveCol] = dist[moveRow, moveCol] - 1;
                    bfsStack.Push(new Node(moveRow, moveCol));
                    
                }

            }
            // 애초에 여기까지 왔다 = 길이 없다
            /*
            if (dist[startR, startC] == -1)
                return null;
            */

            return null;
            // 그러면 찾고 역으로 돌아가기를 하면 되지 않을까?
            // 그래서 스택을 생각하고 있기는 함
            // 그러면 스택으로 최단거리 루트를 하나씩 먹기



            // 갈 수 있는지 확인 방문한 곳인지, 벽인지 맵 크기


            // 다시 뱉어내면서 경로 알려주기




        }

        public static bool DFSRecursive(int startR, int startC, int endR, int endC)
        {
            // 도착점에 도착했을때 재귀가 끝나는 종료 조건
            if (startR == endR && startC == endC)
            {
                return true;
            }

            // 초기 값을 세팅해야함.
            // 초기 위치를 방문했다는 초기화를 진행
            // 여기에서 만약 해당 과정을 진행하지 않는 경우에 재귀로 이어지기 때문에 무한루프가 될 수도 있음.
            isVisited[startR, startC] = true;

            for (int i = 0; i < 4; i++)
            {

                int moveRow = startR + directRow[i];
                int moveCol = startC + directColumn[i];

                if (moveRow < 0 || moveRow >= Map.GetLength(0) || moveCol < 0 || moveCol >= Map.GetLength(1))
                    continue;

                if (Map[moveRow, moveCol] == 1)
                    continue;

                if (isVisited[moveRow, moveCol])
                    continue;

                if (DFSRecursive(moveRow, moveCol, endR, endC))
                    return true;

                // 자동으로 다음 방향을 확인한다.
            }

            return false;
        }

        static void Main(string[] args)
        {
            int result = BFSPath(0, 0, 4, 4);
            Console.WriteLine($"도착지점까지의 최단거리 {result}");

            List<Node> node = BFSFindPath(0, 0, 4, 4);
            if(node != null)
            {
                for (int i = 0; i < node.Count - 1; i++)
                {
                    Console.WriteLine($"{node[i].Row}, {node[i].Col} ===> {node[i + 1].Row}, {node[i + 1].Col}");
                }
            }
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            List<Node> node2 = BFSFindPath2(0, 0, 4, 4);
            if( node2 != null)
            {
                for (int i = 0; i < node2.Count - 1; i++)
                {
                    Console.WriteLine($"{node2[i].Row}, {node2[i].Col} ===> {node2[i + 1].Row}, {node2[i + 1].Col}");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            int mapRows = Map.GetLength(0);
            int mapCols = Map.GetLength(1);

            isVisited = new bool[mapRows, mapCols];
            bool isReached = DFSRecursive(0, 0, 4, 4);
            Console.WriteLine($"해당 위치에 도달할 수 있는지 : {isReached}");

            /*
            Random random = new Random();
            int count = 10;

            List<int> selectionList = new List<int>();
            List<int> insertionList = new List<int>();
            List<int> bubbleList = new List<int>();
            List<int> mergeList = new List<int>();
            List<int> quickList = new List<int>();
            List<int> heapList = new List<int>();

            Console.WriteLine("랜덤 데이터 : ");
            for (int i = 0; i < count; i++)
            {
                int rand = random.Next() % 99 + 1;
                Console.Write($"{rand,3}");

                selectionList.Add(rand);
                insertionList.Add(rand);
                bubbleList.Add(rand);
                mergeList.Add(rand);
                quickList.Add(rand);
                heapList.Add(rand);
            }
            Console.WriteLine();
            Console.WriteLine();

            selectionList.SelectionSort();
            Console.WriteLine("선택 정렬 결과 : ");
            foreach (int value in selectionList)
            {
                Console.Write($"{value,3}");
            }
            Console.WriteLine();

            insertionList.InsertionSort();
            Console.WriteLine("삽입 정렬 결과 : ");
            foreach (int value in insertionList)
            {
                Console.Write($"{value,3}");
            }
            Console.WriteLine();

            bubbleList.BubbleSort();
            Console.WriteLine("버블 정렬 결과 : ");
            foreach (int value in bubbleList)
            {
                Console.Write($"{value,3}");
            }
            Console.WriteLine();

            mergeList.MergeSort();
            Console.WriteLine("병합 정렬 결과 : ");
            foreach (int value in mergeList)
            {
                Console.Write($"{value,3}");
            }
            Console.WriteLine();

            quickList.QuickSort();
            Console.WriteLine("퀵 정렬 결과 : ");
            foreach (int value in quickList)
            {
                Console.Write($"{value,3}");
            }
            Console.WriteLine();

            heapList.QuickSort();
            Console.WriteLine("힙 정렬 결과 : ");
            foreach (int value in heapList)
            {
                Console.Write($"{value,3}");
            }
            Console.WriteLine();

            List<Monster> monsters = new List<Monster>();
            monsters.Add(new Monster("왕뚜껑", 2, "라면"));
            monsters.Add(new Monster("킹뚜껑", 5, "라면"));
            monsters.Add(new Monster("밥아저씨", 18, "사람"));
            monsters.Add(new Monster("그랜드마더", 12, "노인"));



            SortWork.SortByAttribute(monsters);
            Console.WriteLine("속성기준 정렬");
            foreach (Monster monster in monsters)
            {
                Console.Write($"{monster.Name}, {monster.Level}, {monster.Attribute}");
                Console.WriteLine();
            }
            Console.WriteLine("\n레벨기준 정렬");
            SortWork.SortByLevel(monsters);
            foreach (Monster monster in monsters)
            {
                Console.Write($"{monster.Name}, {monster.Level}, {monster.Attribute}");
                Console.WriteLine();
            }
            Console.WriteLine("\n이름기준 정렬");
            SortWork.SortByName(monsters);
            foreach (Monster monster in monsters)
            {
                Console.Write($"{monster.Name}, {monster.Level}, {monster.Attribute}");
                Console.WriteLine();
            }
            Console.WriteLine();


            */
        }
    }
}