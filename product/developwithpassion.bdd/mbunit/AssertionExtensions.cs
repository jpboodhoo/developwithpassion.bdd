using System;
using MbUnit.Framework;

namespace developwithpassion.bdd.mbunit
{
    static public class AssertionExtensions
    {
        static public void should_be_null(this object item)
        {
            Assert.IsNull(item);
        }

        static public void should_not_be_null(this object item)
        {
            Assert.IsNotNull(item);
        }

        static public void should_be_true(this bool item)
        {
            item.should_be_equal_to(true);
        }

        static public void should_be_false(this bool item)
        {
            item.should_be_equal_to(false);
        }

        static public void should_be_equal_ignoring_case(this string item, string other)
        {
            Assert.AreEqual(other.ToLower(), item.ToLower());
        }

        static public void should_not_throw_any_exceptions(this Action work_to_perform)
        {
            work_to_perform();
        }

        static public Type should_be_an_instance_of<Type>(this object item)
        {
            Assert.IsInstanceOfType(typeof (Type), item);
            return (Type) item;
        }
    }
}