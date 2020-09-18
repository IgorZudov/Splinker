using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WaterSplinker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //put fileName to args
            string[] lines;
            if (args.Length > 0)
                lines = File.ReadAllLines(args[0]);
            else
                lines = StubGenerator.GenerateStub(100000); //большой луг в 100к цветов
           // lines = Stub; 

            
            //пусть поливалка имеет бесконечную силу напора и ограничением выступает только угл
            //угл разброса в 90 градусов означает, что поливалка отклоняется на 45 градусов в обе стороны
            //соответственно начальное положение поливалки будет 0 с разбросом -45, 45

            var zone = GenerateZone(lines[0]);
            var flowers = GetFlowers(lines);

            
            
            
            var flowerTypeCount = 0;
            double angle = 0;
            
            var dictionary = new Dictionary<string, int>();
            
            for (int i = 0; i < 360; i++)
            {
                zone.Rotate(i);
                foreach (var flower in flowers)
                {
                    if (zone.IsInside(flower.Point))
                    {
                        if (dictionary.ContainsKey(flower.Name))
                            dictionary[flower.Name] = dictionary[flower.Name] + 1;
                        else
                            dictionary[flower.Name] = 1;
                    }
                }

                if (dictionary.Any())
                {
                    var maxCount = dictionary.Values.Max();
                    if (flowerTypeCount < maxCount)
                    {
                        angle = i;
                        flowerTypeCount = maxCount;
                    }
                }

                dictionary.Clear();
            }

            Console.WriteLine($"Max count: {flowerTypeCount}. Angle: {angle}");
        }


        private static Flower[] GetFlowers(string[] lines) =>
            lines.Skip(1).Select(x =>
            {
                var ar = x.Split(" ");
                return new Flower
                {
                    Name = ar[0],
                    Point = new Point(Convert.ToDouble(ar[1]), Convert.ToDouble(ar[2]))
                };
            }).ToArray();
        
        private static WaterZone GenerateZone(string s)
        {
            var splinkerParts = s.Split(" ")
                .Select(Convert.ToDouble)
                .ToArray();
            
            var splinker = new Splinker
            {
                Angle = splinkerParts[2],
                Point = new Point(splinkerParts[0], splinkerParts[1])
            };
            
            return splinker.GenerateZone();
        }

        private static string[] Stub = {
            "0 0 90",
            "одуванчик -1 -1.1",
            "роза -2 -2.1",
            "роза -1 -2",
            "гибискус -1 -3",
            "одуванчик -1 -2.2",
            "роза -1.2 -2"
        };
    }
}