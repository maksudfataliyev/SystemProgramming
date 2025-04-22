using System;
using System.Net.Http;
using System.Threading;
using System.Text.Json;
class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static string apiKey = "9d6582d69dab0f20bf40c7e131aec78f"; 
    private static string city = "Baku";
    private static bool isRunning = true;
    private static Timer weatherTimer;

    static void Main(string[] args)
    {
        Console.WriteLine("Погодное приложение");
        Console.WriteLine("Введите город (по умолчанию Baku):");
        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            city = input;
        }

        weatherTimer = new Timer(UpdateWeather, null, 0, 10000);

        Console.WriteLine("Нажмите любую клавишу для остановки...");
        Console.ReadKey();

        isRunning = false;
        weatherTimer.Dispose();
        client.Dispose();
    }

    private static async void UpdateWeather(object state)
    {
        if (!isRunning) return;

        try
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=ru";
            
            HttpResponseMessage response = await client.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"\nОшибка: {response.StatusCode} - Город не найден или проблема с API");
                return;
            }
            
            string responseBody = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherData>(responseBody);

            if (weatherData == null)
            {
                Console.WriteLine("\nОшибка: Не удалось получить данные о погоде");
                return;
            }

            Console.Clear();
            Console.WriteLine($"Погода в {weatherData.Name} ({DateTime.Now})");
            Console.WriteLine($"Температура: {weatherData.Main.Temp}°C");
            Console.WriteLine($"Ощущается как: {weatherData.Main.FeelsLike}°C");
            Console.WriteLine($"Влажность: {weatherData.Main.Humidity}%");
            Console.WriteLine($"Давление: {weatherData.Main.Pressure} hPa");
            Console.WriteLine($"Ветер: {weatherData.Wind.Speed} м/с");
            
            if (weatherData.Weather != null && weatherData.Weather.Length > 0)
            {
                Console.WriteLine($"Описание: {weatherData.Weather[0].Description}");
            }
            
            Console.WriteLine("\nНажмите любую клавишу для остановки...");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"\nОшибка при запросе погоды: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nНеожиданная ошибка: {e.Message}");
        }
    }
}

public class WeatherData
{
    public string Name { get; set; }
    public MainData Main { get; set; }
    public WindData Wind { get; set; }
    public WeatherDescription[] Weather { get; set; }
}

public class MainData
{
    public float Temp { get; set; }
    public float FeelsLike { get; set; }
    public int Humidity { get; set; }
    public float Pressure { get; set; }
}

public class WindData
{
    public float Speed { get; set; }
}

public class WeatherDescription
{
    public string Description { get; set; }
}