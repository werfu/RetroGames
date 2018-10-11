using System;
using System.IO;
using System.Text;

namespace RetroGames.FileFormats
{
    public struct WADHeader
    {
        public string Magic;
        public Int32 LumpCount;
        public Int32 DirectoryOffset;
    }

    public struct FileLump
    {
        public Int32 FileOffset;
        public Int32 FileSize;
        public string Name;
        internal Func<byte[]> DataAccessor;
        public byte[] Data => this.DataAccessor();
    }

    public class WAD
    {
        private BinaryReader br;

        public WAD(Stream source)
        {
            br = new BinaryReader(source);

            Header = new WADHeader()
            {
                Magic = Encoding.ASCII.GetString(br.ReadBytes(4)),
                LumpCount = br.ReadInt32(),
                DirectoryOffset = br.ReadInt32()
            };

            source.Seek(Header.DirectoryOffset, SeekOrigin.Begin);

            Lumps = new FileLump[Header.LumpCount];
            for(int i = 0; i < Header.LumpCount; i++)
            {
                Lumps[i] = new FileLump()
                {
                    FileOffset = br.ReadInt32(),
                    FileSize = br.ReadInt32(),
                    Name = Encoding.ASCII.GetString(br.ReadBytes(8))
                };

                Lumps[i].DataAccessor = () => {
                    if (Lumps[i].FileSize == 0)
                        return null;

                    source.Seek(Lumps[i].FileOffset, SeekOrigin.Begin);
                    return br.ReadBytes(Lumps[i].FileSize);
                };
            }
        }

        public WADHeader Header { get; private set; }
        public FileLump[] Lumps { get; private set; }
    }
}
