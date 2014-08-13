using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImajeLib
{
    public enum MessageDirection : byte
    {
        Normal = 0,
        Reverse = 1
    }

    public enum HorizontalDirection : byte
    {

        Normal = 0,
        Reverse = 1
    }

    public enum VerticalDirection : byte
    {

        Normal = 0,
        Reverse = 1
    }

    public enum TachoMode : byte
    {
        No = 0,
        Yes = 1
    }

    public enum ManualTrigger : byte
    {
        No = 0,
        Yes = 1
    }

    public enum TriggerMode : byte
    {
        Object = 0,
        Repetitivef = 1
    }

    public enum UnitForMargins : byte
    {
        Millimeters = 0,
        FrameHt = 1
    }

    public enum DINMode : byte
    {
        No = 0,
        Yes = 1
    }
}
