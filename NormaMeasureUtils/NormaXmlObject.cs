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
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private XDocument xDoc;
        private XElement xRoot;
        string _id = string.Empty;
        protected string id
        {
            set
            {
                bool f = value != _id;
                if(f)
                {
                    _id = value;
                    setChangedFlag(f);
                }
            }
            get
            {
                return _id;
            }
        }
        private string xmlID
        {
            get { return (!string.IsNullOrWhiteSpace(_id)) ? _id : "null"; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == "null") _id = "";
                else
                {
                    _id = value;
                }
            }
        }
        private string _innerXML = null;
        private string innerXML
        {
            get
            {
                return _innerXML;
            }set
            {
                XElement e = createFromAString(value);
                if (isValidRoot(e))
                {
                    string new_state_id = state_id;
                    if (isDifferentState(e, out new_state_id))
                    {
                        xRoot = e;
                        xDoc.RemoveNodes();
                        xDoc.Add(xRoot);
                        buildFromXML();
                        state_id_was = state_id = new_state_id;
                       
                        isValid = true;
                        _innerXML = value;
                       
                    }

                }else
                {
                    isValid = false;
                }
            }
        }

        public string RootElementTagName => this.GetType().Name;
        public bool WasChanged => state_id != state_id_was;
        public string InnerXml
        {
           get
            {
                if (WasChanged)
                {
                    prepareXMLDocumentForFill();
                    fillXMLDocument();
                    _innerXML = readInnerXml();
                    setChangesProccessed();
                }
                return _innerXML;
            }
            set
            {
                innerXML = value;
            }
        }

        private string state_id_was; 
        private string state_id;

        private string refreshStateId()
        {
            int v;
            lock (syncLock)
            {
                DateTime t = DateTime.UtcNow;
                v = random.Next(100000, 99999999);
                string sTime = t.ToBinary().ToString("x") + v.ToString("x");//$"{t.ToString()}:{t.Millisecond}-{r.Next(1, 99999999).ToString("x")}";
                this.state_id_was = state_id;
                this.state_id = sTime;
            }
            return state_id;
        }

        private void setChangesProccessed()
        {
            this.state_id_was = state_id;
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

        protected string InnerXmlOfElement(string el_name)
        {
            if (hasElement(el_name)) return readInnerXml(xRoot.Element(el_name));
            else return String.Empty;
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
            refreshStateId();
            initXMLDocument();
            isValid = true;
        }

        public NormaXmlObject(int objectId)
        {
            _id = objectId.ToString();
            refreshStateId();
            initXMLDocument();
            isValid = true;
        }


        public NormaXmlObject(string inner_xml) : this()
        {
            this.innerXML = inner_xml;
            //initXMLDocument();
            //xDoc.RemoveNodes();
            //xRoot = createFromAString(inner_xml);
            //xDoc.Add(xRoot);
            //isValid = xRoot.Name == this.RootElementTagName;
            //if (isValid) setStateIdFromXML();
        }

        protected virtual bool isValidRoot(XElement root_el)
        {
            return root_el.Name == this.RootElementTagName;
        }

        private bool isDifferentState(XElement e, out string new_state_id)
        {
            new_state_id = e.Attribute("state_id").Value;
            return state_id != new_state_id && !string.IsNullOrWhiteSpace(new_state_id);
        }

        /// <summary>
        /// Собирает объект из xml кода
        /// </summary>
        protected virtual void buildFromXML()
        {
            state_id_was = state_id = xRoot.Attribute("state_id").Value;
            xmlID = xRoot.Attribute("id").Value;
        }


        protected virtual void fillXMLDocument()
        {

        }

        private void initXMLDocument()
        {
            XAttribute a = new XAttribute("id", xmlID);
            XAttribute u = new XAttribute("state_id", state_id);
            xDoc = new XDocument();
            xRoot = new XElement(RootElementTagName);
            xRoot.Add(a);
            xRoot.Add(u);
            xDoc.Add(xRoot);
        }

        protected void setChangedFlag(bool f)
        {
            if (!WasChanged && f) refreshStateId();
        }

        private void prepareXMLDocumentForFill()
        {
            if (xDoc == null) initXMLDocument();
            else
            {
                xRoot.RemoveNodes();
                xRoot.SetAttributeValue("state_id", state_id);
                xRoot.SetAttributeValue("id", xmlID);
            }
        }


        protected void setXmlProp(string key, string value)
        {
                XElement el = xRoot.Element(key);
                bool isNew = el == null;
                if (isNew)
                {
                    el = new XElement(key);
                    xRoot.Add(el);
                    retry:
                    try
                    {
                     el.Value = value;
                    }
                    catch(System.ArgumentNullException)
                    {
                    value = String.Empty;
                    goto retry;
                    }

                }
                el.Value = value;
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
    
        }

        protected void ReplaceElement(string tagName, string new_el_inner)
        {
            XElement newEl = createFromAString(new_el_inner);
            if (hasElement(tagName)) xRoot.Element(tagName).Remove();
            xRoot.Add(newEl);
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

        protected bool tryGetUIntXmlProp(string key, out uint defVal)
        {
            return uint.TryParse(getXmlProp(key), out defVal);
        }


        protected bool hasElement(string key)
        {
            return xDoc.Element(key) != null;
        }

    }
}
