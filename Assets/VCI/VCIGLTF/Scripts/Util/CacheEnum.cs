using System;
using System.Collections.Generic;

namespace VCIGLTF
{
    public static class CacheEnum
    {
        public static T Parse<T>(string name, bool ignoreCase = false) where T : struct, Enum
        {
            if(ignoreCase)
            {
                return CacheParse<T>.ParseIgnoreCase(name);
            }
            else
            {
                return CacheParse<T>.Parse(name);
            }
        }

        public static T TryParseOrDefault<T>(string name,  bool ignoreCase = false, T defaultValue=default(T)) where T : struct, Enum
        {
            try
            {
                if(ignoreCase)
                {
                    return CacheParse<T>.ParseIgnoreCase(name);
                }
                else
                {
                    return CacheParse<T>.Parse(name);
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T[] GetValues<T>() where T : struct, Enum
        {
            return CacheValues<T>.Values;
        }

        public static string[] GetNames<T>() where T : struct, Enum
        {
            return CacheNames<T>.Names;
        }

        private static class CacheParse<T> where T : struct, Enum
        {
            private static readonly Dictionary<string, T> Values = new Dictionary<string, T>();
            private static readonly Dictionary<string, T> IgnoreCaseValues = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

            static CacheParse()
            {
            }

            public static T ParseIgnoreCase(string name)
            {
                if(IgnoreCaseValues.TryGetValue(name, out var value))
                {
                    return value;
                }
                else
                {
                    T result;
                    value =  Enum.TryParse<T>(name, true, out result)
                        ? result
                        : throw new ArgumentException(nameof(result));
                    IgnoreCaseValues.Add(name, value);
                    return value;
                }
            }

            public static T Parse(string name)
            {
                if(Values.TryGetValue(name, out var value))
                {
                    return value;
                }
                else
                {
                    T result;
                    value =  Enum.TryParse<T>(name, false, out result)
                        ? result
                        : throw new ArgumentException(nameof(result));
                    Values.Add(name, value);
                    return value;
                }
            }
        }

        private static class CacheValues<T> where T : struct, Enum
        {
            public static readonly T[] Values;

            static CacheValues()
            {
                Values = Enum.GetValues(typeof(T)) as T[];
            }
        }

        private static class CacheNames<T> where T : struct, Enum
        {
            public static readonly string[] Names;
            static CacheNames()
            {
                Names = Enum.GetNames(typeof(T));
            }
        }
    }
}

