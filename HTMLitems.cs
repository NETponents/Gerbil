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
                        void AddAttribute(string AttrString);
                        string GetElementType();
                    }
                    interface IHtmlContainer
                    {
                        void AddHtmlElement(HtmlElement element);
                    }
                    interface IHtmlTextField
                    {
                        void SetText(string text);
                    }
                    public class HtmlElement : IHtmlElement
                    {
                        List<string> AttributeList;

                        public HtmlElement()
                        {
                            AttributeList = new List<string>();
                        }

                        public void AddAttribute(string AttrString)
                        {
                            AttributeList.Add(AttrString);
                        }

                        public void AddAttribute(string AttrName, string AttrProperty)
                        {
                            AttributeList.Add(AttrName + "=" + AttrProperty);
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
                    public class HtmlTextField : IHtmlTextField
                    {
                        private string elementText = "";

                        public void SetText(string text)
                        {
                            elementText = text;
                        }
                    }
                    public class HtmlItem : IHtmlContainer, IHtmlElement
                    {
                        private HtmlElement elementObject = new HtmlElement();
                        private HtmlContainer elementContainer = new HtmlContainer();

                        public void AddAttribute(string AttrName, string AttrProperty)
                        {
                            elementObject.AddAttribute(AttrName, AttrProperty);
                        }

                        public void AddAttribute(string AttrString)
                        {
                            elementObject.AddAttribute(AttrString);
                        }

                        public void AddHtmlElement(HtmlElement element)
                        {
                            elementContainer.AddHtmlElement(element);
                        }

                        public string GetElementType()
                        {
                            return "html";
                        }
                    }
                    public class Header : IHtmlElement, IHtmlTextField
                    {
                        private HtmlElement elementObject = new HtmlElement();
                        private HtmlTextField elementText = new HtmlTextField();
                        private int headerSize = 1;

                        public void AddAttribute(string AttrName, string AttrProperty)
                        {
                            elementObject.AddAttribute(AttrName, AttrProperty);
                        }

                        public void AddAttribute(string AttrString)
                        {
                            elementObject.AddAttribute(AttrString);
                        }

                        public string GetElementType()
                        {
                            return "h" + headerSize;
                        }

                        public void SetHeaderSize(int size)
                        {
                            if(size >= 1 && size <= 6)
                            {
                                headerSize = size;
                            }
                        }

                        public void SetText(string text)
                        {
                            elementText.SetText(text);
                        }
                    }
                    public class Paragraph : IHtmlElement, IHtmlTextField
                    {
                        private HtmlElement elementObject = new HtmlElement();
                        private HtmlTextField elementText = new HtmlTextField();

                        public void AddAttribute(string AttrName, string AttrProperty)
                        {
                            elementObject.AddAttribute(AttrName, AttrProperty);
                        }

                        public void AddAttribute(string AttrString)
                        {
                            elementObject.AddAttribute(AttrString);
                        }

                        public string GetElementType()
                        {
                            return "p";
                        }

                        public void SetText(string text)
                        {
                            elementText.SetText(text);
                        }
                    }
                    public class LineBreak : IHtmlElement
                    {
                        public void AddAttribute(string AttrName, string AttrProperty)
                        {
                            // Do nothing
                        }

                        public void AddAttribute(string AttrString)
                        {
                            // Do nothing
                        }

                        public string GetElementType()
                        {
                            return "hr";
                        }
                    }
                }
            }
        }
        public class Reporter
        {
            private static bool isEnabled = false;

            public static void AddResult(/*TODO: Add parameters. */)
            {

            }
        }
    }
}
