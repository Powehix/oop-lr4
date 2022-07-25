using System;
using System.Globalization;
using System.Linq;

namespace ConsoleApp1
{
    public class Dispatcher
    {
        public string Message { get; }
        public int Num { get; }

        public Dispatcher(string mes, int num)
        {
            Message = mes;
            Num = num;
        }

    }

    public class Fraction
    {
        private readonly long integerPart;
        private readonly ushort fractionalPart;

        public Fraction Add(Fraction num)
        {
            float res = float.Parse(integerPart + "," + fractionalPart) +
                        float.Parse(num.integerPart + "," + num.fractionalPart);
            string[] parts = res.ToString("0.0###").Split(',');
            return new Fraction(int.Parse(parts[0]), ushort.Parse(parts[1]));
        }

        public Fraction Mult(Fraction num)
        {
            float res = float.Parse(integerPart + "," + fractionalPart) *
                        float.Parse(num.integerPart + "," + num.fractionalPart);
            string[] parts = res.ToString("0.0###").Split(',');
            return new Fraction(int.Parse(parts[0]), ushort.Parse(parts[1]));
        }

        public Fraction Sub(Fraction num)
        {
            float res = float.Parse(integerPart + "," + fractionalPart) -
                        float.Parse(num.integerPart + "," + num.fractionalPart);
            string[] parts = res.ToString("0.0###").Split(',');
            return new Fraction(int.Parse(parts[0]), ushort.Parse(parts[1]));
        }

        public Fraction Div(Fraction num)
        {
            float res = float.Parse(integerPart + "," + fractionalPart) /
                        float.Parse(num.integerPart + "," + num.fractionalPart);
            string[] parts = res.ToString("0.0###").Split(',');
            return new Fraction(int.Parse(parts[0]), ushort.Parse(parts[1]));
        }

        public bool Gt(Fraction num)
        {
            return float.Parse(integerPart + "," + fractionalPart) <
                   float.Parse(num.integerPart + "," + num.fractionalPart);
        }

        public bool Lt(Fraction num)
        {
            return float.Parse(integerPart + "," + fractionalPart) >
                   float.Parse(num.integerPart + "," + num.fractionalPart);
        }

        public Fraction(long integerPart, ushort fractionalPart)
        {
            this.integerPart = integerPart;
            this.fractionalPart = fractionalPart;
        }

        public override string ToString()
        {
            return integerPart + "," + fractionalPart;
        }
    }

    public class Integer
    {
        public delegate void Message(object sender, Dispatcher e);
        public event Message Add_del;
        public event Message Sub_del;
        public event Message Mult_del;
        private readonly int firstNumber;
        private readonly int secondNumber;
        private readonly int thirdNumber;

        public void Add()
        {
            Add_del(this, new Dispatcher("Addition: ", firstNumber + secondNumber));
        }

        public void Add(Integer num)
        {
            Add_del(this, new Dispatcher($"Addition: ", num.firstNumber + num.secondNumber + num.thirdNumber));
        }

        public void Mult()
        {
            Mult_del(this, new Dispatcher($"Multiplication: ", firstNumber * secondNumber));
        }

        public void Mult(Integer num)
        {
            Mult_del(this, new Dispatcher($"Multiplication: ", num.firstNumber * num.secondNumber * num.thirdNumber));
        }

        public void Sub()
        {
            Sub_del(this, new Dispatcher($"Subtraction: ", firstNumber - secondNumber));
        }

        public void Sub(Integer num)
        {
            Sub_del(this, new Dispatcher($"Subtraction: ", num.firstNumber - num.secondNumber - num.thirdNumber));
        }

        public Integer(int firstNumber, int secondNumber)
        {
            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
        }

        public Integer(int firstNumber, int secondNumber, int thirdNumber)
        {
            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
            this.thirdNumber = thirdNumber;
        }

        public override string ToString()
        {
            if (thirdNumber == 0)
                return firstNumber + ", " + secondNumber;
            else return firstNumber + ", " + secondNumber + ", " + thirdNumber;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ANASTASIJA MEZALE, 4802BD");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine();

            Console.WriteLine("-------- 1 --------");
            Console.WriteLine();

            Fraction number1 = new Fraction(12, 5);
            Fraction number2 = new Fraction(15, 25);

            Console.WriteLine("Number1: " + number1);
            Console.WriteLine("Number2: " + number2);
            Console.WriteLine("Addition: " + number1.Add(number2));
            Console.WriteLine("Subtraction: " + number1.Sub(number2));
            Console.WriteLine("Multiplication: " + number1.Mult(number2));
            Console.WriteLine("Division: " + number1.Div(number2));
            Console.WriteLine();
            Console.WriteLine("Number1 < Number2: " + number1.Gt(number2));
            Console.WriteLine("Number1 > Number2: " + number1.Lt(number2));
            Console.WriteLine();

            Console.WriteLine("--- 2 WITH DELEGATES ---");
            Console.WriteLine();

            Integer set1 = new Integer(7, 8);
            Integer set2 = new Integer(15, 4, 6);

            Console.WriteLine("1st set: " + set1);

            set1.Add_del += Show_Message;
            set2.Add_del += Show_Message;
            set1.Sub_del += Show_Message;
            set2.Sub_del += Show_Message;
            set1.Mult_del += Show_Message;
            set2.Mult_del += Show_Message;

            set1.Add();
            set1.Sub();
            set1.Mult();

            Console.WriteLine();
            Console.WriteLine("2nd set: " + set2);

            set2.Add(set2);
            set2.Sub(set2);
            set2.Mult(set2);
     
            Console.ReadLine();
        }

        private static void Show_Message(object sender, Dispatcher e)
        {
            Console.WriteLine(e.Message + e.Num);
        }
    }
}
