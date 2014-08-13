using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImajeLib
{
    public class PrinterFaults
    {
        private bool _head2Faulty;
        private bool _head1Faulty;
        private bool _memoryLost;
        private bool _cpuHardware;
        private bool _pressure;
        private bool _inkLevelLow;

        private bool _additive;
        private bool _fan;
        private bool _visco;
        private bool _temperature;

        public PrinterFaults(byte[] data)
        {
            this.LoadData(data);
        }

        private void LoadData(byte[] data)
        {
            byte generalFaultByte = data[0];

            this._inkLevelLow = (generalFaultByte & 1) == 1;
            generalFaultByte >>= 1;
            this._pressure = (generalFaultByte & 1) == 1;
            generalFaultByte >>= 1;
            this._cpuHardware = (generalFaultByte & 1) == 1;
            generalFaultByte >>= 1;
            this._memoryLost = (generalFaultByte & 1) == 1;
            generalFaultByte >>= 1;
            this._head1Faulty = (generalFaultByte & 1) == 1;
            generalFaultByte >>= 1;
            this._head2Faulty = (generalFaultByte & 1) == 1;

            byte printerFaultByte = data[2];

            printerFaultByte >>= 4;
            this._temperature = (printerFaultByte & 1) == 1;
            printerFaultByte >>= 1;
            this._visco = (printerFaultByte & 1) == 1;
            printerFaultByte >>= 1;
            this._fan = (printerFaultByte & 1) == 1;
            printerFaultByte >>= 1;
            this._additive = (printerFaultByte & 1) == 1;
        }

        public bool Head2Faulty
        {
            get { return _head2Faulty; }
            set { _head2Faulty = value; }
        }

        public bool Head1Faulty
        {
            get { return _head1Faulty; }
            set { _head1Faulty = value; }
        }

        public bool MemoryLost
        {
            get { return _memoryLost; }
            set { _memoryLost = value; }
        }

        public bool CpuHardware
        {
            get { return _cpuHardware; }
            set { _cpuHardware = value; }
        }

        public bool Pressure
        {
            get { return _pressure; }
            set { _pressure = value; }
        }

        public bool InkLevelLow
        {
            get { return _inkLevelLow; }
            set { _inkLevelLow = value; }
        }
    }
}
