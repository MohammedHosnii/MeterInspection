namespace Shared
{
    public static class NumericTool
    {
        public static bool IsNumberBetween(int? number, int min, int max)
        {
            return number.HasValue && number >= min && number <= max;
        }

        public static bool IsPositiveNumber(int? number)
        {
            return number.HasValue && number > 0;
        }

        public static bool IsZeroOrPositiveNumber(int? number)
        {
            return number.HasValue && number > 0;
        }

        public static bool IsZeroOrPositiveNumber(decimal? number)
        {
            return number.HasValue && number >= 0;
        }





    }
}
