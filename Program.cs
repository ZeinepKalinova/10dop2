using System;
using System.IO;

// Интерфейс для выполнения арифметических операций
public interface ICalculatable
{
    double Add(double a, double b);
    double Subtract(double a, double b);
    double Multiply(double a, double b);
    double Divide(double a, double b);
}

// Интерфейс для сохранения и загрузки состояния
public interface IStorable
{
    void SaveState(string fileName);
    void LoadState(string fileName);
}

// Реализация ICalculatable
public class SimpleCalculator : ICalculatable
{
    public double Add(double a, double b)
    {
        return a + b;
    }

    public double Subtract(double a, double b)
    {
        return a - b;
    }

    public double Multiply(double a, double b)
    {
        return a * b;
    }

    public double Divide(double a, double b)
    {
        if (b != 0)
            return a / b;
        else
        {
            Console.WriteLine("Error: Division by zero.");
            return double.NaN; // Not a Number
        }
    }
}

// Расширение функциональности базового калькулятора
public class AdvancedCalculator : SimpleCalculator, IStorable
{
    public double Power(double a, double b)
    {
        return Math.Pow(a, b);
    }

    public double SquareRoot(double a)
    {
        if (a >= 0)
            return Math.Sqrt(a);
        else
        {
            Console.WriteLine("Error: Cannot calculate square root of a negative number.");
            return double.NaN;
        }
    }

    // Реализация методов для сохранения и загрузки состояния
    public void SaveState(string fileName)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine($"Last Result: {LastResult}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving state: {ex.Message}");
        }
    }

    public void LoadState(string fileName)
    {
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        if (double.TryParse(parts[1].Trim(), out double result))
                        {
                            LastResult = result;
                            Console.WriteLine($"State loaded. Last Result: {LastResult}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading state: {ex.Message}");
        }
    }

    // Дополнительное свойство для хранения последнего результата
    public double LastResult { get; private set; }
}

class Program
{
    static void Main()
    {
        // Создаем объекты обоих типов калькуляторов
        SimpleCalculator simpleCalculator = new SimpleCalculator();
        AdvancedCalculator advancedCalculator = new AdvancedCalculator();

        // Демонстрируем работу базового калькулятора
        Console.WriteLine("Simple Calculator:");
        Console.WriteLine($"Addition: {simpleCalculator.Add(5, 3)}");
        Console.WriteLine($"Subtraction: {simpleCalculator.Subtract(8, 4)}");
        Console.WriteLine($"Multiplication: {simpleCalculator.Multiply(2, 6)}");
        Console.WriteLine($"Division: {simpleCalculator.Divide(10, 2)}");

        // Демонстрируем работу расширенного калькулятора
        Console.WriteLine("\nAdvanced Calculator:");
        Console.WriteLine($"Power: {advancedCalculator.Power(2, 3)}");
        Console.WriteLine($"Square Root: {advancedCalculator.SquareRoot(16)}");

        // Демонстрируем работу сохранения и загрузки состояния
        Console.WriteLine("\nAdvanced Calculator State Operations:");
        advancedCalculator.SaveState("calculator_state.txt");
        advancedCalculator.LoadState("calculator_state.txt");

        Console.ReadLine();
    }
}
