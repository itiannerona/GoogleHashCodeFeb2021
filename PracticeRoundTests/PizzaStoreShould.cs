using PracticeRound;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PizzaProblemTests
{
    public class PizzaStoreShould
    {
        private readonly PizzaStore _sut;

        public PizzaStoreShould()
        {
            _sut = new PizzaStore();
        }

        [Theory, ClassData(typeof(TestData))]
        public void SortPizzaWhenAdded(Pizza[] pizzas)
        {
            foreach (var pizza in pizzas)
                _sut.AddPizza(pizza);

            Assert.True(PizzasAreOrderedByIngredientsCountAscending(_sut.PizzaLine));

            //-- local functions

            bool PizzasAreOrderedByIngredientsCountAscending(Pizza pizza)
            {
                if (pizza.NextPizza is null)
                    return true;

                if (pizza.IngredientsCount <= pizza.NextPizza.IngredientsCount)
                    return PizzasAreOrderedByIngredientsCountAscending(pizza.NextPizza);

                else
                    return false;
            }
        }

        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new Pizza[]
                    {
                        new Pizza
                        {
                            IngredientsCount = 4,
                            Ingredients = new string[] { "anchovies", "mushrooms", "cheese", "shrimp" }
                        },
                        new Pizza
                        {
                            IngredientsCount = 2,
                            Ingredients = new string[] { "sausage", "mushrooms" }
                        },
                        new Pizza
                        {
                            IngredientsCount = 5,
                            Ingredients = new string[] { "meatballs", "pepper", "sausage", "cheese", "olives" }
                        },
                        new Pizza
                        {
                            IngredientsCount = 1,
                            Ingredients = new string[] { "cheese" }
                        },
                        new Pizza
                        {
                            IngredientsCount = 2,
                            Ingredients = new string[] { "sausage", "mushrooms" }
                        }
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Fact]
        public void SetCorrectLastPizza()
        {
            var filePath = "InputFiles/a_example";
            string[] actual = { "sausage", "mushroom", "meatballs", "olive" };
            
            var (sut, _) = FileService.ReadFile(filePath);
            sut.GetAndSetLastPizza();

            Assert.True(actual.SequenceEqual(sut.LastPizza.Ingredients));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void RemovePizzasFromBeginningAndLast(int numberOfPizzas)
        {
            var filePath = "InputFiles/a_example";

            var (pizzaStore, _) = FileService.ReadFile(filePath);

            var sutFirstPizzaId = pizzaStore.PizzaLine.Id;
            var sutLastPizzaId = pizzaStore.LastPizza.Id;

            pizzaStore.DeliverPizzas(numberOfPizzas);

            Assert.NotEqual(pizzaStore.PizzaLine.Id, sutFirstPizzaId);
            Assert.NotEqual(pizzaStore.LastPizza.Id, sutLastPizzaId);
        }
    }
}




