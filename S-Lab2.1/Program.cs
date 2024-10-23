using System;
using System.Collections.Generic;
using System.Linq;

interface IOrderStrategy
{
    void PlaceOrder(List<(int count, int foodId)> order);
}

class FastFoodOrder : IOrderStrategy
{
    public void PlaceOrder(List<(int count, int foodId)> order)
    {
        var fastFoodOrder = order.Select(o => new int[] { o.foodId, o.count }).ToList();
        Console.WriteLine("Замовлення фастфуду:");
        foreach (var item in fastFoodOrder)
        {
            Console.WriteLine($"Айдi Страви: {item[0]}, Кiлькiсть: {item[1]}");
        }
    }
}

class SushiOrder : IOrderStrategy
{
    public void PlaceOrder(List<(int count, int foodId)> order)
    {
        var foodIds = order.Select(o => o.foodId).ToList();
        var count = order.Select(o => o.count).ToList();

        Console.WriteLine("Замовлення сушi:");
        Console.WriteLine("Айдi Страви: " + string.Join(", ", foodIds));
        Console.WriteLine("Кiлькiсть: " + string.Join(", ", count));
    }
}

class UkrainianFoodOrder : IOrderStrategy
{
    public void PlaceOrder(List<(int count, int foodId)> order)
    {
        var ukrainianOrder = new List<int>();
        foreach (var o in order)
        {
            for (int i = 0; i < o.count; i++)
            {
                ukrainianOrder.Add(o.foodId);
            }
        }

        Console.WriteLine("Замовлення традицiйної української кухнi:");
        Console.WriteLine("Айдi Їжi: " + string.Join(", ", ukrainianOrder));
    }
}

class OrderSystem
{
    private IOrderStrategy _orderStrategy;

    public void SetOrderStrategy(IOrderStrategy orderStrategy)
    {
        _orderStrategy = orderStrategy;
    }

    public void PlaceOrder(List<(int count, int foodId)> order)
    {
        _orderStrategy.PlaceOrder(order);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var order = new List<(int count, int foodId)>
        {
            (3, 123), (1, 500), (2, 42)
        };

        var orderSystem = new OrderSystem();

        Console.WriteLine("Виберiть тип їжi (a - Фастфуд, b - Сушi, c - Традицiйна українська кухня):");
        char foodType = Console.ReadLine()[0];


        switch (foodType)
        {
            case 'a':
                orderSystem.SetOrderStrategy(new FastFoodOrder());
                break;
            case 'b':
                orderSystem.SetOrderStrategy(new SushiOrder());
                break;
            case 'c':
                orderSystem.SetOrderStrategy(new UkrainianFoodOrder());
                break;
            default:
                Console.WriteLine("Невiдомий тип замовлення.");
                return;
        }

        orderSystem.PlaceOrder(order);
    }
}