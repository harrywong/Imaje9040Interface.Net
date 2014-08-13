using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImajeLib
{
    public class PartialMessage
    {
        private byte _headNumber;
        private IList<Zone> _zones;

        public PartialMessage()
        {
            this._headNumber = 1;
            this._zones = new List<Zone>();
        }

        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                stream.WriteByte(this._headNumber);
                stream.WriteByte((byte)this._zones.Count);
                foreach (var zone in this._zones)
                {
                    var buffer = zone.GetBytes();
                    stream.Write(buffer, 0, buffer.Length);
                }

                return stream.ToArray();
            }
        }

        public IList<Zone> Zones
        {
            get { return this._zones; }
            set { this._zones = value; }
        }
    }
}
