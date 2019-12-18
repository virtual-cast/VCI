using System.IO;
using NAudio.Wave;
using NLayer.NAudioSupport;

namespace VCI
{
    public static class MP3Util
    {
        public static byte[] ToWavData(byte[] bytes)
        {
            using (var mp3Stream = new MemoryStream(bytes))
            using (var outStream = new MemoryStream())
            using (var reader = new Mp3FileReader(mp3Stream, wf => new Mp3FrameDecompressor(wf)))
            {
                WaveFileWriter.WriteWavFileToStream(outStream, new WaveFloatTo16Provider(reader));
                return outStream.ToArray();
            }
        }
    }
}
