using System;

namespace ImajeLib
{
    public class AckResponse
    {
        private readonly byte _ack;
        private readonly byte _ident;
        private readonly ushort _length;
        private readonly byte[] _data;
        private readonly byte _checkSum;
        private bool _valid;
        public const byte NACK = 0x15;
        public AckResponse(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException("bytes");
            }
            try
            {
                this._ack = bytes[0];
                if (bytes.Length > 1)
                {
                    this._ident = bytes[1];
                    byte[] lengthBytes = new byte[2];
                    Array.Copy(bytes, 2, lengthBytes, 0, 2);
                    Array.Reverse(lengthBytes);
                    this._length = BitConverter.ToUInt16(lengthBytes, 0);
                    this._data = new byte[this._length];
                    Array.Copy(bytes, 4, this._data, 0, this._data.Length);
                    this._checkSum = bytes[bytes.Length - 1];
                    this._valid = (this._checkSum == this.CalcCheckSum());
                }
            }
            catch
            {
                this._ack = NACK;
            }
        }

        private byte CalcCheckSum()
        {
            byte checkSum;
            byte[] length = BitConverter.GetBytes(this._length);
            checkSum = this._ident;
            checkSum ^= length[0];
            checkSum ^= length[1];
            for (int i = 0; i < this._data.Length; i++)
            {
                checkSum ^= this._data[i];
            }
            return checkSum;
        }

        public byte Ack
        {
            get { return _ack; }
        }

        public byte Ident
        {
            get { return _ident; }
        }

        public ushort Length
        {
            get { return _length; }
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public byte CheckSum
        {
            get { return _checkSum; }
        }

        public bool Valid
        {
            get { return _valid; }
            set { _valid = value; }
        }
    }
}