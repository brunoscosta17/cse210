using System;

class Program
{
    static void Main(string[] args)
    {
        // Teste dos 3 construtores
        var f1 = new Fraction();            // 1/1
        var f2 = new Fraction(5);           // 5/1
        var f3 = new Fraction(3, 4);        // 3/4
        var f4 = new Fraction(1, 3);        // 1/3

        // Saída no formato do exemplo do enunciado
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());

        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());

        Console.WriteLine(f4.GetFractionString());
        Console.WriteLine(f4.GetDecimalValue());

        // Testando getters e setters
        f1.SetTop(6);
        f1.SetBottom(7);
        Console.WriteLine(f1.GetFractionString());  // 6/7
        Console.WriteLine(f1.GetDecimalValue());    // 0.857142857...
    }
}
