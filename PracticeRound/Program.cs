using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PracticeRound
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    //They have an option to upload one file, so opted to put everything in one file

    public class PizzaStore
    {
        private readonly string[] files = 
        { 
            "b_little_bit.in",
            //"c_many_ingredients.in",
            //"d_many_pizzas.in",
            //"d_many_pizzas.in"
        };

        public PizzaStore() { }

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

        public void DeliverPizzas(int numberOfPizzas)
        {
            
        }
    }

    public class PizzaDeliveryTeam
    {
        public int TwoMemberTeams { get; init; }
        public int ThreeMemberTeams { get; init; }
        public int FourMemberTeams { get; init; }

        public int CountDistinctIngredients(IList<string[]> ingredientsSet)
        {
            IEnumerable<string> combinedSet = Enumerable.Empty<string>();
            foreach (var set in ingredientsSet)
                combinedSet = combinedSet.Union(set);

            return combinedSet.Count();
        }
    }

    public record Pizza
    {
        public int Id { get; init; }
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

            int id = 0;
            while (reader.Peek() >= 0)
            {
                Pizza newPizza = ParsePizza(reader.ReadLine().Split(), id);
                pizzaStore.AddPizza(newPizza);
                id++;
            }

            pizzaStore.GetAndSetLastPizza();

            return (pizzaStore, deliveryTeam);

            // local functions

            static Pizza ParsePizza(string[] unparsedPizza, int id)
            {
                var ingredients = new string[unparsedPizza.Length - 1];
                Array.Copy(unparsedPizza, 1, ingredients, 0, unparsedPizza.Length - 1);
                return new Pizza()
                {
                    Id = id,
                    IngredientsCount = int.Parse(unparsedPizza[0]),
                    Ingredients = ingredients
                };
            }
        }
    }
}
