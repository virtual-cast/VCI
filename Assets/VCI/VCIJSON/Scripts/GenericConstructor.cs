using System;
using System.Reflection;

namespace VCIJSON
{
    internal struct GenericConstructor<T, U>
        where T : IListTreeItem, IValue<T>
    {
        private static V[] ArrayCreator<V>(ListTreeNode<T> src)
        {
            if (!src.IsArray()) throw new ArgumentException("value is not array");
            var count = src.GetArrayCount();
            return new V[count];
        }

        private static Func<ListTreeNode<T>, U> GetCreator()
        {
            var t = typeof(U);
            if (t.IsArray)
            {
                var mi = typeof(GenericConstructor<T, U>).GetMethod("ArrayCreator",
                    BindingFlags.NonPublic | BindingFlags.Static);
                var g = mi.MakeGenericMethod(t.GetElementType());
                return GenericInvokeCallFactory.StaticFunc<ListTreeNode<T>, U>(g);
            }

            {
                return _s => { return Activator.CreateInstance<U>(); };
            }
        }

        private delegate U Creator(ListTreeNode<T> src);

        private static Creator s_creator;

        public U Create(ListTreeNode<T> src)
        {
            if (s_creator == null)
            {
                var d = GetCreator();
                s_creator = new Creator(d);
            }

            return s_creator(src);
        }
    }
}