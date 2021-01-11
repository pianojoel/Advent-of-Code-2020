using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace AoC21
{
    class Program
    {
        static void Main(string[] args)
        {

            var foods = InitFoodList();

            var originalFoods = foods.SelectMany(f => f.Ingredients).ToList();

            Dictionary<string, string> allergenIngredient = new Dictionary<string, string>();

            List<string> usedFoods = new List<string>();
            List<string> usedAllergen = new List<string>();

            var allergenlist = foods.SelectMany(a => a.Allergens).Distinct().Reverse().ToList();
            do
            {
                foods = InitFoodList();
                allergenIngredient.Clear();
                var ingredientlist = foods.SelectMany(i => i.Ingredients).Distinct().ToList(); 
                foreach (var allergen in allergenlist)
                {
                    foreach (var ingredient in ingredientlist)
                    {

                        var containsAllergen = foods.Where(f => f.Allergens.Contains(allergen)).ToList();
                        if (containsAllergen.All(f => f.Ingredients.Contains(ingredient)))
                        {
                            var containsIngredient = foods.Where(f => f.Ingredients.Contains(ingredient)).ToList();

                            allergenIngredient.Add(allergen, ingredient);
                            foreach (var food in containsIngredient)
                            {
                                food.Ingredients.Remove(ingredient);
                                food.Allergens.Remove(allergen);

                            }
                            break;
                        }
                    }

                }
               //rotate allergenlist after failed attempt
                allergenlist.Add(allergenlist[0]);
                allergenlist.RemoveAt(0);
               
        }
            while (foods.SelectMany(a => a.Allergens).Distinct().Count() > 0);

            
            

           
            

            var remaining = foods.SelectMany(f => f.Ingredients).Distinct().ToList();
            
            int count = 0;
            foreach (var f in originalFoods)
            {
                if (remaining.Contains(f)) count++;
            }

            Console.WriteLine("Sum: " + count);

            var q = allergenIngredient.OrderBy(k => k.Key);
            string canonicalList = "";
            foreach (var key in q)
            {
                
                canonicalList += key.Value + ",";
            }
            canonicalList = canonicalList.Trim(',');
            Console.WriteLine("Canonical Dangerous Ingredient List: " + canonicalList);
            Console.ReadKey();
        }
        static List<Food> InitFoodList()
        {
            var data = File.ReadAllText("input.txt").Split("\r\n").Select(l => l.Trim(' ', '\r', '\n')).ToList();
            var foods = new List<Food>();
            foreach (var line in data)
            {
                
                var ingredients = line.Substring(0, line.IndexOf('(') - 1).Split(' ');
                List<string> i = new List<string>();
                Food f = new Food();
                f.Ingredients.AddRange(ingredients);
                var allergens = line.Substring(line.IndexOf('(') + 10).Split(' ').Select(l => l.Trim(',', ')')).ToList();
                List<string> a = new List<string>();
                f.Allergens.AddRange(allergens);
                foods.Add(f);

            }
            return foods;
        }
    }
    class Food
    {
        public List<string> Ingredients { get; set; }
        public List<string> Allergens { get; set; }
        public Food()
        {
            Ingredients = new List<string>();
            Allergens = new List<string>();
        }
    }
}
