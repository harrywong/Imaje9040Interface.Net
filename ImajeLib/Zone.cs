using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImajeLib
{
    public class Zone
    {
        private byte _lineNumber;
        private ushort _position;
        private ushort _editingNumber;
        private string _symbols;

        public Zone()
        {
            this._lineNumber = 0;
            this._position = 0;
        }

        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                stream.WriteByte(_lineNumber);
                byte[] buffer;
                buffer = BitConverter.GetBytes(this._position);
                Array.Reverse(buffer);
                stream.Write(buffer, 0, 2);
                buffer = BitConverter.GetBytes(this._editingNumber);
                Array.Reverse(buffer);
                stream.Write(buffer, 0, 2);

                buffer = Encoding.ASCII.GetBytes(this._symbols);
                stream.Write(buffer, 0, buffer.Length);

                return stream.ToArray();
            }
        }

        public string Symbols
        {
            get { return this._symbols; }
            set
            {
                this._symbols = value;
                this._editingNumber = (ushort)this._symbols.Length;
            }
        }

        public byte LineNumber
        {
            get { return this._lineNumber; }
            set { this._lineNumber = value; }
        }

        public ushort Position
        {
            get { return this._position; }
            set { this._position = value; }
        }
    }
}
