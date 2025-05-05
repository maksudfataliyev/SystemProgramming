namespace Async_await_practice;

class Program
{
    static async Task Application(string[] args)
    {
        Random random = new Random();
        List<int> numbers = new List<int>();
        for (int i = 0; i < 20; i++)
        {
            numbers.Add(random.Next(1, 101));
        }

        Console.WriteLine("Сгенерированные числа:");
        Console.WriteLine(string.Join(", ", numbers));
        var evenTask = FilterEvenAsync(numbers);
        var oddTask = FilterOddAsync(numbers);
        var sumTask = CalculateSumAsync(numbers);

        await Task.WhenAll(evenTask, oddTask, sumTask);

        var evenNumbers = await evenTask;
        var oddNumbers = await oddTask;
        var sum = await sumTask;

        Console.WriteLine($"\nЧетные числа: {string.Join(", ", evenNumbers)}");
        Console.WriteLine($"Нечетные числа: {string.Join(", ", oddNumbers)}");
        Console.WriteLine($"Сумма чисел: {sum}");
    }
    static async Task<List<int>> FilterEvenAsync(List<int> numbers)
    {
        await Task.Delay(1500); 
        return numbers.Where(n => n % 2 == 0).ToList();
    }
    

    static async Task<List<int>> FilterOddAsync(List<int> numbers)
    {
        await Task.Delay(1500); 
        return numbers.Where(n => n % 2 != 0).ToList();
    }

    static async Task<int> CalculateSumAsync(List<int> numbers)
    {
        await Task.Delay(1000); 
        return numbers.Sum();
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}