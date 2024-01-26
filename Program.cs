//#define VERBOSE

namespace WarenpreisApproximation
{
    class ApproximatePrices
    {
        static int ClosestMatch = int.MaxValue;
        static List<int[]>? ClosestMatchCombinations;
        static int NumberOfItems;
        static double TargetAsDouble;
        static void FindCombinations(int target, int index, int[] combination)
        {
            if (index == combination.Length - 1)
            {
                combination[index] = target;
                // Check if the combination satisfies the condition
                int result = combination[0];
                for (int i = 1; i < NumberOfItems; i++) {
                    result *= combination[i];
                } 
                // Performance improvement
                if (Math.Abs(TargetAsDouble - result) <= Math.Abs(TargetAsDouble - ClosestMatch))
                {
#if VERBOSE
                    Console.WriteLine("{0}, {1}, {2} {3}", Math.Abs(result), ClosestMatch, result, string.Join(", ", combination));
#endif
                    if (ClosestMatch == result && ClosestMatchCombinations != null) { // null check is only for warning.
                        int[] _combination = combination.ToArray();
                        ClosestMatchCombinations.Add(_combination);
                    }
                    else
                    {
                        ClosestMatch = result;
                        int[] _combination = combination.ToArray();
                        ClosestMatchCombinations = new List<int[]> { _combination };

                    }
                }
            }
            else
            {
                for (int i = 1; i <= target; i++)
                {
                    combination[index] = i;
                    FindCombinations(target - i, index + 1, combination);
                }
            }
        }
        static void StartApproximation(float numberToApprox)
        {
            int[] combination = new int[NumberOfItems];
            int factor10 = GetFactor10(numberToApprox);
            int numberAsInt = GetIntegerValue(numberToApprox);
            TargetAsDouble = numberAsInt * Math.Pow(10, factor10 * (NumberOfItems - 1));

            FindCombinations(numberAsInt, 0, combination);
            if (ClosestMatchCombinations == null)
            {
                Console.WriteLine("No results found");
            }
            else
            {
                Console.WriteLine("Closest match: {0} \nPossible Combinations:", ClosestMatch / Math.Pow(10, factor10 * (NumberOfItems)));
                foreach (var match in ClosestMatchCombinations)
                {
                    string output = "";
                    foreach (var matchIntValue in match) {
                        output += matchIntValue / Math.Pow(10, factor10) + ", ";
                    }
                    Console.WriteLine(output[..^2]);
                }
            }
        }
        static int GetFactor10(float number)
        {
            string[] parts = number.ToString().Split('.');
            return parts.Length > 1 ? parts[1].Length : 0;
        }
        static int GetIntegerValue(float number)
        {
            return int.Parse(string.Join("", number.ToString().Split('.')));
        }
        public static void Main()
        {
            float numberToApprox = 0;
            NumberOfItems = 0;
            Console.WriteLine("What number do you want to get? (7.77 by default)");
            while (numberToApprox <= 0)
            {
                try
                {
                    string _num = Console.ReadLine() ?? "0";
                    if (_num.Trim() == "") {
                        numberToApprox = 7.77f;
                    }
                    else { 
                        numberToApprox = float.Parse(_num);
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Number!");
                }
            }
            Console.WriteLine("How many items are there? (4 by default)");
            while (NumberOfItems == 0)
            {
                try
                {
                    string _items = Console.ReadLine() ?? "0";
                    if (_items.Trim() == "")
                    {
                        NumberOfItems = 4;
                    }
                    else
                    {
                        NumberOfItems = int.Parse(_items);
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Number!");
                }
            }
            StartApproximation(numberToApprox);
        }
    }
}