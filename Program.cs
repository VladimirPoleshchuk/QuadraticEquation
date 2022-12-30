// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Collections.Generic;

StartQuadraticEquation();

static void StartQuadraticEquation()
{
    Print("Введите три коэффициента квадратного уравнения.");
    Print("a * x^2 + b * x + c = 0");

    Print("Выедите значение a:");
    double a = GetCoefficient();

    Print("Введите значение b:");
    double b = GetCoefficient();

    Print("Введите значение c:");
    double c = GetCoefficient();

    try
    {
        EquationRoots(a, b, c);
    }
    catch (MyException ex)
    {
        Print($"{ex.Message}");
        FormatData($"Вещественных значений не найдено.", Severity.Warning, new Dictionary<string, double>());
    }

    static void Print(string text)
    {
        Console.WriteLine(text);
    }

    static double GetCoefficient()
    {
        double coefficient;
        string lineCoefficient = "";

        while (true)
        {
            try
            {
                lineCoefficient = Console.ReadLine();
                coefficient = Convert.ToDouble(lineCoefficient);

                return coefficient;
            }
            catch (FormatException)
            {
                FormatData($"Вы ввели: { lineCoefficient}", Severity.Error, new Dictionary<string, double>());
                Print("Вы ввели не число!");
                Print("Попробуйте ввести число еще раз:");
            }
        }
    }

    static void EquationRoots(double a, double b, double c)
    {
        double epsilon = 1.0e-10;

        IDictionary<string, double> data = new Dictionary<string, double>();

        if (Math.Abs(a) <= epsilon)
        {
            if (Math.Abs(b) <= epsilon)
            {
                if (Math.Abs(c) > epsilon)
                {
                    throw new MyException("Уравнение не имеет корней.");
                }

                throw new MyException("Уравнение имеет бесконечное множество корней.");
            }
            else
            {
                double x = -c / b;

                data.Add("x", x);

                FormatData("Корень линейного уравнения:", Severity.Norm, data);
            }
        }
        else
        {
            double discriminant = b * b - 4 * a * c;

            if (discriminant < -epsilon)
            {
                throw new MyException("Уравнение корней не имеет.");
            }
            else if (Math.Abs(discriminant) <= epsilon)
            {
                double x = -b / (2 * a);

                data.Add("x", x);

                FormatData("Уравнение имеет один корень:", Severity.Norm, data);
            }
            else
            {
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

                data.Add("x1", x1);
                data.Add("x2", x2);

                FormatData("Уравнение имеет два корня:", Severity.Norm, data);
            }
        }
    }

    static void FormatData(string message, Severity severity, IDictionary<string, double> data)
    {
        Console.WriteLine(new string('_', 50));
        Console.WriteLine();

        switch (severity)
        {
            case Severity.Error:
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            case Severity.Warning:
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                }
            case Severity.Norm:
                {
                    Console.ResetColor();
                    break;
                }
        }

        Console.WriteLine(message);

        foreach (var item in data)
        {
            Console.WriteLine(item.Key + " = " + item.Value);
        }

        Console.ResetColor();
    }
}

class MyException : Exception
{
    public MyException(string message)
        : base(message) { }
}

enum Severity
{
    Norm = 0,
    Error = 1,
    Warning = 2,
}
