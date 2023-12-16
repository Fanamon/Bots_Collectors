using System;

namespace RandomTypesExtensions
{
    public class RandomFloat
    {
        private readonly Random _random;

        public RandomFloat()
        {
            _random = new Random();
        }

        public float NextFloat()
        {
            double value = _random.NextDouble() * float.MaxValue;

            return (float)value;
        }

        public float NextFloat(float maxValue)
        {
            double value = _random.NextDouble() * maxValue;

            return (float)value;
        }

        public float NextFloat(float minValue, float maxValue)
        {
            double value = _random.NextDouble() * (maxValue - minValue) + minValue;

            return (float)value;
        }
    }
}