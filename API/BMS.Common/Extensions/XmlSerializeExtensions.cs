using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BMS.Common.Extensions
{
    public static class XmlSerializeExtensions
    {
        public static string ToXml<T>(this T obj)
        {
            var xml = string.Empty;
            if (obj != null)
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xsSubmit.Serialize(writer, obj);
                        xml = sww.ToString();
                    }
                }
            }
            return xml;
        }
    }
}
