# Palindrome

A palindrome is a word, number, phrase or other sequence of characters that reads the same forwards as it does backwards.

Some examples of a numeric palindrome are 777 or 12321.  A palindrome can be a date such as 02-02-2020 or a time like 12:21.

This is a series of examples intended to demonstrate a variety of ways to use palindrome to demonstrate csharp code.  

## Problem

Given a string `s` determine if it is a palindrome.

## Discussion

There are a number of algorithms you could derive to determine if a string s is a palindrome.  

When writing code you generally want to write it in the smallest most efficient manner to reduce time and space (i.e. memory) requirements while maintaining readability.  That is not the goal here.

Additionally, there are a number of factors you may or may not want to consider when determining if a sequence of characters is a palindrome.  Should you consider punctuation?  Do symbols like a slash or a dash count?  What about spaces and whitespace?

For the purposes of this exercise I will presume that any string passed in has been normalized and that I do not need to consider any of those questions.  If whitespace is irrelevant then it is presumed that the whitespace has been removed by some other prior method.

## Solutions 1: A loop

Since the input is a string I can easily compare individual characters in the string in most languages by using an index.

In this solution I will simply inspect each set of characters to determine if they match each other.  If at any time I find a set of characters that do not match I know immediately that the string is not a palindrome.

### Algorithm: Loop

Start by describing the algorithm to be used.

1. Get the length of the string
2. Set a start_index to 0 (A zero based index is presumed)
3. Set end_index to string_length - 1 (To account for zero based indexes)
4. While the start_index is less than the end_index repeat a loop.
5. If the character at start_index does not equal the character at end_index, return false.
6. Increment the start_index by 1.
7. Decrement the end_index by 1.
8. Evaluate loop
9. If loop ends, return true.

### Implementation: Loop

```chsarp
public static bool IsPalindrome(string s)
{
    int start_index = 0;
    int end_index = s.Length - 1;

    while(start_index < end_index)
    {
        if(s[start_index] != s[end_index])
            return false;
        
        start_index++;
        end_index--;
    }
    return true;
}
```

This implementation handles a few edge cases without any special code.  It handles a string of length 0 or 1.  A string of length 0 or 1 is a palindrome.  If the string is less than 2 characters, then the start and end indexes will be the same and the code will default to the return true without entring the while loop.

This implementation also does not require any extra memory to be allocated for another string in order to test for the palindrome.  The only extra memory that is used is the two indexes.  This extra space is minimal and is always the same amount regardless of the size of the input string.

The space complexity of this implementation is O(1) because the amount of memory used is always the same and not dependent upon the size of the input string.

The time complexity of this implementation is O(n/2).  In the event that the input string is a single character we have no comparisons to make and we have a constant number of operations.  If this were the worse case scenario it would make our algorithem a O(1).

Alas this algorithm is not O(1).  Instead the number of comparisons depends upon the size of the input string.  Regardless of the number of characters in the input string we will never have more than n/2 operations.  This is because we compare the first the two characters that mirror each other each time we loop through the string.  The comparison will never have to go past the middle of the input string.

