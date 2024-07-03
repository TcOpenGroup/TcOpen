namespace TcoCognexVision.Converters
{
    public class SpecialAsciiToSignConverters
    {
        public static string SpecialAsciiToSign(byte value)
        {
            switch ((byte)value)
            {
                case 0:
                {
                    return "NUL";
                    break;
                }
                case 1:
                {
                    return "SOH";
                    break;
                }
                case 2:
                {
                    return "STX";
                    break;
                }
                case 3:
                {
                    return "ETX";
                    break;
                }
                case 4:
                {
                    return "EOT";
                    break;
                }
                case 5:
                {
                    return "ENQ";
                    break;
                }
                case 6:
                {
                    return "ACK";
                    break;
                }
                case 7:
                {
                    return "BEL";
                    break;
                }
                case 8:
                {
                    return "BS";
                    break;
                }
                case 9:
                {
                    return "HT";
                    break;
                }
                case 10:
                {
                    return "LF";
                    break;
                }
                case 11:
                {
                    return "VT";
                    break;
                }
                case 12:
                {
                    return "FF";
                    break;
                }
                case 13:
                {
                    return "CR";
                    break;
                }
                case 14:
                {
                    return "SO";
                    break;
                }
                case 15:
                {
                    return "SI";
                    break;
                }
                case 16:
                {
                    return "DLE";
                    break;
                }
                case 17:
                {
                    return "DC1";
                    break;
                }
                case 18:
                {
                    return "DC2";
                    break;
                }
                case 19:
                {
                    return "DC3";
                    break;
                }
                case 20:
                {
                    return "DC4";
                    break;
                }
                case 21:
                {
                    return "NAK";
                    break;
                }
                case 22:
                {
                    return "SYN";
                    break;
                }
                case 23:
                {
                    return "ETB";
                    break;
                }
                case 24:
                {
                    return "CAN";
                    break;
                }
                case 25:
                {
                    return "EM ";
                    break;
                }
                case 26:
                {
                    return "SUB";
                    break;
                }
                case 27:
                {
                    return "ESC";
                    break;
                }
                case 28:
                {
                    return "FS";
                    break;
                }
                case 29:
                {
                    return "GS";
                    break;
                }
                case 30:
                {
                    return "RS";
                    break;
                }
                case 31:
                {
                    return "US";
                    break;
                }

                default:
                    return "N/A";
            }
        }
    }
}
