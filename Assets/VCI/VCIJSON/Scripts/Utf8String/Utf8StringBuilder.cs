using System.Text;

namespace VCIJSON
{
    public class Utf8StringBuilder
    {
        private ByteBuffer m_buffer = new ByteBuffer();

        public void Ascii(char c)
        {
            m_buffer.Push((byte) c);
        }

        private static Encoding s_utf8 = new UTF8Encoding(false);

        public void Quote(string text)
        {
            Ascii('"');
            m_buffer.Push(s_utf8.GetBytes(text));
            Ascii('"');
        }

        public void Add(Utf8String str)
        {
            m_buffer.Push(str.Bytes);
        }

        public Utf8String ToUtf8String()
        {
            return new Utf8String(m_buffer.Bytes);
        }
    }
}