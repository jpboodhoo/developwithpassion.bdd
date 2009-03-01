using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.bdd.core.extensions;
using MbUnit.Framework;

namespace developwithpassion.bdd.mbunit
{
    static public class EnumerableAssertionExtensions
    {
        static public void force_traversal<T>(this IEnumerable<T> items)
        {
            items.Count();
        }

        static public void should_contain_item_matching<T>(this IEnumerable<T> items, Predicate<T> condition)
        {
            Assert.IsTrue(new List<T>(items).Exists(condition));
        }

        static public void should_not_contain_item_matching<T>(this IEnumerable<T> items, Predicate<T> condition)
        {
            Assert.IsFalse(new List<T>(items).Exists(condition));
        }

        static public void should_contain<T>(this IEnumerable<T> items, T item)
        {
            Assert.IsTrue(new List<T>(items).Contains(item));
        }

        static public void should_contain<T>(this IEnumerable<T> items, params T[] items_that_should_be_found)
        {
            items_that_should_be_found.each(x => should_contain(items, x));
        }

        static public void should_not_contain<T>(this IEnumerable<T> items, params T[] items_that_should_not_be_found)
        {
            var list = new List<T>(items);
            foreach (var item in items_that_should_not_be_found) Assert.IsFalse(list.Contains(item));
        }

        static public void should_only_contain<T>(this IEnumerable<T> items, params T[] itemsToFind)
        {
            var results = new List<T>(items);
            itemsToFind.Length.should_be_equal_to(items.Count());
            foreach (var itemToFind in itemsToFind)
            {
                results.Contains(itemToFind).should_be_true();
            }
        }

        static public void should_only_contain_in_order<T>(this IEnumerable<T> items, params T[] itemsToFind)
        {
            var results = new List<T>(items);
            itemsToFind.Length.should_be_equal_to(items.Count());
            for (var i = 0; i < itemsToFind.Count(); i++)
            {
                itemsToFind[i].should_be_equal_to(results[i]);
            }
        }
    }
}