namespace System
{
    public static class DoubleExtensions
    {
        public static double MapRange(this double value, double newMin, double newMax, double oldMin = 0, double oldMax = 1)
        {
            double slope = (newMax - newMin) / (oldMax - oldMin);
            return (value - oldMin) * slope + newMin;
        }
    }
}
