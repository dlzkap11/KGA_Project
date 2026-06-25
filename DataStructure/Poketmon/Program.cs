namespace Poketmon
{
    internal class Program
    {
        public static Stack<string> PoketmonUI = new Stack<string>();
        public static Queue<string> queue = new Queue<string>();

        public enum UI
        {
            MainMenu,
            Battle,
            Bag,
            Poketmon,
            Run,

        }

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

            // MainMenu 반복
            // 포켓몬 등장
            Console.WriteLine("!야생의 포켓몬이 나타났다!");
            // 상대 포켓몬 상태
            // 내 포켓몬 상태
            // 선택지 UI
            Console.WriteLine("1)싸운다    2)가방");
            Console.WriteLine("3)포켓몬    4)도망친다");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    //싸운다
                    break;
                case "2":
                    //가방확인
                    break; 
                case "3": 
                    //포켓몬 확인
                    break; 
                case "4": 
                    //도망치기(종료)
                    break;
            }
            

        }
    }
}
