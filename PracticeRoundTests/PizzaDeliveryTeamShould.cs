using PracticeRound;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace PizzaProblemTests
{
    public class PizzaDeliveryTeamShould
    {
        [Theory, ClassData(typeof(Ingredients_TestData))]
        public void GetDistinctCountOfIngredients(int expected, List<string[]> ingredients)
        {
            PizzaDeliveryTeam deliveryTeam = new();
            int sut = deliveryTeam.CountDistinctIngredients(ingredients);

            Assert.Equal(expected, sut);
        }

        private class Ingredients_TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    6,
                    new List<string[]>
                    {
                        new string[] { "olive", "jalapeno", "pepper" },
                        new string[] { "sausage", "olive", "pepper", "meatball"},
                        new string[] { "cheese" },
                    }
                };
                yield return new object[]
                {
                    8,
                    new List<string[]>
                    {
                        new string[] { "shrimp", "anchovies", "cheese", "olive" },
                        new string[] { "sausage", "olive", "pepper", "cheese", "meatballs", "tomato" },
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
