using System;
using System.Collections.Generic;
using System.IO;

namespace ImajeLib
{
    public class Message
    {
        private byte _headNumber;
        private byte _messageNumber;
        private string _messageTitle;

        // Structure Indicator
        private bool _generalParametersPresent;
        private bool _messageTestPresent;
        private byte _numberOfCounters;
        private byte _numberOfPostdates;
        private bool _presentTimeCodes;
        private byte _numberOfBarcodes;

        // General Message
        private MessageDirection _messageDirection;
        private HorizontalDirection _horizontalDirection;
        private VerticalDirection _verticalDirection;
        private TachoMode _tachoMode;
        private ManualTrigger _manualTrigger;
        private TriggerMode _triggerMode;
        private UnitForMargins _unitForMargins;
        private DINMode _dinMode;

        private byte _mulitTopValue; //1-255
        private byte _objectTopFilter; //1-10
        private byte _tachoDivision; //1-127
        private ushort _forwardMargin; //3-9000mm
        private ushort _returnMargin; //3-9000mm
        private ushort _interval; //3-9000mm
        private ushort _printingSpeed; //1-9999mm/s
        private ushort _algorithmNumber;

        // Lines
        private IList<Line> _lines;

        public Message()
        {
            this._headNumber = 1;
            this._messageNumber = 1;
            this._messageTitle = DateTime.Now.Ticks.ToString().Substring(0, 8);

            this._generalParametersPresent = true;
            this._messageTestPresent = true;
            this._numberOfCounters = 0;
            this._numberOfPostdates = 0;
            this._presentTimeCodes = false;
            this._numberOfBarcodes = 0;

            this._messageDirection = MessageDirection.Normal;
            this._horizontalDirection = HorizontalDirection.Normal;
            this._verticalDirection = VerticalDirection.Normal;
            this._tachoMode = TachoMode.No;
            this._manualTrigger = ManualTrigger.Yes;
            this._triggerMode = TriggerMode.Object;
            this._unitForMargins = UnitForMargins.Millimeters;
            this._dinMode = DINMode.No;

            this._mulitTopValue = 0;
            this._objectTopFilter = 1;
            this._tachoDivision = 1;
            this._forwardMargin = 3;
            this._returnMargin = 3;
            this._interval = 3;
            this._printingSpeed = 100;
            this._algorithmNumber = 0;

            this.Lines = new List<Line>();
        }

        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                stream.WriteByte(this._headNumber);
                //stream.WriteByte(this._messageNumber);
                //byte[] titleBytes = Encoding.ASCII.GetBytes(this._messageTitle);
                //stream.Write(titleBytes, 0, 8);

                byte structureByte1 = 0;
                byte structureByte2 = 0;


                structureByte1 |= (byte)((this._generalParametersPresent == true) ? 1 : 0);
                structureByte1 <<= 1;
                structureByte1 |= (byte)((this._messageTestPresent == true) ? 1 : 0);
                structureByte1 <<= 2;
                structureByte1 <<= 2;
                structureByte1 <<= 2;

                stream.WriteByte(structureByte1);

                structureByte2 |= 1;
                structureByte2 <<= 1;
                structureByte2 |= (byte)((this._presentTimeCodes == true) ? 1 : 0);
                structureByte2 <<= 1;
                structureByte2 <<= 3;
                structureByte2 |= this._numberOfBarcodes;

                stream.WriteByte(structureByte2);

                byte messageParameterByte = 0;
                messageParameterByte |= (byte)this._messageDirection;
                messageParameterByte <<= 1;
                messageParameterByte |= (byte)this._horizontalDirection;
                messageParameterByte <<= 1;
                messageParameterByte |= (byte)this._verticalDirection;
                messageParameterByte <<= 1;
                messageParameterByte |= (byte)this._tachoMode;
                messageParameterByte <<= 1;
                messageParameterByte |= (byte)this._manualTrigger;
                messageParameterByte <<= 1;
                messageParameterByte |= (byte)this._triggerMode;
                messageParameterByte <<= 1;
                messageParameterByte |= (byte)this._unitForMargins;
                messageParameterByte <<= 1;
                messageParameterByte |= (byte)this._dinMode;

                stream.WriteByte(messageParameterByte);

                //stream.WriteByte(0xC0);
                //stream.WriteByte(0x20);
                //stream.WriteByte(0x10);

                stream.WriteByte(this._mulitTopValue);
                stream.WriteByte(this._objectTopFilter);
                stream.WriteByte(this._tachoDivision);

                byte[] buffer;
                buffer = BitConverter.GetBytes(this._forwardMargin);
                Array.Reverse(buffer);
                stream.Write(buffer, 0, 2);

                buffer = BitConverter.GetBytes(this._returnMargin);
                Array.Reverse(buffer);
                stream.Write(buffer, 0, 2);

                buffer = BitConverter.GetBytes(this._interval);
                Array.Reverse(buffer);
                stream.Write(buffer, 0, 2);

                buffer = BitConverter.GetBytes(this._printingSpeed);
                Array.Reverse(buffer);
                stream.Write(buffer, 0, 2);

                buffer = BitConverter.GetBytes(this._algorithmNumber);
                Array.Reverse(buffer);
                stream.Write(buffer, 0, 2);

                foreach (var line in this._lines)
                {
                    var lineBytes = line.GetBytes();
                    stream.Write(lineBytes, 0, lineBytes.Length);
                }

                stream.WriteByte(0x0D);

                return stream.ToArray();
            }
        }

        #region Properties

        public byte HeadNumber
        {
            get { return _headNumber; }
            set { _headNumber = value; }
        }

        public byte MessageNumber
        {
            get { return _messageNumber; }
            set { _messageNumber = value; }
        }

        public string MessageTitle
        {
            get { return _messageTitle; }
            set { _messageTitle = value; }
        }

        public bool GeneralParametersPresent
        {
            get { return _generalParametersPresent; }
            set { _generalParametersPresent = value; }
        }

        public bool MessageTestPresent
        {
            get { return _messageTestPresent; }
            set { _messageTestPresent = value; }
        }

        public byte NumberOfCounters
        {
            get { return _numberOfCounters; }
            set { _numberOfCounters = value; }
        }

        public byte NumberOfPostdates
        {
            get { return _numberOfPostdates; }
            set { _numberOfPostdates = value; }
        }

        public bool PresentTimeCodes
        {
            get { return _presentTimeCodes; }
            set { _presentTimeCodes = value; }
        }

        public byte NumberOfBarcodes
        {
            get { return _numberOfBarcodes; }
            set { _numberOfBarcodes = value; }
        }

        public MessageDirection MessageDirection
        {
            get { return _messageDirection; }
            set { _messageDirection = value; }
        }

        public HorizontalDirection HorizontalDirection
        {
            get { return _horizontalDirection; }
            set { _horizontalDirection = value; }
        }

        public VerticalDirection VerticalDirection
        {
            get { return _verticalDirection; }
            set { _verticalDirection = value; }
        }

        public TachoMode TachoMode
        {
            get { return _tachoMode; }
            set { _tachoMode = value; }
        }

        public ManualTrigger ManualTrigger
        {
            get { return _manualTrigger; }
            set { _manualTrigger = value; }
        }

        public TriggerMode TriggerMode
        {
            get { return _triggerMode; }
            set { _triggerMode = value; }
        }

        public UnitForMargins UnitForMargins
        {
            get { return _unitForMargins; }
            set { _unitForMargins = value; }
        }

        public DINMode DinMode
        {
            get { return _dinMode; }
            set { _dinMode = value; }
        }

        public byte MulitTopValue
        {
            get { return _mulitTopValue; }
            set { _mulitTopValue = value; }
        }

        public byte ObjectTopFilter
        {
            get { return _objectTopFilter; }
            set { _objectTopFilter = value; }
        }

        public byte TachoDivision
        {
            get { return _tachoDivision; }
            set { _tachoDivision = value; }
        }

        public ushort ForwardMargin
        {
            get { return _forwardMargin; }
            set { _forwardMargin = value; }
        }

        public ushort ReturnMargin
        {
            get { return _returnMargin; }
            set { _returnMargin = value; }
        }

        public ushort Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public ushort PrintingSpeed
        {
            get { return _printingSpeed; }
            set { _printingSpeed = value; }
        }

        public ushort AlgorithmNumber
        {
            get { return _algorithmNumber; }
            set { _algorithmNumber = value; }
        }

        public IList<Line> Lines
        {
            get { return this._lines; }
            set { this._lines = value; }
        }

        #endregion
    }
}