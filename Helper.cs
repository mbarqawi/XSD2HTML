using System.Collections;
using System.Text;

namespace XSD2HTML
{
    internal class Helper
    {
        public static bool IsEmpty(string str)
        {
            return str == null || str.Length == 0;
        }

        public static bool IsEmpty(ArrayList list)
        {
            return list == null || list.Count == 0;
        }

        public static string FormatErrorMessage(string msgId, params object[] args)
        {
            //return string.Format(StringResources.GetString(msgId), args);
            return msgId;
        }

        public static void WriteStartElement(StringBuilder sb, string name)
        {
            sb.Append(string.Format("<{0}>", name));
        }

        public static void WriteEndElement(StringBuilder sb, string name)
        {
            sb.Append(string.Format("</{0}>", name));
        }

        //Start DCR #73672
        // MX messages reuire tagging of namespace prefix with every node.
        /*
		 * This method checks if the message is of type MT or MX.
         * If it is of type MX, it prepends the namsepace prefix "ns0:"" with every node that is passed to it.
         * ns0 prefix corresponds to the Document schema namespace in the Infopath form template files.
         * If XMLTag is '.', then no need to prepend the ns0 prefix with the same for MX messages, else add ns0 prefix.
		 *
		 * @xmlTag - The name of the node for which the prefix tagging is to be done.
		 *
		 * @return XmlTag- For MX messages, "ns0:"+Xmltag is returned else Xmltag is returned as it is.
		 */
        //public static string FormatCode(string xmlTag)
        //{
        //    if ((!xmlTag.Equals(".")) && FormGenerator.IncomingMsgType.Equals(Message.MXMessage))
        //        return "ns0:" + xmlTag;

        //    return xmlTag;
        //}
        //End DCR #73672
    }
}