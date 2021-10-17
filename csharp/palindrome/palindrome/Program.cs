using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("palindrome.tests")]

namespace palindrome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"rotor IsPalindrome_ByIndex: {IsPalindrome_ByIndex("rotor")}");
            Console.WriteLine($"rater IsPalindrome_ByIndex: {IsPalindrome_ByIndex("rater")}");

            Console.WriteLine($"rotor IsPalindrome_WithLinq: {IsPalindrome_WithLinq("rotor")}");
            Console.WriteLine($"rater IsPalindrome_WithLinq: {IsPalindrome_WithLinq("rater")}");

            Console.WriteLine($"rotor IsPalindrome_WithRecursion: {IsPalindrome_WithRecursion("rotor")}");
            Console.WriteLine($"rater IsPalindrome_WithRecursion: {IsPalindrome_WithRecursion("rater")}");

            Console.WriteLine($"rotor IsPalindrome_RubeGoldberg: {IsPalindrome_RubeGoldberg("rotor")}");
            Console.WriteLine($"rater IsPalindrome_RubeGoldberg: {IsPalindrome_RubeGoldberg("rater")}");
        }

        /// <summary>
        /// This method demonstrates how you can test if a string is a
        /// palindrome by inspecting each set of characters.
        /// 
        /// Time Complexity O(n/2)
        /// Space Complexity O(1)
        /// 
        /// The advantage of this method is that it does not require
        /// any additional space for string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true if s is a palindrome</returns>
        public static bool IsPalindrome_ByIndex(string s)
        {
            int end_index = s.Length - 1;
            int start_index = 0;
            while(start_index < end_index)
            {
                if(s[start_index] != s[end_index])
                    return false;
                start_index++;
                end_index--;
            }
            return true;
        }

        /// <summary>
        /// This method utilizes features of the C# language.
        /// Specifically it utilizes LINQ to reverse the string.
        /// 
        /// LINQ is required to do this because the String class
        /// does not have a built in reverse method.
        /// 
        /// This method is a quick one-liner.  However, since
        /// this method relies upon the library, you can't
        /// reliably determine the time or space complexity.
        /// 
        /// Given that the String class is immutable it requires
        /// that at least one new copy of the string be created.
        /// This means that the space complexity is at a minimum O(n).
        /// We ignore the space used by s since that is passed in.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true if s is a palindrome, else false.</returns>
        public static bool IsPalindrome_WithLinq(string s)
        {
            return s.Equals(new string(s.Reverse().ToArray()));
        }

        /// <summary>
        /// This method is similar to using two indexes.
        /// Instead of tracking the two indexes it uses recursion to 
        /// compare the string
        /// 
        /// The time and space complexity are similar to the 
        /// index method.  However, this will create extra memory
        /// on the call stack.  That makes this method a bit overkill.
        /// The purpose however is to demonstrate how you might use
        /// recursion.  In reality, this is just a more expensive way
        /// to create a while loop.
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPalindrome_WithRecursion(string s)
        {
            // A string of length 0 or 1, then it is a palindrome.
            if(s.Length <= 1)
                return true;

            if(s[0] != s[s.Length -1])
                return false;

            return IsPalindrome_WithRecursion(s.Substring(1, s.Length - 2));
        }

        /// <summary>
        /// This method represents a solution that works.  However,
        /// the method is so over the top complicated that it is beyond
        /// necessary.
        /// 
        /// Please do not solve the exercise using a method like this.
        /// Yes, I have seen this done before.  No I won't mentioned the
        /// person that tried to solve it like this.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPalindrome_RubeGoldberg(string s)
        {
            int midpoint = (s.Length%2 == 0) ? s.Length/2 : s.Length/2 + 1;

            // Split the string into two parts.
            var mirror = s.Substring(midpoint).ToCharArray(); // Take the second part of the string.

            // Reverse the second part, but since String class does not have a reverse method, use Array.
            Array.Reverse(mirror);

            // See if the two parts match.
            return s.Substring(0, s.Length/2).Equals(new String(mirror));
        }
    }
}
