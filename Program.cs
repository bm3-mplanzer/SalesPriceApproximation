#define VERBOSE
using System.Net.WebSockets;

namespace WarenpreisApproximation
{
    class ApproximatePrices
    {
        static int ClosestMatch = int.MaxValue;
        static List<int[]>? ClosestMatchCombinations;
        static int Items;
        static double TargetAsDouble;
        static int counter = 0;

//        static void _FindCombinations(int target, int index, int[] combination)
//        {
//            if (index == combination.Length - 1)
//            {
//                combination[index] = target;
//                // Check if the combination satisfies the condition
//                int result = (combination[0] * combination[1] * combination[2] * combination[3]);
//                counter++;
//                if (Math.Abs(777000000 - result) < Math.Abs(777000000 - ClosestMatch))
//                {
//#if VERBOSE
//                    // Console.WriteLine("{0}, {1}, {2} {3}", Math.Abs(result), ClosestMatch, result, string.Join(", ", combination));
//#endif
//                    ClosestMatch = result;
//                    ClosestMatchCombinations = combination;
//                }
//            }
//            else
//            {
//                for (int i = 1; i <= target; i++)
//                {
//                    combination[index] = i;
//                    FindCombinations(target - i, index + 1, combination);
//                }
//            }
//        }
        static void FindCombinations(int target, int index, int[] combination)
        {
            if (index == combination.Length - 1)
            {
                combination[index] = target;
                // Check if the combination satisfies the condition
                int result = combination[0];
                for (int i = 1; i < Items; i++) {
                    result *= combination[i];
                } 
                counter++;
                // Performance improvement
                if (Math.Abs(TargetAsDouble - result) <= Math.Abs(TargetAsDouble - ClosestMatch))
                {
#if VERBOSE
                    // Console.WriteLine("{0}, {1}, {2} {3}", Math.Abs(result), ClosestMatch, result, string.Join(", ", combination));
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
        static int GetDecimalFactor(float number)
        {
            string[] parts = number.ToString().Split('.');
            return parts.Length > 1 ? parts[1].Length : 0;
        }
        static int GetIntegerValue(float number) {
            return int.Parse(String.Join("", number.ToString().Split('.')));
        }
        static void StartApproximation(float number, int items)
        {
            int[] combination = new int[items]; // Array to store combinations
            int decimalFactor = GetDecimalFactor(number);
            int intValue = GetIntegerValue(number);
            Items = items;
            TargetAsDouble = (intValue * (double)Math.Pow(10, decimalFactor * (items - 1)));

            FindCombinations(intValue, 0, combination);
            if (ClosestMatchCombinations == null)
            {
                Console.WriteLine("No results found");
            }
            else
            {
                Console.WriteLine("Closest match: {0} \nPossible Combinations:", ClosestMatch / Math.Pow(10, decimalFactor * (items)));
                foreach (var match in ClosestMatchCombinations)
                {
                    string output = "";
                    foreach (var matchIntValue in match) {
                        output += matchIntValue / Math.Pow(10, decimalFactor) + ", ";
                    }
                    Console.WriteLine(output[..^2]);
                }
            }
        }
        public static void Main()
        {
            float number = 0;
            int items = 0;
            Console.WriteLine("What number do you want to get? (7.77 by default)");
            while (number <= 0)
            {
                try
                {
                    string _num = Console.ReadLine() ?? "0";
                    if (_num.Trim() == "") {
                        number = 7.77f;
                    }
                    else { 
                        number = float.Parse(_num);
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Number!");
                }
            }
            Console.WriteLine("How many items are there? (4 by default)");
            while (items == 0)
            {
                try
                {
                    string _items = Console.ReadLine() ?? "0";
                    if (_items.Trim() == "")
                    {
                        items = 4;
                    }
                    else
                    {
                        items = int.Parse(_items);
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Number!");
                }
            }
            StartApproximation(number, items);
        }
    }
}