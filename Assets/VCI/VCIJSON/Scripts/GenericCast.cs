using System;

namespace VCIJSON
{
    internal struct GenericCast<S, T>
    {
        public static T Null()
        {
            if (typeof(T).IsClass)
                return default(T);
            else
                throw new MsgPackTypeException("can not null");
        }

        private delegate T CastFunc(S value);

        private static CastFunc s_cast;

        private delegate Func<T> ConstFuncCreator(S value);

        private static ConstFuncCreator s_const;

        public static Func<T> Const(S value)
        {
            if (s_const == null) s_const = new ConstFuncCreator(GenericCast.CreateConst<S, T>());
            return s_const(value);
        }

        public static T Cast(S value)
        {
            if (s_cast == null) s_cast = new CastFunc(GenericCast.CreateCast<S, T>());
            return s_cast(value);
        }
    }

    internal static partial class GenericCast
    {
        public static Func<S, T> CreateCast<S, T>()
        {
            var mi = ConcreteCast.GetMethod(typeof(S), typeof(T));
            if (mi == null)
                return (Func<S, T>) ((S s) => { return (T) (object) s; });
            else
                return GenericInvokeCallFactory.StaticFunc<S, T>(mi);
        }

        public static Func<S, Func<T>> CreateConst<S, T>()
        {
            var cast = CreateCast<S, T>();
            return (Func<S, Func<T>>) ((S s) => { return (Func<T>) (() => cast(s)); });
        }
    }
}