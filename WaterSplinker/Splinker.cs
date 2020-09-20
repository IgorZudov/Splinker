using System;

namespace WaterSplinker
{
    /// <summary>
    /// Поливалка
    /// </summary>
    public class Splinker
    {
        public Point Point { get; set; }

        public double Angle { get; set; }

        /// <summary>
        /// Создать зону полива
        /// </summary>
        public WaterZone GenerateZone()
        {
            var angle = Angle / 2 * Math.PI / 180;
            var x = Point.X + 1000;
            var y = Math.Tan(angle) * 1000;
            return new WaterZone(Point,
                new Point(x, Point.Y + y),
                new Point(x, Point.Y - y));
        }
    }

    public class Flower
    {
        public Point Point { get; set; }

        public string Name { get; set; }
    }
}