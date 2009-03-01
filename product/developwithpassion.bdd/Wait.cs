using System;
using System.Threading;

namespace developwithpassion.bdd
{
    public class Wait
    {
        const int default_timeout = 2000;

        public static void until(Func<bool> condition_is_met)
        {
            until(condition_is_met,default_timeout);
        }

        public static void until(Func<bool> condition_is_met,int timeout_in_milliseconds)
        {
            var elapsed_time = 0;

            while (! condition_is_met())
            {
                Thread.Sleep(100);
                elapsed_time += 100;
                if (elapsed_time >= timeout_in_milliseconds) throw new Exception("The timeout time has elapsed");
            }
        }
    }
}