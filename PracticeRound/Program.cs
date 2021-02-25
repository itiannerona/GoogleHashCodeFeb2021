using System;
using System.IO;

namespace PracticeRound
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class PizzaStore
    {
        private readonly string[] files = 
        { 
            "b_little_bit.in",
            //"c_many_ingredients.in",
            //"d_many_pizzas.in",
            //"d_many_pizzas.in"
        };

        public PizzaStore()
        {
            PizzaLine = new();
        }

        public int PizzasCount { get; init; }
        public Pizza PizzaLine { get; set; }
        public Pizza LastPizza { get; set; }

        public void AddPizza(Pizza newPizza)
        {
            PizzaLine = AddAndSortPizzaCircle(PizzaLine, newPizza);

            // ** local functions
            
            Pizza AddAndSortPizzaCircle(Pizza currentPizza, Pizza newPizza)
            {
                if (currentPizza is null)
                {
                    currentPizza = newPizza;
                }
                else if (currentPizza.IngredientsCount == newPizza.IngredientsCount)
                {
                    newPizza.NextPizza = currentPizza.NextPizza;
                    currentPizza.NextPizza = newPizza;
                }
                else if (currentPizza.IngredientsCount > newPizza.IngredientsCount)
                {
                    newPizza.NextPizza = currentPizza;
                    currentPizza = newPizza;
                }
                else
                    currentPizza.NextPizza = AddAndSortPizzaCircle(currentPizza.NextPizza, newPizza);
                
                return currentPizza;
            }
        }

        public void GetAndSetLastPizza()
        {
            LastPizza = FindNullPizza(PizzaLine);

            // ** local function
            Pizza FindNullPizza(Pizza currentPizza)
            {
                if (currentPizza.NextPizza is not null)
                    return FindNullPizza(currentPizza.NextPizza);

                return currentPizza;
            }
        }
    }

    public class PizzaDeliveryTeam
    {
        public int TwoMemberTeams { get; init; }
        public int ThreeMemberTeams { get; init; }
        public int FourMemberTeams { get; init; }
    }

    public record Pizza
    {
        public int IngredientsCount { get; set; }
        public string[] Ingredients { get; set; }
        public Pizza NextPizza { get; set; }
    }

    public class FileService
    {
        public static (PizzaStore, PizzaDeliveryTeam) ReadFile(string path)
        {
            using StreamReader reader = new(path);
            var firstLine = reader.ReadLine().Split(' ');

            PizzaDeliveryTeam deliveryTeam = new()
            {
                TwoMemberTeams = int.Parse(firstLine[1]),
                ThreeMemberTeams = int.Parse(firstLine[2]),
                FourMemberTeams = int.Parse(firstLine[3])
            };

            PizzaStore pizzaStore = new() 
            {
                PizzasCount = int.Parse(firstLine[0]),
            };
            
            pizzaStore.PizzaLine = ParsePizza(reader.ReadLine().Split());

            while (reader.Peek() >= 0)
            {
                Pizza newPizza = ParsePizza(reader.ReadLine().Split());
                pizzaStore.AddPizza(newPizza);
            }

            pizzaStore.GetAndSetLastPizza();

            return (pizzaStore, deliveryTeam);

            // local functions

            static Pizza ParsePizza(string[] unparsedPizza)
            {
                var ingredients = new string[unparsedPizza.Length - 1];
                Array.Copy(unparsedPizza, 1, ingredients, 0, unparsedPizza.Length - 1);
                return new Pizza()
                {
                    IngredientsCount = int.Parse(unparsedPizza[0]),
                    Ingredients = ingredients
                };
            }

            
        }
    }
}
