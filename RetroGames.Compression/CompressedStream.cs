using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroGames.Compression
{
    public abstract class CompressedStream : Stream
    {
        #region privates
        private int _length;
        private int innerPosition;
        #endregion

        protected Byte[] buffer { get; set; }
        protected abstract bool Process();

        public CompressedStream(Stream baseStream)
        {
            this.BaseStream = baseStream;
        }

        public CompressedStream(Stream baseStream, bool preprocess)
        {
            this.BaseStream = baseStream;
            if (preprocess)
                Process();
        }

        public bool isProcessed { get; protected set; }
        public Stream BaseStream { get; protected set; }

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
                return _length;
            }

            private set { 
                _length = (int) value;
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
            if (innerPosition + offset + count > Length)
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
                    innerPosition = (int) Length - 1 + (int) offset;
                    break;
            }
            return innerPosition;
        }

        public override void SetLength(long value)
        {
            Length = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        #endregion


    }
}
