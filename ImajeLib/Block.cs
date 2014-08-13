using System;

namespace ImajeLib
{
    public class Block
    {
        // block
        private byte[] _positiions;
        private byte _font;
        private byte _expansion;
        private byte _identifier;

        //data
        private string _text;

        public Block()
        {
            this._positiions = new byte[] { 0x80, 0x01 };
            this._font = 56;
            this._expansion = 1;
            this._identifier = 0x10;
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[10 + this._text.Length];
            byte[] others = new byte[5];
            Array.Copy(this._positiions, 0, others, 0, 2);
            others[2] = this._font;
            others[3] = this._expansion;
            others[4] = this._identifier;

            others.CopyTo(bytes, 0);

            byte[] ascii = System.Text.Encoding.ASCII.GetBytes(this._text);
            ascii.CopyTo(bytes, 5);

            Array.Reverse(others);
            others.CopyTo(bytes, 5 + this._text.Length);

            return bytes;
        }

        #region Properties

        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        public byte[] Positiions
        {
            get { return _positiions; }
            set { _positiions = value; }
        }

        public byte Font
        {
            get { return _font; }
            set { _font = value; }
        }

        public byte Expansion
        {
            get { return _expansion; }
            set { _expansion = value; }
        }

        public byte Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        #endregion
    }
}