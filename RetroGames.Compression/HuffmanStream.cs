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
        protected override bool Process()
        {
            throw new NotImplementedException();
        }

        public HuffmanStream(Stream baseStream) : base(baseStream) { }
        public HuffmanStream(Stream baseStream, bool preprocess) : base(baseStream, preprocess) { }
    }
}
