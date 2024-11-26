using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    class DataObject
    {
        public Dictionary<string, string> Properties { get; set; }

        public DataObject()
        {
            Properties = new Dictionary<string, string>();
        }

        public string GetProperty(string propertyId)
        {
            return Properties.TryGetValue(propertyId, out var value) ? value : null;
        }

        public void SetProperty(string propertyId, string value)
        {
            Properties[propertyId] = value;
        }
    }

    static void Main(string[] args)
    {
        // Кеш для хранения объектов
        Dictionary<string, DataObject> cache = new Dictionary<string, DataObject>();

        LoadInitialData("initial_data.txt", cache);

        while (true)
        {
            Console.WriteLine("Введите команду (или 'exit' для выхода): ");
            var input = Console.ReadLine();
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

            ProcessInput(input, cache);
        }
    }

    static void LoadInitialData(string filePath, Dictionary<string, DataObject> cache)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(',');
            if (parts.Length < 2)
                continue;

            var objectId = parts[0].Trim();
            var propertyId = parts[1].Trim();
            var propertyValue = parts.Length > 2 ? parts[2].Trim() : string.Empty;

            if (!cache.ContainsKey(objectId))
            {
                cache[objectId] = new DataObject();
            }
            cache[objectId].SetProperty(propertyId, propertyValue);
        }
    }

    static void ProcessInput(string input, Dictionary<string, DataObject> cache)
    {
        var parts = input.Split(' ');
        if (parts.Length < 2)
            return;

        string command = parts[0];
        string objectId = parts[1];

        if (command.Equals("get", StringComparison.OrdinalIgnoreCase))
        {
            if (cache.TryGetValue(objectId, out var dataObject))
            {
                Console.WriteLine($"Свойства объекта {objectId}:");
                foreach (var property in dataObject.Properties)
                {
                    Console.WriteLine($"{property.Key}: {property.Value}");
                }
                Console.WriteLine("Введите новое значение в формате 'set <propertyId> <newValue>':");
                var updateInput = Console.ReadLine();
                ProcessUpdate(updateInput, dataObject);
            }
            else
            {
                Console.WriteLine($"Объект с ID {objectId} не найден.");
            }
        }
        else
        {
            Console.WriteLine("Неизвестная команда.");
        }
    }

    static void ProcessUpdate(string input, DataObject dataObject)
    {
        var parts = input.Split(' ');
        if (parts.Length < 3 || !parts[0].Equals("set", StringComparison.OrdinalIgnoreCase))
            return;

        string propertyId = parts[1];
        string newValue = string.Join(' ', parts[2..]);

        dataObject.SetProperty(propertyId, newValue);
        Console.WriteLine($"Свойство {propertyId} обновлено на: {newValue}");
    }
}