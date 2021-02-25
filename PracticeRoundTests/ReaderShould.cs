using PracticeRound;
using System;
using System.Linq;
using Xunit;

namespace PracticeRoundTests
{
    public class ReaderShould
    {
        private const string FilePath = "InputFiles/a_example";

        public ReaderShould() { }

        [Fact]
        public void GetPizzaCountAndTeamNumbers()
        {
            int pizzasCount = 6;
            int twoMemberTeams = 1;
            int threeMemberTeams = 2;
            int fourMemberTeams = 3;

            var (sutPizzaStore, sutPizzaDeliveryTeam) = FileService.ReadFile(FilePath);
            Assert.Equal(pizzasCount, sutPizzaStore.PizzasCount);
            Assert.Equal(twoMemberTeams, sutPizzaDeliveryTeam.TwoMemberTeams);
            Assert.Equal(threeMemberTeams, sutPizzaDeliveryTeam.ThreeMemberTeams);
            Assert.Equal(fourMemberTeams, sutPizzaDeliveryTeam.FourMemberTeams);
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

            var (_sut, _) = FileService.ReadFile(FilePath);

            Assert.True(FoundPizza(_sut.PizzaLine, Pizza2));
            Assert.True(FoundPizza(_sut.PizzaLine, Pizza3));
            Assert.True(FoundPizza(_sut.PizzaLine, Pizza4));
        }

        [Fact]
        public void NotHavePizzasNotInFile()
        {
            Pizza Pizza2 = new()
            {
                IngredientsCount = 3,
                Ingredients = new string[] { "chicken" }
            };

            var (sut, _) = FileService.ReadFile(FilePath);

            Assert.False(FoundPizza(sut.PizzaLine, Pizza2));
        }

        private bool FoundPizza(Pizza currentPizza, Pizza pizzaToFind)
        {
            if (currentPizza is null)
                return false;

            if (currentPizza.Ingredients.SequenceEqual(pizzaToFind.Ingredients) is true)
                return true;

            return FoundPizza(currentPizza.NextPizza, pizzaToFind);
        }
    }
}
