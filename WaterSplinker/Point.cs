namespace WaterSplinker
{
    public struct Point
    {
        public double X;

        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        
        public void Rotate(double angleInDegrees, Point rotationPoint)
        {
            angleInDegrees *= System.Math.PI / 180;
            var sin = System.Math.Sin(angleInDegrees);
            var cos = System.Math.Cos(angleInDegrees);
 
            X -= rotationPoint.X;
            Y -= rotationPoint.Y;
 
            var xNew = X * cos - Y * sin;
            var yNew = X * sin + Y * cos;
     
            X = xNew + rotationPoint.X;
            Y = yNew + rotationPoint.Y;
        }
    }
}