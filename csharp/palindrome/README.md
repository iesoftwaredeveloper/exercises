# Palindrome

<https://github.com/iesoftwaredeveloper/exercises/tree/main/csharp/palindrome>

A palindrome is a word, number, phrase or other sequence of characters that reads the same forwards as it does backwards.

Some examples of a numeric palindrome are 777 or 12321.  A palindrome can be a date such as 02-02-2020 or a time like 12:21.

This is a series of examples intended to demonstrate a variety of ways to use palindrome to demonstrate csharp code.  

## Problem

Given a string `s` determine if it is a palindrome.

## Discussion

There are a number of algorithms that can be derived to determine if a string is a palindrome.  

When writing code it is generally preferred to write it in the smallest most efficient manner to reduce time and space (i.e. memory) requirements while maintaining readability.  That is not the goal here.  The goal here is to express mutliple methods for determining if a string is a palindrome.

Additionally, there are a number of factors that may or may not need to be considered when determining if a sequence of characters is a palindrome.  Should punctuation be considered?  Do symbols like a slash or a dash count?  What about whitespace?

For the purposes of this exercise I will presume that any string passed in has been normalized and that I do not need to consider any of those questions.  If whitespace is irrelevant then it is presumed that the whitespace has been removed by some other prior method.

## Solution: Loop

Since the input is a string, I can easily compare individual characters in the string in most languages by using an index.

In this solution I will simply inspect each set of characters to determine if they match each other.  If at any time I find a set of characters that does not match I know immediately that the string is not a palindrome.

### Algorithm: Loop

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

```csharp
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

The time complexity of this implementation is O(n/2).  In the event that the input string is a single character I have no comparisons to make and I have a constant number of operations.  If this were the worst case scenario it would make our algorithem a O(1).

Alas this algorithm is not O(1).  Instead the number of comparisons depends upon the size of the input string.  Regardless of the number of characters in the input string I will never have more than n/2 operations.  This is because I compare the first the two characters that mirror each other each time I loop through the string.  The comparison will never have to go past the middle of the input string.

## Solution: Linq

The straight forward method for determining if a string is a palindrome is to compare the string to the reverse of the string.  If the reverse of the string matches the original string then it is a palindrome.

In some languages reversing a string is simple and straight forward by using a reverse method.  In C# the `String` class does not have a reverse method.  This requires that I use some other method to reverse the string.  The process of reversing a string is relatively straight forward and I could write my own method to do this, but I would prefer to use some feature of the language to do this instead.  In C# that means using Linq.

### Algorithm: Linq

1. Reverse the string using the Linq `Reverse()` method
2. Convert the result of `Reverse()` to an array. (Reverse returns an enumerable)
3. Create a new string to represent the newly reversed string
4. If the reversed string is equal to the original string, return true, else false

### Implementation: Linq

```csharp
public static bool IsPalindrome(string s)
{
    return s.Equals(new string(s.Reverse().ToArray()));
}
```

This solution meets several key ideas.  As a single line solution it is compact and concise.  It is easy to read and understand what is occurring.  They only step that might not be obvious is the `ToArray()`.  As a regular user of C# and Linq or being accustomed to strings being created from arrays of characters then this is easy to reconcile.

This case also handles the edge cases of the string being less than 2 characters with no special logic.

A concern with this implementation is that I do not know how the reversal and to array is being implemented.  With the power and memory of modern machines this can often be ignored.  And if only a few strings are being processing then the amount of CPU and memory being used is rather insignificant.  If thousands or millions of strings are processing it can quickly add up.

Given that I do not know the implementation of the items I cannot compute a real big O for the time complexity of this implementation.  Considering just the code that is used it is effectively a O(1) because each operation is performed once.  In reality I suspect the complexity is probably more like a O(n) upon inspecting the implementation of the `Reverse()` and other methods used.

What about memory?  This solution does require that additional memory be allocated to create a second version of the string.  When the string is only a few characters this is not a major issue.  When processing very large strings the amount of memory used can become an issue.  Since an entire replica of the original string is created the space complexity of this implementation is O(n).

## Solution: Recursion

Recursion is often used to break a problem down into a smaller unit to simplify the test.  Testing for a palindrome can also be done using recursion.  This is similar to using a loop with two indexes to test.  Instead of tracking an index the substring is tested by sending a substring to be tested using the same method.

### Algorithm: Recursion

1. If the string is less than or equal to a length of 1, then return true
2. If the first character is not equal to the last character, return false
3. Test if the substring without the first and last character is a palindrome

### Implementation: Recursion

```csharp
public static bool IsPalindrome_WithRecursion(string s)
{
    // A string of length 0 or 1, then it is a palindrome.
    if(s.Length <= 1)
        return true;

    if(s[0] != s[s.Length -1])
        return false;

    return IsPalindrome_WithRecursion(s.Substring(1, s.Length - 2));
}
```

With any recursive implementation there must be some method that will indicate the recursion should stop.  A string that is less than 2 characters in length is the test that will end the recursion in this implementation.

If the recursion does not need to be ended, then the test is performed.  If the test fails then the recursion is ended.  If the test passes, then the next interation of the recursion is performed.

When considering time complexity this method is similar to the index method.  This is because each set of characters has to be tested to determine if they are equal.  This results in a time complexity of O(n).

When looking at space complexity the recursive method has some properties that are specific to recursion.  The obvious is that a new substring must be created each time to pass it to the next iteration of the recursion.

The first time through the method the memory allocated is n - 2 when the length of the string is greater than 1.  As the length of the string increase the amount of total memory that has to be allocted increases as well.  Each iteration of the recursion the amount of memory allocated for that individual iteration is always 2 less than the previous iteration.

The memory allocation is really a sum of a series of numbers.  While not an exact formula the general sum of memory that needs to be allocated for a string to be tested with the recursive method is (n/2) x (n-2) when the length of the original string is odd.  If the length of the original string is even then the amount of memory allocated is (n/2) x (n-2) - ((n-2)/2).

In order to express this in big O notation I will estimate it as one half n to the power of 2 or O((n^2)/2). This is a rough estimation and the actual performance is better than this.  As the size of the string increases, the estimated big O is much further from the actual value.  However, since the intent of the big O notation is to provide a quick estimate of the complexity it makes for a good way to compare to other methods.

One last thing to consider when using recursion is the call stack.  Each time the method is called it requires that the the current memory space be placed onto the call stack.  This increases the time and spaced used by the system to manage the call stack.

## Solution: Rube Goldberg

For the last method I decided to take an approach that was more complicated than necessary.  To see why I chose the name of the solution see [Rube Goldberg machine](https://en.wikipedia.org/wiki/Rube_Goldberg_machine).

### Algorithm: Rube Goldberg

1. Get the midpoint of the string, accounting for an odd or even length string.
2. Get the last half of the original string as a substring that should be tested for equality with the first half of the string.
3. Reverse the second half of the string.
4. Test if the first half of the string is equal to the reversed half of the second string.

### Implementation: Rube Goldberg

```csharp
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
```

This implementation is not so easy to understand.  It works, but there is a lot of work going on that is just not necessary.

The first thing that is done is to determine the midpoint of the string.  This is required to extract the substring that will be reversed.

To avoid using Linq which was used in a previous implementation the extracted substring must be converted to a character array.  This is necessary because the `String` class does not have a built in `reverse` method.  In order to reverse the substring the `Reverse()` method of the `Array` class is being used.  This requires that the item to be reversed is an array of characters.

In order to test the two parts for equality it is necessary to extract the first half of the string as a substring so that it can be compared to the reversed substring.  Since the reversed substring is now an array of characters it needs to first be converted back into a string.

Finally after extracting both halves of the string and converting them to the same type the test for equality can be made.

When evaluating the performance of the algoritm the time complexity is similar to the Linq method.  There are a fixed number of operations and they all are executed the same number of times regardless of the size of the input string.  Since the methods that are doing the work are not exposed the actual time complexity cannot be reliably determined.  The complexity of the code that is written is O(1) because the same number of operations are performed for all problem sets.  In reality the complexity is probably more O(n).

The space complexity is also not able to be reliably determined since the memory allocation of the standard methods used are not known.  The code that is in the implementation can be used to determine a best guess though.  

The allocation of the midpoint is of type O(1) since it does not depend on the size of the input string.

The allocation of space for the `mirror` substring is interesting.  The final `mirror` will hold a character array the size of n/2.  In order to create the array a substring of size n/2 must first be created.  This effectively requires a space complexity of O(n) to get an array of size n/2.

The last line of the implementation that does the comparison has several space allocations.  First there is an additional string allocated from the `mirror` in order to convert it from a character array to a string.  The size of this string is n/2.  In order to compare only the first half of the input string another substring must be allocated for the first half of the input string.  This is also of size n/2.  This makes the last line a O(n/2) space complexity.

This makes the overall space complexity of this implementation a O(n) implementation.

## Performance

After evaluating the methods the real question is which is better?  I ran several tests and used the [Stopwatch](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch?view=net-5.0) to time each of the test cases.  Given that all of the methods are relatively quick I had to use the [ElapsedTicks](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.stopwatch.elapsedticks?view=net-5.0) in order to get any significant resolution in the test results.  I also observed that the first test case that runs for each method take a bit of setup time and is significantly longer that the other tests.  For this reason I remove the highest and lowest times to mitigate this affect.

Here is what the results were.

| Method | Max Ticks | Min Ticks | Mean Ticks | Median Ticks | Sum | Space | Time |
|--------|-----------|-----------|------------|--------------|-----|-------|------|
| By Index | 576 | 100 | 145.9285714 | 110.5 | 2043 | O(1) | O(n/2) |
| WithLinq | 10723 | 907 | 2117.142857 | 1144 | 29640 | O(n) | O(n)? |
| WithRecursion | 3908 | 195 | 565.5714286 | 214.5 | 7918 | O((n^2)/2) | O(n) |
| WithRubeGoldberg | 559 | 409 | 455.2857143 | 451.5 | 6374 | O(n) | O(n/2) |

| Sample | WithIndex | WithLinq | WithRecursion | WithRubeGoldberg | MAX | MIN | Median | Sum |
|--------|-----------|----------|---------------|------------------|-----|-----|--------|-----|
| ailihphilia  | 92171 | 983     | 227    | 428    | 983   | 428 | 705.5  | 1411 |
| redivider    | 129   | 1773    | 195    | 427    | 427   | 195 | 311    | 622 |
| malayalam    | 105   | 982     | 218    | 475    | 475   | 218 | 346.5  | 693 |
| rotavator    | 114   | 917     | 212    | 434    | 434   | 212 | 323    | 646 |
| ele’ele      | 108   | 903     | 201    | 559    | 559   | 201 | 380    | 760 |
| rater        | 113   | 907     | 134517 | 409    | 907   | 409 | 658    | 1316 |
| deleveled    | 125   | 1110    | 229    | 450    | 450   | 229 | 339.5  | 679 |
| aoxomoxoa    | 101   | 1181    | 214    | 459    | 459   | 214 | 336.5  | 673 |
| rotor        | 100   | 1178    | 170    | 454    | 454   | 170 | 312    | 624 |
| aibohphobia  | 134   | 970     | 363    | 403    | 403   | 363 | 383    | 766 |
| releveler    | 106   | 10723   | 204    | 133357 | 10723 | 204 | 5463.5 | 10927 |
| evitative    | 98    | 1701345 | 215    | 418    | 418   | 215 | 316.5  | 633 |
| tattarrattat | 125   | 1644    | 1320   | 453    | 1320  | 453 | 886.5  | 1773 |
| elihphile    | 106   | 1351    | 205    | 453    | 453   | 205 | 329    | 658 |
| pullup       | 101   | 915     | 207    | 426    | 426   | 207 | 316.5  | 633 |
| 1111 111 ... | 576   | 5006    | 3908   | 529    | 3908  | 576 | 2242   | 4484 |

## Conclusion

Using Linq to reverse the string and check for the two being equal is a compact way of testing for a palindrome.  However, it is not the most effecient method.

Since the implementation of the Linq methods are not known, it isn't possible to determine the complexity of the method.  Based upon the timing it is clearly the worst performing option.

The recursion and Rube Goldberg methods are about equivalent in the performance.

It should not be a surprise that the loop method is the most performant based upon the big O notation.

## Data

Here is the actual data from the tests.

### WithIndex

| Ticks | Sample |
| ----- | ------ |
| 92171 | ailihphilia |
| 129 | redivider |
| 105 | malayalam |
| 114 | rotavator |
| 108 | ele’ele |
| 113 | rater |
| 125 | deleveled |
| 101 | aoxomoxoa |
| 100 | rotor |
| 134 | aibohphobia |
| 106 | releveler |
| 98 | evitative |
| 125 | tattarrattat |
| 106 | elihphile |
| 101 | pullup |
| 576 | 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 01 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 |

### WithLinq

| Ticks | Sample |
| ----- | ------ |
| 983 | ailihphilia |
| 1773 | redivider |
| 982 | malayalam |
| 917 | rotavator |
| 903 | ele’ele |
| 907 | rater |
| 1110 | deleveled |
| 1181 | aoxomoxoa |
| 1178 | rotor |
| 970 | aibohphobia |
| 10723 | releveler |
| 1701345 | evitative |
| 1644 | tattarrattat |
| 1351 | elihphile |
| 915 | pullup |
| 5006 | 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 01 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 |

### WithRecursion

| Ticks | Sample |
| ----- | ------ |
| 227 | ailihphilia |
| 195 | redivider |
| 218 | malayalam |
| 212 | rotavator |
| 201 | ele’ele |
| 134517 | rater |
| 229 | deleveled |
| 214 | aoxomoxoa |
| 170 | rotor |
| 363 | aibohphobia |
| 204 | releveler |
| 215 | evitative |
| 1320 | tattarrattat |
| 205 | elihphile |
| 207 | pullup |
| 3908 | 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 01 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 |

### WithRubeGoldberg

| Ticks | Sample |
| ----- | ------ |
| 428 | ailihphilia |
| 427 | redivider |
| 475 | malayalam |
| 434 | rotavator |
| 559 | ele’ele |
| 409 | rater |
| 450 | deleveled |
| 459 | aoxomoxoa |
| 454 | rotor |
| 403 | aibohphobia |
| 133357 | releveler |
| 418 | evitative |
| 453 | tattarrattat |
| 453 | elihphile |
| 426 | pullup |
| 529 | 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 01 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 |
