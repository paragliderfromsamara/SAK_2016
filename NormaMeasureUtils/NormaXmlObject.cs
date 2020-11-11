using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;


namespace NormaMeasure.Utils
{
    public class NormaXmlObject
    {
        private XDocument xDoc;
        private XElement xRoot;
        public string RootElementTagName => this.GetType().Name;
        public bool WasChanged;
        public string InnerXml => readInnerXml();


        private string state_id;

        private string refreshStateId()
        {
            return state_id = DateTime.Now.ToBinary().ToString("x");
        }
        public string StateId => state_id;

        private string readInnerXml()
        {
            setStateIdToRoot();
            XmlReader r = xDoc.CreateReader();
            r.MoveToContent();
            return r.ReadOuterXml();
        }

        private void setStateIdToRoot()
        {
            xRoot.SetAttributeValue("state_id", state_id);
        }

        private string readInnerXml(XElement e)
        {
            XmlReader r = e.CreateReader();
            r.MoveToContent();
            return r.ReadOuterXml();
        }

        private XElement createFromAString(string innerXml)
        {
            XElement e = XElement.Parse(innerXml);
            return e;
        }

        private bool isValid;
        public bool IsValid => isValid;
        public string ElementId
        {
            set
            {
                xRoot.Attribute("id").Value = value;
            }get
            {
                return xRoot.Attribute("id").Value;
            }
        }
        public NormaXmlObject()
        {
            XAttribute a = new XAttribute("id", "null");
            XAttribute u = new XAttribute("state_id", refreshStateId());
            xDoc = new XDocument();
            xRoot = new XElement(RootElementTagName);
            xRoot.Add(a);
            xRoot.Add(u);
            xDoc.Add(xRoot);
            isValid = true;
        }


        public NormaXmlObject(string innerXml) : this()
        {
            xDoc.RemoveNodes();
            xRoot = createFromAString(innerXml);
            xDoc.Add(xRoot);
            isValid = xRoot.Name == this.RootElementTagName;
            if (isValid) setStateIdFromXML();
        }

        private void setStateIdFromXML()
        {
            XAttribute a = xRoot.Attribute("state_id");
            if (a != null)
            {
                state_id = a.Value;
            }
            else refreshStateId();
        }

        protected void setXmlProp(string key, string value)
        {
                XElement el = xRoot.Element(key);
                bool isNew = el == null;
                if (isNew)
                {
                    el = new XElement(key);
                    xRoot.Add(el);
                    el.Value = value;
                }
                el.Value = value;
                refreshStateId();
        }

        protected string getXmlProp(string key)
        {
            XElement el = xRoot.Element(key);
            return (el == null) ? String.Empty : el.Value;
        }

        protected Dictionary<string, string> GetNodesInnerXmlFromContainer(string containerTag, string nodeTagName)
        {
            XElement parent = xRoot.Element(containerTag);
            Dictionary<string, string> d = new Dictionary<string, string>();
            if (parent != null)
            {
                int i = 0;
                foreach(XElement e in parent.Elements(nodeTagName))
                {
                    XAttribute idAttr = e.Attribute("id");
                    string id = idAttr == null ? (i++).ToString() : idAttr.Value;
                    d.Add(id, readInnerXml(e));
                }
            }
            return d;
        }

        protected void AddElementToContainer(string containerTag, string elementAsXML)
        {
            XElement container = getOrCreateElement(containerTag);
            container.Add(createFromAString(elementAsXML));
            refreshStateId();
        }

        protected void ReplaceElementOnContainer(string containerTag, string elTagName, string innerXml, string id)
        {
            XElement container = getOrCreateElement(containerTag);
            XElement child = null;
            XElement newEl = createFromAString(innerXml);

            foreach (XElement e in container.Elements(elTagName))
            {
                if (e.Attribute("id").Value == id)
                {
                    child = e;
                    break;
                }
            }
            if (child == null)
            {
               container.Add(newEl);
            }else
            {
                child.ReplaceWith(newEl);
            }
            refreshStateId();
        }

        protected void RemoveElementFromContainer(string containerTagName, string elTagName, string id)
        {
            XElement container = getOrCreateElement(containerTagName);
            foreach (XElement e in container.Elements(elTagName))
            {
                try
                {
                    if (e.Attribute("id").Value != id) continue;
                    e.Remove();
                    refreshStateId();
                }
                catch
                {
                    continue;
                }
            }

        }

        protected XElement getOrCreateElement(string tagName)
        {
            XElement node = xRoot.Element(tagName);
            XElement el = (node == null) ? new XElement(tagName) : node;
            if (node == null) xRoot.Add(el);
            return el;
        }

        protected bool tryGetIntXmlProp(string key, out int defVal)
        {
            return int.TryParse(getXmlProp(key), out defVal);
        }

        protected bool hasElement(string key)
        {
            return xDoc.Element(key) != null;
        }

    }
}
