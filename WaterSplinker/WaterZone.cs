using System;

namespace WaterSplinker
{
    /// <summary>
    /// Зона полива
    /// </summary>
    public class WaterZone
    {
        public Point BasePoint;

        public Point B;

        public Point C;

        public WaterZone(Point basePoint, Point b, Point c)
        {
            BasePoint = basePoint;
            B = b;
            C = c;
        }

        /// <summary>
        /// Повернуть зону полива
        /// </summary>
        /// <param name="angle"></param>
        public void Rotate(double angle)
        {
            B.Rotate(angle, BasePoint);
            C.Rotate(angle, BasePoint);
        }
        
        /// <summary>
        /// Находится ли точка внутри треугольника
        /// </summary>
        public bool IsInside(Point point)
        {
            //todo можно оптимизнуть, так как профилировщик указывает на bottleneck
            //todo можно закэшировать вычисления
            
            var a = ((B.Y - C.Y) * (point.X - C.X) + (C.X - B.X) * (point.Y - C.Y)) /
                    ((B.Y - C.Y) * (BasePoint.X - C.X) + (C.X - B.X) * (BasePoint.Y - C.Y));
            var b = ((C.Y - BasePoint.Y) * (point.X - C.X) + (BasePoint.X - C.X) * (point.Y - C.Y)) /
                    ((B.Y - C.Y) * (BasePoint.X - C.X) + (C.X - B.X) * (BasePoint.Y - C.Y));
            var c = 1 - a - b;
            
            if (a == 0 || b == 0 || c == 0)
                return true;
            if (a >= 0 && a <= 1 && b >= 0 && b <= 1 && c >= 0 && c <= 1)
                return true;
            return false;
        }
    }
}