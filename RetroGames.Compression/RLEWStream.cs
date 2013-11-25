using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroGames.Compression
{

    /// <summary>
    /// RLEWStream
    /// Implement RLEW decompression algorithm, read-only
    /// </summary>
    public class RLEWStream : CompressedStream
    {

        override protected bool Process() {
            if (!this.BaseStream.CanRead) { return false; }

            BinaryReader br = new BinaryReader(this.BaseStream);
            SetLength(br.ReadInt32());
            buffer = new byte[Length];
            
            UInt16 word = br.ReadUInt16();
            for (int ptr = 0; ptr < Length; word = br.ReadUInt16())
            {
                UInt16 time = 1;

                if (word == 0xFEFE)
                {
                    time = br.ReadUInt16();
                    word = br.ReadUInt16();
                } 
                
                byte[] bytes = BitConverter.GetBytes(word); /// TODO : Skip this call and get bytes directly instead of using br.readUInt16 for the word
                for (UInt16 i = 0; i < time; i++, ptr += 2)
                {
                    Buffer.BlockCopy(bytes, 0, buffer, ptr, 2);
                }
            }

            return isProcessed = true;
        }

        public RLEWStream(Stream baseStream) : base(baseStream)
        {
        }

        public RLEWStream(Stream baseStream, bool preprocess) : base(baseStream, preprocess)
        {
        }

    }
}
