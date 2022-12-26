namespace Recursion
{
    public class IsPalindrome
    {
        public static bool CheckString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            if (s.Length == 1)
                return true;

            return IsPal(s, 0, s.Length - 1);
        }
        
        public static bool IsPal(string s, int left, int right)
        {
            if (left >= right)
                return true;

            bool isPalindrome = s[left].Equals(s[right]);
            
            return isPalindrome && IsPal(s, left + 1, right - 1);
        }
    }
}