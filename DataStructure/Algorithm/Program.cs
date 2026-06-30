using System.Diagnostics;
using System.Numerics;

namespace Algorithm
{

    public static class Extended
    {
        //최솟값과 최대값을 제외한 평균 점수를 구하시오
        public static float GetAverage(this IEnumerable<int> container)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            int sum = 0;
            int cnt = 0;
            foreach (int item in container)
            {
                sum += item;
                min = Math.Min(min, item);
                max = Math.Max(max, item);
                cnt++;
            }

            sum -= (min + max);

            return sum / (cnt - 2);
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            //알고리즘
            //주어진 리스트에서 가장 작은값 두개 구하기
            List<int> list = new List<int> { 11, 17, 33, 27, 50, 7, 22, 43 };


            Console.WriteLine($"최소값과 최대값을 제외한 평균 점수 : {list.GetAverage}");
            //Console.WriteLine($"최소값과 최대값을 제외한 평균 점수 : {GetAverage(list)}");
        }

        


    }
}