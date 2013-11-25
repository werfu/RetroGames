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
    public class RLEWStream : Stream
    {
        #region privates
        private Stream _baseStream;
        private int finalLength;
        private byte[] buffer;
        private int innerPosition;
        #endregion

        private bool Process() {
            if (!this._baseStream.CanRead) { return false; }

            BinaryReader br = new BinaryReader(this._baseStream);
            finalLength = br.ReadInt32();
            buffer = new byte[finalLength];
            
            UInt16 word = br.ReadUInt16();
            for (int ptr = 0; ptr < finalLength; word = br.ReadUInt16())
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

        public RLEWStream(Stream baseStream)
        {
            this._baseStream = baseStream;
        }

        public RLEWStream(Stream baseStream, bool preprocess)
        {
            this._baseStream = baseStream;
            if (preprocess)
                Process();
        }

        public bool isProcessed
        {
            get;
            private set;
        }

        #region Stream implementation

        public override bool CanRead
        {
            get { return isProcessed || Process(); }
        }

        public override bool CanSeek
        {
            get { return isProcessed || Process(); }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new InvalidOperationException();
        }

        public override long Length
        {
            get { 
                if (!isProcessed && !Process()) {
                    throw new InvalidOperationException();
                }
                return finalLength;
            }
        }


        public override long Position
        {
            get
            {
                return innerPosition;
            }
            set
            {
                innerPosition = (int)value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (innerPosition + offset + count > finalLength)
                throw new InvalidOperationException();
            Buffer.BlockCopy(this.buffer, innerPosition + offset, buffer, 0, count);
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            { 
                case SeekOrigin.Begin:
                    innerPosition = (int) offset;
                    break;
                case SeekOrigin.Current:
                    innerPosition += (int)offset;
                    break;
                case SeekOrigin.End:
                    innerPosition = finalLength - 1 + (int) offset;
                    break;
            }
            return innerPosition;
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        #endregion
    }
}
