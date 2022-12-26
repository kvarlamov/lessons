namespace Recursion
{
    public static class exponentiation
    {
        public static long Exp(int N, int M)
        {
            if (M == 0)
                return 1;
            
            if (M == 1)
                return N;

            return N * Exp(N, M - 1);
        }
    }
}