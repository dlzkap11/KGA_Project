

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

class Shopping
{

    // IProduct, IDiscountable 인터페이스를 직접 설계한다.
    // Food, Electronics 클래스로 인터페이스를 구현한다.
    // Cart<T> where T : IProduct 제네릭 장바구니를 만든다.
    // 전체를 조합해서 결제 시뮬레이션을 완성한다.

    interface IProduct
    {
        void DisplayInfo();      // 상품 정보 출력
        int GetPrice();         // 정가 반환
        string GetName();        // 상품명 반환
    }

    interface IDiscountable
    {
        int GetDiscountRate();        // 할인율 반환 (0~100)
        int GetDiscountedPrice();     // 할인가 반환
    }

    class Food : IProduct, IDiscountable
    {
        // 필드: 상품명, 정가, 할인율, 유통기한
        private string name;
        private int price;
        private int discountRate;
        private int expirationDate;


        // 생성자
        public Food(string _name, int _price, int _discountRate, int _expirationDate)
        {
            name = _name;
            price = _price;
            discountRate = _discountRate;
            expirationDate = _expirationDate;
        }



        //상품명 반환
        public string GetName()
        {
            //return $"상품명 : {name}";
            return name;
        }
        //정가 반환
        public int GetPrice()
        {
            return price;
        }

        //유통기한 반환등등...

        //"[식품] 상품명 / 정가 / 유통기한" 형식으로 출력
        public void DisplayInfo()
        {
            Console.Write($"[식품] 상품명 : {name} / 정가 : {price} / 유통기한 : {expirationDate}");
        }


        //할인율 반환
        public int GetDiscountRate()
        {
            return discountRate;
        }

        //정가 * (100 - 할인율) / 100 반환
        public int GetDiscountedPrice()
        {
            return price * (100 - discountRate) / 100;
        }
    }

    class Electronics : IProduct
    {
        // 필드: 상품명, 정가, 브랜드
        private string name;
        private int price;
        private string brand;

        // 생성자
        public Electronics(string _name, int _price, string _brand)
        {
            name = _name; price= _price; brand= _brand;
        }

        //상품명 반환
        public string GetName()
        {
            //return $"상품명 : {name}";
            return name;
        }
        //정가 반환
        public int GetPrice()
        {
            return price;
        }

        //"[전자제품] 상품명 / 정가 / 브랜드" 형식으로 출력
        public void DisplayInfo()
        {
            Console.WriteLine($"[전자제품] 상품명 : {name} / 정가 : {price} / 브랜드 : {brand}");
        }
    }
    
    class Cart<T> where T : IProduct
    {
        // 필드: List<T> _items
        List<T> _items = new List<T>();
        // Add(T item)    : _items에 추가, "{상품명} 장바구니 담김" 출력
        public void Add(T item)
        {
            _items.Add(item);
            Console.WriteLine($"{item.GetName()} 장바구니 담김");
        }

        // Remove(T item) : _items에서 제거, "{상품명} 장바구니 제거" 출력
        public void Remove(T item)
        {
            _items.Remove(item);
            Console.WriteLine($"{item.GetName()} 장바구니 제거");
        }


        // GetTotalPrice() : 전체 정가 합산 반환
        //   → item.GetPrice() 사용
        public int GetTotalPrice()
        {
            int total = 0;
            foreach(T item in _items)
            {
                total += item.GetPrice();
            }

            return total;
        }



        // GetFinalPrice() : 할인가 합산 반환
        //   → IDiscountable이면 GetDiscountedPrice() 사용
        //   → 아니면 GetPrice() 사용
        //   → is 패턴 활용: if (item is IDiscountable d)
        public int GetFinalPrice()
        {
            int total = 0;
            foreach (T item in _items)
            {
                if(item is IDiscountable d)
                    total += d.GetDiscountedPrice();
                else
                    total += item.GetPrice();
            }

            return total;
        }




        // PrintReceipt() : 영수증 출력
        //   → 각 아이템 DisplayInfo() 호출
        //   → IDiscountable이면 할인가, 할인율 함께 출력
        //   → 정가 합계 / 최종 결제액 / 총 할인액 출력

        public void PrintReceipt()
        {
            foreach (T item in _items)
            {
                item.DisplayInfo();

                if(item is IDiscountable d)
                {
                    Console.WriteLine($" / 할인가 : {d.GetDiscountedPrice()} / 할인율 : {d.GetDiscountRate()}");
                }
            }
            Console.WriteLine($"정가 합계 : {GetTotalPrice()} / 최종 결제액 : {GetFinalPrice()} / 총 할인액 : {GetTotalPrice() - GetFinalPrice()}");
        }
    }


    class Program
    {
        static void Main()
        {
            // 식품 장바구니 생성
            // Food 3개 추가: 삼각김밥(1200원, 30%), 샌드위치(3500원, 20%), 바나나우유(1800원, 0%)
            // PrintReceipt() 호출
            Cart<Food> foodCart = new Cart<Food>();
            Food samkim = new Food("삼각김밥", 1200, 30, 10);
            Food sandwitch = new Food("샌드위치", 3500, 20, 14);
            Food bananaMilk = new Food("바나나우유", 1800, 0, 30);
            foodCart.Add(samkim);
            foodCart.Add(sandwitch);
            foodCart.Add(bananaMilk);
            
            foodCart.PrintReceipt();


            // 전자제품 장바구니 생성
            // Electronics 2개 추가: 무선이어폰(59000원, Sony), USB허브(25000원, Anker)
            // PrintReceipt() 호출
            Cart<Electronics> elecCart = new Cart<Electronics>();
            elecCart.Add(new Electronics("무선이어폰", 59000, "sony"));
            elecCart.Add(new Electronics("USB허브", 25000, "Anker"));

            elecCart.PrintReceipt();
            
            
        }
    }

}