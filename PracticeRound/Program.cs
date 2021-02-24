﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

        }

        public int PizzasCount { get; init; }
        public int TwoMemberTeams { get; init; }
        public int ThreeMemberTeams { get; init; }
        public int FourMemberTeams { get; init; }

        public Pizza PizzaCircle { get; set; }
        //TODO: highest and lowest ingredients
    }

    public record Pizza
    {
        public int IngredientsCount { get; set; }
        public string[] Ingredients { get; set; }
        public Pizza MoreIngredientsPizza { get; set; }
    }

    public class FileService
    {
        public static PizzaStore ReadFile(string path)
        {
            using StreamReader reader = new(path);
            var firstLine = reader.ReadLine().Split(' ');
            PizzaStore pizzaStore = new() 
            {
                PizzasCount = int.Parse(firstLine[0]),
                TwoMemberTeams = int.Parse(firstLine[1]),
                ThreeMemberTeams = int.Parse(firstLine[2]),
                FourMemberTeams = int.Parse(firstLine[3])
            };
            
            pizzaStore.PizzaCircle = ParsePizza(reader.ReadLine().Split());

            while (reader.Peek() >= 0)
            {
                Pizza newPizza = ParsePizza(reader.ReadLine().Split());
                CompareIngredientsCount(pizzaStore.PizzaCircle, newPizza);
            }

            return pizzaStore;

            // local functions

            Pizza ParsePizza(string[] unparsedPizza)
            {
                var ingredients = new string[unparsedPizza.Length - 1];
                Array.Copy(unparsedPizza, 1, ingredients, 0, unparsedPizza.Length - 1);
                return new Pizza()
                {
                    IngredientsCount = int.Parse(unparsedPizza[0]),
                    Ingredients = ingredients
                };
            }

            void CompareIngredientsCount(Pizza currentPizza, Pizza newPizza)
            {
                if (currentPizza is null)
                {
                    currentPizza = newPizza;
                    return;
                }

                if (currentPizza.IngredientsCount < newPizza.IngredientsCount)
                {
                    CompareIngredientsCount(currentPizza.MoreIngredientsPizza, newPizza);
                    return;
                }
                else
                {
                    newPizza.MoreIngredientsPizza = currentPizza.MoreIngredientsPizza;
                    currentPizza.MoreIngredientsPizza = newPizza;
                }
            }
        }
    }
}