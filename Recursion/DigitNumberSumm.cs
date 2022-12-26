namespace Recursion
{
    public static class DigitNumberSumm
    {
        public static int SummOfNums(int n)
        {
            if (n / 10 == 0)
                return n;

            return n % 10 + SummOfNums(n / 10);
        }
    }
}