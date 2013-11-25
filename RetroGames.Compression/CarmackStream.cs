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
            return false;
        }

        public CarmackStream(Stream baseStream) : base(baseStream)
        {
        }

        public CarmackStream(Stream baseStream, bool preprocess) : base(baseStream, preprocess)
        {
        }

    }
}
