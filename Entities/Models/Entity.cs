using Entities.LinkModels;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Entities.Models
{
    public class Entity:DynamicObject, IXmlSerializable, IDictionary<string, object>
    {
        public object this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<string> Keys => throw new NotImplementedException();

        public ICollection<object> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public XmlSchema? GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private void WriteLinksToXML(string key, object value, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(key);
            if (value.GetType() == typeof(List<Link>))
            {
                foreach (var item in value as List<Link>)
                {
                    xmlWriter.WriteStartElement(nameof(Link));
                    WriteLinksToXML(nameof(item.Href), item.Href, xmlWriter);
                    WriteLinksToXML(nameof(item.Method), item.Method, xmlWriter);
                    WriteLinksToXML(nameof(item.Rel), item.Rel, xmlWriter);
                    xmlWriter.WriteEndElement();
                }
            }
            else
            {
                xmlWriter.WriteString(value.ToString());
            }
            xmlWriter.WriteEndElement();
        }
    }
}
