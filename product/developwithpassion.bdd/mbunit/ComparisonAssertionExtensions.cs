using System;
using MbUnit.Framework;

namespace developwithpassion.bdd.mbunit
{
    static public class ComparisonAssertionExtensions
    {
        static public void should_be_greater_than<T>(this T item, T other) where T : IComparable<T>
        {
            (item.CompareTo(other) > 0).should_be_true();
        }

        static public void should_be_greater_than_or_equal_to<T>(this T item, T other) where T : IComparable<T>
        {
            (item.CompareTo(other) >= 0).should_be_true();
        }

        static public void should_be_less_than<T>(this T item, T other) where T : IComparable<T>
        {
            (item.CompareTo(other) < 0).should_be_true();
        }

        static public void should_be_less_than_or_equal_to<T>(this T item, T other) where T : IComparable<T>
        {
            (item.CompareTo(other) <= 0).should_be_true();
        }

        static public void should_not_be_equal_to<T>(this T item, T other)
        {
            Assert.AreNotEqual(item, other);
        }

        static public void should_be_equal_to<T>(this T actual, T expected)
        {
            Assert.AreEqual(expected, actual);
        }
    }
}