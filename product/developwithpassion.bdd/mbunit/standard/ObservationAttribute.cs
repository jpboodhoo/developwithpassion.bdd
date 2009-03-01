using System;
using MbUnit.Core.Framework;

namespace developwithpassion.bdd.mbunit.standard
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ObservationAttribute : TestPatternAttribute {}
}