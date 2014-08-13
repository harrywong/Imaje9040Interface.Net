using System;
using System.IO.Ports;
using System.Threading;

namespace ImajeLib
{
    public class PrinterControl : IDisposable
    {
        public const byte ACK = 0x06;
        public const byte NACK = 0x15;
        public const int SecondsToWait = 10000; // 10s


        private readonly SerialPort _port;
        private readonly AutoResetEvent _ar;
        private object _lastResponse;
        private bool _disposed;

        public PrinterControl(SerialPort port)
        {
            this._port = port;
            this._port.Open();
            this._port.DataReceived += OnPortDataReceived;
            this._ar = new AutoResetEvent(false);
        }

        ~PrinterControl()
        {
            this.Dispose(false);
        }

        public void V24Request()
        {
            var response = this.InternalSendV24Request();
            this.CheckAck(response);
        }

        public void StartUp()
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.Power;
            request.Data = new byte[] { 0xFF };

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
        }

        public void LongShutDown()
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.Power;
            request.Data = new byte[] { 0x00 };

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
        }

        public void ShortShutDown()
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.Power;
            request.Data = new byte[] { 0x01 };

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
        }

        public void ResetFaults()
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.ResetFaults;

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
        }

        public void SendMessage(Message message)
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.TransmitNonLibraryMessage;
            request.Data = message.GetBytes();

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
        }

        public void SendPartialMessage(PartialMessage message)
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.TransmitPartialMessage;
            request.Data = message.GetBytes();

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
        }

        public void TransmitMessageToPrint(byte headNumber, byte messageNumber)
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.TransmitMessageNumber;
            request.Data = new byte[] { headNumber, 0x00, messageNumber };

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
        }

        public InkInfo RequestInkInfo()
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.RequestInkCircuitSolenoidValve;

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
            return new InkInfo(response.Data);
        }

        public PrinterFaults RequestPrinterFaults()
        {
            var request = new EnqRequest();
            request.Ident = Identifiers.RequestPrinterFaults;

            var response = this.InternalSendRequest(request);
            this.CheckAck(response);
            return new PrinterFaults(response.Data);
        }

        private AckResponse InternalSendV24Request()
        {
            this._port.Write(new byte[] { 0x05 }, 0, 1);

            Thread.VolatileWrite(ref this._lastResponse, null);
            this._ar.WaitOne(SecondsToWait);

            if (this._lastResponse == null)
            {
                throw new Exception("Cannot connect to port!");
            }

            return (AckResponse)this._lastResponse;
        }

        private AckResponse InternalSendRequest(EnqRequest request)
        {
            var bytes = request.GetBytes();
            this._port.Write(bytes, 0, bytes.Length);

            Thread.VolatileWrite(ref this._lastResponse, null);
            this._ar.WaitOne(SecondsToWait);

            if (this._lastResponse == null)
            {
                throw new Exception("Cannot connect to port!");
            }

            return (AckResponse)this._lastResponse;
        }

        private void CheckAck(AckResponse response, string callName = null)
        {
            if (response.Ack == ACK)
            {
                return;
            }
            if (response.Ack == NACK)
            {
                throw new NackException();
            }
            throw new UnknownAckException();
        }

        private void OnPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var bytes = new byte[this._port.BytesToRead];
            this._port.Read(bytes, 0, bytes.Length);
            this._lastResponse = new AckResponse(bytes);
            this._ar.Set();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }
            if (disposing)
            {
                if (this._port != null)
                {
                    //this._port.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}