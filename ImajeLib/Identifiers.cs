namespace ImajeLib
{
    public static class Identifiers
    {
        // Printer
        public const byte Power = 0x30;
        public const byte ResetFaults = 0x3C;
        public const byte TransmitKeyboardCode = 0x3E;
        public const byte TransmitSecurityCode = 0x3F;
        public const byte PermitMenuModification = 0x0F;
        public const byte TransmitPrintAcknowledgementRequest = 0x41;
        public const byte TransmitPrinterInitialization = 0x36;
        public const byte UploadFiles = 0x50;

        // Head
        public const byte TransmitJetStatus = 0x31;

        public const byte RequestJetStatus = 0x32;
        public const byte RequestJetSpeedAndPhase = 0x33;

        // Message
        public const byte TransmitMessageNumber = 0x5A;
        public const byte TransmitExternalVariables = 0x5B;
        public const byte TransmitNonLibraryMessage = 0x57;
        public const byte TransmitLibrarayMessage = 0x58;
        public const byte TransmitPartialMessage = 0x59;

        // Variables
        public const byte TransmitCurrentCounterValue = 0x51;
        public const byte ResetCounters = 0x3A;
        public const byte TransmitTablesOfMonthAndTimeCodes = 0x53;
        public const byte InitializeAutodating = 0xC8;
        public const byte TransmitAutodaingTable = 0xDF;

        public const byte RequestCurrentCounters = 0x39;
        public const byte RequestPppPrintingCounter = 0x56;
        public const byte RequestAutodating = 0xD6;
        public const byte RequestForAnAutodaingTable = 0xDE;
        public const byte RequestTablesOfMonthsAndTimeCodes = 0x52;

        // Printer
        public const byte V24DialogRequest = 0x05;
        public const byte RequestKeyboardCode = 0x45;
        public const byte RequestPrinterParameters = 0x20;
        public const byte RequestInkCircuitSolenoidValve = 0x35;
        public const byte RequestCrcs = 0x37;
        public const byte RequestPrinterFaults = 0x3B;
        public const byte RequestStatusForContrastPrinter = 0x4D;
    }
}