using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace developwithpassion.bdd.core.extensions
{
    static public class TypeExtensions
    {
        public const string generic_argument_type_format = "<{0}>";

        static public ConstructorInfo greediest_constructor(this Type type)
        {
            return type.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Count())
                .First();
        }


        static public string proper_name(this Type type)
        {
            var message = new StringBuilder(type.Name);
            if (!type.IsGenericType) return message.ToString();

            type.GetGenericArguments().each(x => message.AppendFormat(generic_argument_type_format, x));

            return message.ToString();
        }
    }
}