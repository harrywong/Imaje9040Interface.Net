using System.Collections.Generic;
using System.IO;

namespace ImajeLib
{
    public class Line
    {
        private IList<Block> _blocks;

        public Line()
        {
            this._blocks = new List<Block>();
        }

        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                stream.WriteByte(0x0A);
                foreach (var block in this._blocks)
                {
                    var blockBytes = block.GetBytes();
                    stream.Write(blockBytes, 0, blockBytes.Length);
                }
                return stream.ToArray();
            }
        }

        public IList<Block> Blocks
        {
            get { return this._blocks; }
            set { this._blocks = value; }
        }
    }
}