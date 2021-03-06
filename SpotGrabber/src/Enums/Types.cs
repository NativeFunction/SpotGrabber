
namespace SpotGrabber
{
    public enum ControlType
    {
        None,
        Line,
        ScaleCornerTopLeft,
        ScaleCornerTopRight,
        ScaleCornerBottomRight,
        ScaleCornerBottomLeft,
        ScaleMidLeft,
        ScaleMidRight,
        ScaleMidUp,
        ScaleMidDown,
        Rotate,
        Position
    }

    public enum CameraManufacturer
    {
        Axis,
        ChannelVision,
        Hi3516,
        Megapixel,
        AxisMkII,
        PanasonicHD,
        None
    }

    public enum CameraQuality
    {
        Low,
        Medium,
        High,
        None
    }

    public enum LotSize
    {
        Small,
        Medium,
        Large,
        None
    }
}
