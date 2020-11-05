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

        public NormaXmlObject(string innerXml) : this()
        {
            xml.InnerXml = innerXml;
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

        protected Dictionary<string, string> GetNodesInnerXmlFromContainer(string containerTag, string nodeTagName)
        {
            XmlNodeList v;
            XmlNode containerNode = xml.GetElementsByTagName(containerTag)[0];
            XmlElement el;
            Dictionary<string, string> d = new Dictionary<string, string>();
            if (containerNode != null)
            {
                el = containerNode as XmlElement;
                v = el.GetElementsByTagName(nodeTagName);
                for (int i = 0; i < v.Count; i++)
                {
                    XmlElement e = v[i] as XmlElement;
                    string id = (e.HasAttribute("id")) ? e.GetAttribute("id") : i.ToString();
                    d.Add(id, e.InnerXml);
                }
            }
            return d;
        }

        protected void AddElementToContainer(string containerTag, string elTagName, string innerXml, string id = null)
        {
            XmlNode parentNode = xml.GetElementsByTagName(containerTag)[0];
            XmlElement parentEl = getOrCreateElement(containerTag);
            XmlElement childEl = xml.CreateElement(elTagName);
            childEl.InnerXml = innerXml;
            childEl.SetAttribute("id", id);
            parentEl.AppendChild(childEl);
        }

        protected XmlElement getOrCreateElement(string tagName)
        {
            XmlNode node = xml.GetElementsByTagName(tagName)[0];
            XmlElement el = (node == null) ? xml.CreateElement(tagName) : node as XmlElement;
            if (node == null) xml.AppendChild(el);
            return el;
        }

        protected bool tryGetIntXmlProp(string key, out int defVal)
        {
            return int.TryParse(getXmlProp(key), out defVal);
        }

        protected bool hasElement(string key)
        {
            return xml.GetElementsByTagName(key).Count > 0;
        }
    }
}
