
using System;
using System.Reflection;


namespace VCIJSON
{
    public static partial class GenericInvokeCallFactory
    {

//////////// StaticAction

        public static Action<A0> StaticAction<A0>(MethodInfo m)
        {
            if (!m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is not static", m));
            }

            return (Action<A0>)Delegate.CreateDelegate(typeof(Action<A0>), null, m);
        }


        public static Action<A0, A1> StaticAction<A0, A1>(MethodInfo m)
        {
            if (!m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is not static", m));
            }

            return (Action<A0, A1>)Delegate.CreateDelegate(typeof(Action<A0, A1>), null, m);
        }


        public static Action<A0, A1, A2> StaticAction<A0, A1, A2>(MethodInfo m)
        {
            if (!m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is not static", m));
            }

            return (Action<A0, A1, A2>)Delegate.CreateDelegate(typeof(Action<A0, A1, A2>), null, m);
        }


        public static Action<A0, A1, A2, A3> StaticAction<A0, A1, A2, A3>(MethodInfo m)
        {
            if (!m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is not static", m));
            }

            return (Action<A0, A1, A2, A3>)Delegate.CreateDelegate(typeof(Action<A0, A1, A2, A3>), null, m);
        }

//////////// OpenAction

        public static Action<S, A0> OpenAction<S, A0>(MethodInfo m)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Action<S, A0>)Delegate.CreateDelegate(typeof(Action<S, A0>), m);
        }


        public static Action<S, A0, A1> OpenAction<S, A0, A1>(MethodInfo m)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Action<S, A0, A1>)Delegate.CreateDelegate(typeof(Action<S, A0, A1>), m);
        }


        public static Action<S, A0, A1, A2> OpenAction<S, A0, A1, A2>(MethodInfo m)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Action<S, A0, A1, A2>)Delegate.CreateDelegate(typeof(Action<S, A0, A1, A2>), m);
        }

//////////// BindAction

        public static Action<A0> BindAction<S, A0>(MethodInfo m, S instance)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Action<A0>)Delegate.CreateDelegate(typeof(Action<A0>), instance, m);
        }


        public static Action<A0, A1> BindAction<S, A0, A1>(MethodInfo m, S instance)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Action<A0, A1>)Delegate.CreateDelegate(typeof(Action<A0, A1>), instance, m);
        }


        public static Action<A0, A1, A2> BindAction<S, A0, A1, A2>(MethodInfo m, S instance)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Action<A0, A1, A2>)Delegate.CreateDelegate(typeof(Action<A0, A1, A2>), instance, m);
        }


        public static Action<A0, A1, A2, A3> BindAction<S, A0, A1, A2, A3>(MethodInfo m, S instance)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Action<A0, A1, A2, A3>)Delegate.CreateDelegate(typeof(Action<A0, A1, A2, A3>), instance, m);
        }

//////////// StaticFunc

        public static Func<A0, T> StaticFunc<A0, T>(MethodInfo m)
        {
            if (!m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is not static", m));
            }

            return (Func<A0, T>)Delegate.CreateDelegate(typeof(Func<A0, T>), null, m);
        }


        public static Func<A0, A1, T> StaticFunc<A0, A1, T>(MethodInfo m)
        {
            if (!m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is not static", m));
            }

            return (Func<A0, A1, T>)Delegate.CreateDelegate(typeof(Func<A0, A1, T>), null, m);
        }


        public static Func<A0, A1, A2, T> StaticFunc<A0, A1, A2, T>(MethodInfo m)
        {
            if (!m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is not static", m));
            }

            return (Func<A0, A1, A2, T>)Delegate.CreateDelegate(typeof(Func<A0, A1, A2, T>), null, m);
        }


        public static Func<A0, A1, A2, A3, T> StaticFunc<A0, A1, A2, A3, T>(MethodInfo m)
        {
            if (!m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is not static", m));
            }

            return (Func<A0, A1, A2, A3, T>)Delegate.CreateDelegate(typeof(Func<A0, A1, A2, A3, T>), null, m);
        }

//////////// OpenFunc

        public static Func<S, A0, T> OpenFunc<S, A0, T>(MethodInfo m)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Func<S, A0, T>)Delegate.CreateDelegate(typeof(Func<S, A0, T>), m);
        }


        public static Func<S, A0, A1, T> OpenFunc<S, A0, A1, T>(MethodInfo m)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Func<S, A0, A1, T>)Delegate.CreateDelegate(typeof(Func<S, A0, A1, T>), m);
        }


        public static Func<S, A0, A1, A2, T> OpenFunc<S, A0, A1, A2, T>(MethodInfo m)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Func<S, A0, A1, A2, T>)Delegate.CreateDelegate(typeof(Func<S, A0, A1, A2, T>), m);
        }

//////////// BindFunc

        public static Func<A0, T> BindFunc<S, A0, T>(MethodInfo m, S instance)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Func<A0, T>)Delegate.CreateDelegate(typeof(Func<A0, T>), instance, m);
        }


        public static Func<A0, A1, T> BindFunc<S, A0, A1, T>(MethodInfo m, S instance)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Func<A0, A1, T>)Delegate.CreateDelegate(typeof(Func<A0, A1, T>), instance, m);
        }


        public static Func<A0, A1, A2, T> BindFunc<S, A0, A1, A2, T>(MethodInfo m, S instance)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Func<A0, A1, A2, T>)Delegate.CreateDelegate(typeof(Func<A0, A1, A2, T>), instance, m);
        }


        public static Func<A0, A1, A2, A3, T> BindFunc<S, A0, A1, A2, A3, T>(MethodInfo m, S instance)
        {
            if (m.IsStatic)
            {
                throw new ArgumentException(string.Format("{0} is static", m));
            }

            return (Func<A0, A1, A2, A3, T>)Delegate.CreateDelegate(typeof(Func<A0, A1, A2, A3, T>), instance, m);
        }


    }
}

