using System;
using Xunit;
using Xunit.Abstractions;
using System.Diagnostics;

namespace palindrome.tests
{
    public class UnitTest
    {
        private readonly ITestOutputHelper _output;
        Stopwatch sw;

        public UnitTest(ITestOutputHelper output)
        {
            sw  = new Stopwatch();
            _output = output;
        }

        [Theory]
        [InlineData("rotor", true)]
        [InlineData("rater", false)]
        [InlineData("ele’ele", true)]
        [InlineData("pullup", true)]
        [InlineData("aoxomoxoa", true)]
        [InlineData("malayalam", true)]
        [InlineData("deleveled", true)]
        [InlineData("releveler", true)]
        [InlineData("rotavator", true)]
        [InlineData("aibohphobia", true)]
        [InlineData("ailihphilia", true)]
        [InlineData("elihphile", true)]
        [InlineData("tattarrattat", true)]
        [InlineData("redivider", true)]
        [InlineData("evitative", true)]
        [InlineData("1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 01 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111", false)]
        public void TestByIndex(string sample, bool expected)
        {
            sw.Reset();
            sw.Start();
            var sut = Program.IsPalindrome_ByIndex(sample);
            sw.Stop();

            _output.WriteLine($"[{sw.ElapsedTicks}]");

            Assert.Equal(expected,sut);
        }

        [Theory]
        [InlineData("rotor", true)]
        [InlineData("rater", false)]
        [InlineData("ele’ele", true)]
        [InlineData("pullup", true)]
        [InlineData("aoxomoxoa", true)]
        [InlineData("malayalam", true)]
        [InlineData("deleveled", true)]
        [InlineData("releveler", true)]
        [InlineData("rotavator", true)]
        [InlineData("aibohphobia", true)]
        [InlineData("ailihphilia", true)]
        [InlineData("elihphile", true)]
        [InlineData("tattarrattat", true)]
        [InlineData("redivider", true)]
        [InlineData("evitative", true)]
        [InlineData("1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 01 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111", false)]
        public void TestWithLinq(string sample, bool expected)
        {
            sw.Reset();
            sw.Start();
            var sut = Program.IsPalindrome_WithLinq(sample);
            sw.Stop();

            _output.WriteLine($"[{sw.ElapsedTicks}]");

            Assert.Equal(expected,sut);
        }

        [Theory]
        [InlineData("rotor", true)]
        [InlineData("rater", false)]
        [InlineData("ele’ele", true)]
        [InlineData("pullup", true)]
        [InlineData("aoxomoxoa", true)]
        [InlineData("malayalam", true)]
        [InlineData("deleveled", true)]
        [InlineData("releveler", true)]
        [InlineData("rotavator", true)]
        [InlineData("aibohphobia", true)]
        [InlineData("ailihphilia", true)]
        [InlineData("elihphile", true)]
        [InlineData("tattarrattat", true)]
        [InlineData("redivider", true)]
        [InlineData("evitative", true)]
        [InlineData("1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 01 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111 1111", false)]
        public void TestWithRecursion(string sample, bool expected)
        {
            sw.Reset();
            sw.Start();
            var sut = Program.IsPalindrome_WithRecursion(sample);
            sw.Stop();

            _output.WriteLine($"[{sw.ElapsedTicks}]");

            Assert.Equal(expected,sut);
        }

    }
}
