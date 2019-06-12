using System;
using System.Linq;
using NUnit.Framework;

namespace VCIJSON.MsgPack
{
    [TestFixture]
    public class StringTest
    {
        [Test]
        public void str()
        {
            var f = new MsgPackFormatter();
            f.Value("文字列");
            var bytes = f.GetStoreBytes();

            var v = MsgPackParser.Parse(bytes).GetValue();
            Assert.AreEqual("文字列", v);
        }

        [Test]
        public void fix_str()
        {
            for (var i = 1; i < 32; ++i)
            {
                var str = String.Join("", Enumerable.Range(0, i).Select(_ => "0").ToArray());
                var f = new MsgPackFormatter();
                f.Value(str);
                var bytes = f.GetStoreBytes();

                var value = MsgPackParser.Parse(bytes);
                Assert.AreEqual(i, ((String) value.GetValue()).Length);
            }
        }
    }
}