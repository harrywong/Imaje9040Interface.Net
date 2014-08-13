using System;

namespace ImajeLib
{
    public class EnqRequest
    {
        private byte _ident;
        private ushort _length;
        private byte[] _data;

        public EnqRequest()
        {
            this._data = new byte[0];
        }

        private byte CalcCheckSum()
        {
            byte checkSum;
            byte[] length = this.GetLengthBytes();
            checkSum = this._ident;
            checkSum ^= length[0];
            checkSum ^= length[1];
            for (int i = 0; i < this._data.Length; i++)
            {
                checkSum ^= this._data[i];
            }
            return checkSum;
        }

        public byte Ident
        {
            get { return _ident; }
            set { _ident = value; }
        }

        public ushort Length
        {
            get { return this._length; }
        }

        public byte[] Data
        {
            get { return _data; }
            set
            {
                _data = value;
                this._length = (ushort) this._data.Length;
            }
        }

        public byte CheckSum
        {
            get { return this.CalcCheckSum(); }
        }

        public byte[] GetBytes()
        {
            int length = 4 + this._data.Length;
            byte[] enq = new byte[length];
            enq[0] = this._ident;
            byte[] lengthBytes = this.GetLengthBytes();
            lengthBytes.CopyTo(enq, 1);
            this._data.CopyTo(enq, 3);
            enq[length - 1] = this.CalcCheckSum();

            //Array.Reverse(enq);

            return enq;
        }

        public byte[] GetLengthBytes()
        {
            byte[] length = BitConverter.GetBytes(this._length);
            Array.Reverse(length);
            return length;
        }
    }
}