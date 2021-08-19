using System.Collections.Generic;

namespace TimerClases.Objects
{
    public static class IntCounter
    {
        private static readonly Dictionary<string, int> Counters = new Dictionary<string, int>();

        public static int GetNextValue(string ObjectName)
        {
            if (!Counters.ContainsKey(ObjectName))
            {
                Counters.Add(ObjectName, 0);
            }

            return ++Counters[ObjectName];

        }

        public static void ClearCounters()
        {
            Counters.Clear();
        }

        public static void SetValue(string ObjectName, int val)
        {
            if (!Counters.ContainsKey(ObjectName))
            {
                Counters.Add(ObjectName, val);
            }
            else
            {
                Counters[ObjectName] = val;
            }
        }
    }
}
