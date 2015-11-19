using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerbil
{
    namespace Reporting
    {
        namespace Generation
        {
            namespace HTML
            {
                namespace raw
                {
                    interface IHtmlElement
                    {
                        void AddAttribute(string AttrName, string AttrProperty);
                        string GetElementType();
                    }
                    interface IHtmlContainer
                    {
                        void AddHtmlElement(HtmlElement element);
                    }
                    public class HtmlElement : IHtmlElement
                    {
                        Dictionary<string, string> AttributeList;

                        public HtmlElement()
                        {
                            AttributeList = new Dictionary<string, string>();
                        }
                        public void AddAttribute(string AttrName, string AttrProperty)
                        {
                            AttributeList.Add(AttrName, AttrProperty);
                        }

                        public string GetElementType()
                        {
                            return "";
                        }
                    }
                    public class HtmlContainer : IHtmlContainer
                    {
                        private List<HtmlElement> elementlist;

                        public HtmlContainer()
                        {
                            elementlist = new List<HtmlElement>();
                        }
                        public void AddHtmlElement(HtmlElement element)
                        {
                            elementlist.Add(element);
                        }
                    }
                    public class HtmlItem : IHtmlContainer, IHtmlElement
                    {
                        private HtmlElement elementObject = new HtmlElement();
                        public void AddAttribute(string AttrName, string AttrProperty)
                        {
                            throw new NotImplementedException();
                        }

                        public void AddHtmlElement(HtmlElement element)
                        {
                            throw new NotImplementedException();
                        }

                        public string GetElementType()
                        {
                            return "html";
                        }
                    }
                }
            }
        }
    }
}
