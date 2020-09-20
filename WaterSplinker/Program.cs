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
            string[] lines;
            if (args.Length > 0)
                //читаем путь файла из аргументов
                lines = File.ReadAllLines(args[0]);
            else
                //генерируем луг в 100к цветов
                lines = StubGenerator.GenerateStub(100000);
            //заглушка из примера задания
            // lines = Stub; 

            
            //пусть поливалка имеет бесконечную силу напора и ограничением выступает только угл
            //угл разброса в 90 градусов означает, что поливалка отклоняется на 45 градусов в обе стороны
            //соответственно начальное положение поливалки будет 0 с разбросом -45, 45

            var zone = GenerateZone(lines[0]);
            var flowers = GetFlowers(lines);
            
            //максимальное число сортов цветов
            var maxFlowerTypeCount = 0;
            //угл, при котором достигается максимальное числов сортов цветов
            double needAngle = 0;
            //счетчик сортов
            var flowerTypeCounter = new Dictionary<string, int>();

            //точность расчетов 1 градус
            for (var i = 1; i < 360; i++)
            {
                zone.Rotate(i);
                
                for (var index  = 0; index < flowers.Length; index++)
                {
                    var flower = flowers[index];
                    if (!zone.IsInside(flower.Point)) 
                        continue;
                    
                    if (flowerTypeCounter.ContainsKey(flower.Name))
                        flowerTypeCounter[flower.Name] = flowerTypeCounter[flower.Name] + 1;
                    else
                        flowerTypeCounter[flower.Name] = 1;
                }

                if (flowerTypeCounter.Any())
                {
                    var maxCount = flowerTypeCounter.Values.Max();
                    if (maxFlowerTypeCount < maxCount)
                    {
                        needAngle = i;
                        maxFlowerTypeCount = maxCount;
                    }
                }

                flowerTypeCounter.Clear();
            }

            Console.WriteLine($"Max count: {maxFlowerTypeCount}. Angle: {needAngle}");
        }


        private static Flower[] GetFlowers(IEnumerable<string> lines) =>
            lines.Skip(1).Select(x =>
            {
                var ar = x.Split(" ");
                return new Flower
                {
                    Name = ar[0],
                    Point = new Point(Convert.ToDouble(ar[1]), Convert.ToDouble(ar[2]))
                };
            }).ToArray();
        
        /// <summary>
        /// Создает зону полива
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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