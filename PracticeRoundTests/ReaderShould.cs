using PracticeRound;
using System;
using System.Linq;
using Xunit;

namespace PracticeRoundTests
{
    public class ReaderShould
    {
        private const string FilePath = "InputFiles/a_example";
        private readonly PizzaStore actualPizza;

        public ReaderShould()
        {
            actualPizza = new()
            {
                PizzasCount = 5,
                TwoMemberTeams = 1,
                ThreeMemberTeams = 2,
                FourMemberTeams = 3,
            };

            Pizza Pizza1 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "onion", "pepper", "olive" }
            };

            Pizza Pizza2 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "chicken", "mushroom", "pepper" }
            };

            Pizza Pizza3 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "tomato", "mushroom", "basil" }
            };

            Pizza Pizza4 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "chicken", "basil" }
            };

            Pizza Pizza5 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "mushroom", "tomato", "basil" }
            };

        }

        [Fact]
        public void GetPizzaCountAndTeamNumbers()
        {
            PizzaStore _sut = FileService.ReadFile(FilePath);
            Assert.Equal(actualPizza.PizzasCount, _sut.PizzasCount);
            Assert.Equal(actualPizza.TwoMemberTeams, _sut.TwoMemberTeams);
            Assert.Equal(actualPizza.ThreeMemberTeams, _sut.ThreeMemberTeams);
            Assert.Equal(actualPizza.FourMemberTeams, _sut.FourMemberTeams);
        }

        [Fact]
        public void GetPizzasAndToppings()
        {
            Pizza Pizza2 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "chicken", "mushroom", "pepper" }
            };

            Pizza Pizza3 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "tomato", "mushroom", "basil" }
            };

            Pizza Pizza4 = new()
            {
                IngredientsCount = 2,
                Ingredients = new string[] { "chicken", "basil" }
            };

            PizzaStore _sut = FileService.ReadFile(FilePath);

            Assert.True(FoundPizza(_sut.PizzaCircle, Pizza2));
            Assert.True(FoundPizza(_sut.PizzaCircle, Pizza3));
            Assert.True(FoundPizza(_sut.PizzaCircle, Pizza4));
        }

        [Fact]
        public void NotHavePizzasNotInFile()
        {
            Pizza Pizza2 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "chicken" }
            };

            PizzaStore _sut = FileService.ReadFile(FilePath);

            Assert.False(FoundPizza(_sut.PizzaCircle, Pizza2));
        }

        private bool FoundPizza(Pizza currentPizza, Pizza pizzaToFind)
        {
            if (currentPizza is null)
                return false;

            if (currentPizza.Ingredients.SequenceEqual(pizzaToFind.Ingredients) is true)
                return true;

            return FoundPizza(currentPizza.MoreIngredientsPizza, pizzaToFind);
        }
    }
}
