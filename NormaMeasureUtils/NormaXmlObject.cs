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
        private XmlElement documentElement;
        public string docElementTagName;
        public bool WasChanged;
        public string InnerXml => xml.InnerXml;
        private bool isValid;
        public bool IsValid => isValid;
        private string docElTagName;
        public NormaXmlObject()
        {
            docElTagName = this.GetType().Name;
            xml = new XmlDocument();
            documentElement = xml.CreateElement(docElTagName);
            xml.AppendChild(documentElement);
            isValid = true;
        }

        public NormaXmlObject(string innerXml) : this()
        {
            xml.InnerXml = innerXml;
            isValid = hasElement(docElTagName);
        }

        protected void setXmlProp(string key, string value)
        {
            retry:
            try
            {
                XmlElement newEl = xml.CreateElement(key);
                newEl.InnerXml = value;
                
                if (documentElement.GetElementsByTagName(key).Count > 0)
                {
                    XmlElement lastEl = documentElement.GetElementsByTagName(key)[0] as XmlElement;
                    documentElement.ReplaceChild(lastEl, newEl);
                }else
                {
                    documentElement.AppendChild(newEl);
                }
               // WasChanged = documentElement.GetElementsByTagName(key)[0].InnerXml != value;
               // XmlElement el = documentElement.GetElementsByTagName(key)[0] as XmlElement;
                
               // el.InnerXml = value;
               // documentElement.ReplaceChild(documentElement.GetElementsByTagName(key)[0], el);
                
            }catch(NullReferenceException)
            {
                documentElement.AppendChild(xml.CreateElement(key));
                goto retry;
            }
        }

        protected string getXmlProp(string key)
        {
            string v;
            try
            {
                v = documentElement.GetElementsByTagName(key)[0].Value;
            }
            catch (NullReferenceException)
            {
                v = "";
            }finally
            {
                v = "";
            }
            return v;
        }

        protected Dictionary<string, string> GetNodesInnerXmlFromContainer(string containerTag, string nodeTagName)
        {
            XmlNodeList v;
            XmlNode containerNode = documentElement.GetElementsByTagName(containerTag)[0];
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
            XmlElement parentEl = getOrCreateElement(containerTag);
            XmlElement childEl = xml.CreateElement(elTagName);
            childEl.InnerXml = innerXml;
            childEl.SetAttribute("id", id);
            parentEl.AppendChild(childEl);
        }

        protected void ReplaceElementOnContainer(string containerTag, string elTagName, string innerXml, string id)
        {
            XmlNode parentNode = documentElement.GetElementsByTagName(containerTag)[0];
            XmlElement parentEl = getOrCreateElement(containerTag);
            XmlElement el = null;
            foreach(XmlElement e in parentEl.GetElementsByTagName(elTagName))
            {
                if (e.GetAttribute("id") == id)
                {
                    el = e;
                    break;
                }
            }
            if (el == null) AddElementToContainer(containerTag, elTagName, innerXml, id);
            else el.InnerXml = innerXml;
        }

        protected XmlElement getOrCreateElement(string tagName)
        {
            XmlNode node = documentElement.GetElementsByTagName(tagName)[0];
            XmlElement el = (node == null) ? xml.CreateElement(tagName) : node as XmlElement;
            if (node == null) documentElement.AppendChild(el);
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
