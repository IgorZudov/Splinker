using System;
using System.Globalization;
using System.Linq;

namespace WaterSplinker
{
    public static class StubGenerator
    {
        public static string[] GenerateStub(int elementCount)
        {
            var rnd = new Random();
            var result = new string[elementCount];
            result[0] = "0 0 90";
            for (var i = 1; i < elementCount; i++)
                result[i] = $"{RandName(rnd)} {RandNum(rnd)} {RandNum(rnd)}";
            return result;
        }
        
        private static string RandName(Random rnd)
        {
            var flowers = new[] { "роза", "ромашка", "гибискус", "пион"};
            return flowers[rnd.Next(flowers.Length)];
        }
        
        private static string RandNum(Random rnd)
        {
            const int minimum = -1000;
            const int maximum = 1000;
            var random = new Random();
            return Math.Round(rnd.NextDouble() * (maximum - minimum) + minimum, 4).ToString(
                new NumberFormatInfo{NumberDecimalSeparator = "."});
        }
    }
}