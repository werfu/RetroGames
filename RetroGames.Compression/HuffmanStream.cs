using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroGames.Compression
{
    class HuffmanStream : CompressedStream
    {
		private struct HuffmanNode
		{
			public UInt16 left;
			public UInt16 right;
		}

		private HuffmanNode[] nodes;

        protected override bool Process()
        {
			if (!BaseStream.CanRead) { return false; }

			BinaryReader br = new BinaryReader (BaseStream);

			// Verify signature
			string signature = br.ReadChars (4).ToString ();
			if (signature != "HUFF") { throw new InvalidDataException (); }

			// Get length
			SetLength ( br.ReadUInt16 () );

			nodes = new HuffmanNode[255];
			for (int i =0; i < 255; i++) {
				nodes [i].left = br.ReadUInt16 ();
				nodes [i].right = br.ReadUInt16 ();
			}

			int currentNode = 254, nextnode;
			byte bin, bmask;

			for(int ptr = 0; ptr < Length;) {
				bin = br.ReadByte ();
				for (bmask = 0; bmask < 8 && ptr < Length; bmask++) {
					if ( bin > 0 && (2 ^ bmask) > 0)
						{ nextnode = nodes [currentNode].right; }
					else
						{ nextnode = nodes [currentNode].left; }

					if (nextnode < 256) {
						buffer [ptr] = (byte) (nextnode & 0xFF);
						ptr++;
						currentNode = 254;
					} else {
						currentNode = nextnode & 0xFF;
					}
				}
			}
			return isProcessed = true;
        }

        public HuffmanStream(Stream baseStream) : base(baseStream) { }
        public HuffmanStream(Stream baseStream, bool preprocess) : base(baseStream, preprocess) { }
    }
}
