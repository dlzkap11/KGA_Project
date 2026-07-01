namespace Sort
{
    public static class Extension
    {
        // <선택 정렬>
        // 데이터 중 가장 작은 값부터 하나씩 '선택'해서 맨 앞에 두는 방식
        // 시간복잡도 -  O(n²)
        // 공간복잡도 -  O(1)
        public static void SelectionSort(this IList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;
                for (int j = i; j < list.Count; j++)
                {
                    if (list[j] < list[minIndex]) minIndex = j;
                }

                int temp = list[minIndex];
                list[minIndex] = list[i];
                list[i] = temp;
            }
        }

        // <삽입 정렬>
        // 데이터를 하나씩 꺼내어 정렬된 자료 중 적합한 위치에 '삽입'하여 정렬
        // 시간복잡도 -  O(n²)
        // 공간복잡도 -  O(1)
        public static void InsertionSort(this IList<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                // 1. 데이터를 하나 꺼낸다.
                int target = list[i];
                // 2. 정렬된 자료 중 적합한 위치에 삽입한다.
                // 2-3. 반복해서
                for (int j = i; j >= 0; j--)
                {
                    // 예외 상황 : 제일 작을 값이었을 때는 맨 앞에 배치
                    if (j == 0)
                    {
                        list[0] = target;
                    }
                    // 2-1. 앞 데이터와 비교해서
                    else if (list[j - 1] <= target)
                    {
                        // 더이상 안밀어내도 될때까지 이동한다.
                        list[j] = target;
                        break;
                    }
                    else
                    {
                        // 2-2. 더 작으면 뒤로 밀어내고
                        list[j] = list[j - 1];
                    }
                }


            }
        }

        // <버블 정렬>
        // 서로 인접한 데이터를 비교하여 정렬
        // 시간복잡도 -  O(n²)
        // 공간복잡도 -  O(1)
        public static void BubbleSort(this IList<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        int temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }

        // <병합 정렬>
        // 데이터를 2분할하여 정렬 후 합치는 방식으로 동작
        // 시간복잡도 -  O(nlogn)
        // 공간복잡도 -  O(n)
        public static void MergeSort(this IList<int> list) => MergeSort(list, 0, list.Count - 1);
        private static void MergeSort(IList<int> list, int start, int end)
        {
            if (start == end)
                return;

            int middle = (start + end) / 2;
            MergeSort(list, start, middle);
            MergeSort(list, middle + 1, end);
            Merge(list, start, middle, end);
        }

        private static void Merge(IList<int> list, int start, int middle, int end)
        {
            List<int> sortedList = new List<int>();
            int left = start;
            int right = middle + 1;

            while (left <= middle && right <= end)
            {
                if (list[left] < list[right])
                {
                    sortedList.Add(list[left]);
                    left++;
                }
                else
                {
                    sortedList.Add(list[right]);
                    right++;
                }
            }

            if (left > middle)
            {
                for (int i = right; i <= end; i++)
                {
                    sortedList.Add(list[i]);
                }
            }
            else // if (right > end)
            {
                for (int i = left; i <= middle; i++)
                {
                    sortedList.Add(list[i]);
                }
            }

            for (int i = 0; i < sortedList.Count; i++)
            {
                list[start + i] = sortedList[i];
            }
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int count = 10;

            List<int> selectionList = new List<int>();
            List<int> insertionList = new List<int>();
            List<int> bubbleList = new List<int>();
            List<int> mergeList = new List<int>();
            List<int> quickList = new List<int>();

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

        }
    }
}