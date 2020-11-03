using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NormaMeasure.Utils
{
    public class NormaXmlObject
    {
        private XmlDocument xml;
        public bool WasChanged;
        public string InnerXml => xml.InnerXml;
        public NormaXmlObject()
        {
            xml = new XmlDocument();
        }

        protected void setXmlProp(string key, string value)
        {
            retry:
            try
            {
                WasChanged = xml.GetElementsByTagName(key)[0].InnerXml != value;
                xml.GetElementsByTagName(key)[0].InnerXml = value;
            }catch(NullReferenceException)
            {
                xml.PrependChild(xml.CreateElement(key));
                goto retry;
            }
        }

        protected string getXmlProp(string key)
        {
            string v;
            try
            {
                v = xml.GetElementsByTagName(key)[0].InnerXml;
            }
            catch (NullReferenceException)
            {
                v = "";
            }
            return v;
        }


    }
}
