using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroGames.Compression
{
    class CarmackStream : CompressedStream
    {
        override protected bool Process() {
			if (!BaseStream.CanRead) { return false; }
			BinaryReader br = new BinaryReader (BaseStream);
			SetLength (br.ReadUInt16());

			int byteCount;
			UInt16 instruction;
			int srcPtr;
			long currentPosition;
			byte t;

			for (int ptr = 0; ptr < Length || BaseStream.CanRead; ) {
				byteCount = br.ReadUInt16 () * 2;
				instruction = br.ReadUInt16 ();

				if (instruction == 0xA7) {
					// Near Ptr;
					currentPosition = BaseStream.Position;
					srcPtr = br.ReadUInt16 () * -2;
					BaseStream.Seek (srcPtr, SeekOrigin.Current);

					for (int i = 0; i < byteCount; i++, ptr++) {
						t = br.ReadByte (); // Read the byte

						// If previous byte is 0 and current byte is A7 or A8
						if (i > 0 && ptr > 0 && buffer [ptr - 1] == 0 && (t == 0xA7 || t == 0xA8 )) {
							ptr--; // Erase the 0 we just copied
						}
						buffer [ptr] = t;
					}

					BaseStream.Seek (currentPosition, SeekOrigin.Begin);
				} else if (instruction == 0xA8) {
					// Far Ptr;
					srcPtr = (int) br.ReadUInt32 () * 2;
					Buffer.BlockCopy (buffer, srcPtr, buffer, ptr, byteCount);
				} else {
					throw new InvalidDataException ();
				}

				ptr += byteCount;
			}
			return isProcessed = true;
        }

        public CarmackStream(Stream baseStream) : base(baseStream)
        {
        }

        public CarmackStream(Stream baseStream, bool preprocess) : base(baseStream, preprocess)
        {
        }

    }
}
