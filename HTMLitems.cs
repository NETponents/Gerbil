using System;
using System.Collections.Generic;

namespace Gerbil
{
    namespace Reporting
    {
        namespace Generation
        {
            namespace HTML
            {
                namespace Raw
                {
                    /// <summary>
                    /// Interface for all generated HTML elements.
                    /// </summary>
                    public interface IHtmlElement
                    {
                        void AddAttribute(string AttrName, string AttrProperty);
                        void AddAttribute(string AttrString);
                        string GetAttributeString();
                        string GetElementType();
                    }
                    /// <summary>
                    /// Interface for all generated HTML elements that can hold other elements.
                    /// </summary>
                    public interface IHtmlContainer : IHtmlElement
                    {
                        void AddHtmlElement(IHtmlElement element);
                        List<IHtmlElement> GetElements();
                    }
                    /// <summary>
                    /// Interface for all generated HTML elements that can hold text.
                    /// </summary>
                    public interface IHtmlTextField : IHtmlElement
                    {
                        void SetText(string text);
                        string GetText();
                    }
                    /// <summary>
                    /// Element class for IHtmlElement interface.
                    /// </summary>
                    public class HtmlElement : IHtmlElement
                    {
                        private List<string> AttributeList;

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

                        public string GetAttributeString()
                        {
                            string result = "";
                            foreach(string i in AttributeList)
                            {
                                result += i + " ";
                            }
                            return result;
                        }

                        public string GetElementType()
                        {
                            return "";
                        }
                    }
                    /// <summary>
                    /// Container class for IHtmlContainer interface.
                    /// </summary>
                    public class HtmlContainer : IHtmlContainer
                    {
                        private List<IHtmlElement> elementlist;
                        private List<string> AttributeList;

                        public HtmlContainer()
                        {
                            elementlist = new List<IHtmlElement>();
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

                        public void AddHtmlElement(IHtmlElement element)
                        {
                            elementlist.Add(element);
                        }

                        public string GetAttributeString()
                        {
                            string result = "";
                            foreach (string i in AttributeList)
                            {
                                result += i + " ";
                            }
                            return result;
                        }

                        public List<IHtmlElement> GetElements()
                        {
                            return elementlist;
                        }

                        public string GetElementType()
                        {
                            throw new NotImplementedException();
                        }
                    }
                    /// <summary>
                    /// TextField class for IHtmlTextField interface.
                    /// </summary>
                    public class HtmlTextField : IHtmlTextField
                    {
                        private string elementText = "";

                        public void AddAttribute(string AttrString)
                        {
                            throw new NotImplementedException();
                        }

                        public void AddAttribute(string AttrName, string AttrProperty)
                        {
                            throw new NotImplementedException();
                        }

                        public string GetAttributeString()
                        {
                            throw new NotImplementedException();
                        }

                        public string GetElementType()
                        {
                            throw new NotImplementedException();
                        }

                        public string GetText()
                        {
                            return elementText;
                        }

                        public void SetText(string text)
                        {
                            elementText = text;
                        }
                    }
                    /// <summary>
                    /// Main html tag for generated HTML pages.
                    /// </summary>
                    public class HtmlItem : IHtmlContainer
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

                        public void AddHtmlElement(IHtmlElement element)
                        {
                            elementContainer.AddHtmlElement(element);
                        }

                        public string GetElementType()
                        {
                            return "html";
                        }

                        public string GetAttributeString()
                        {
                            return elementObject.GetAttributeString();
                        }

                        public List<IHtmlElement> GetElements()
                        {
                            return elementContainer.GetElements();
                        }
                    }
                    /// <summary>
                    /// Body tag for generated HTML pages.
                    /// </summary>
                    public class BodyItem : IHtmlContainer
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

                        public void AddHtmlElement(IHtmlElement element)
                        {
                            elementContainer.AddHtmlElement(element);
                        }

                        public string GetElementType()
                        {
                            return "body";
                        }

                        public string GetAttributeString()
                        {
                            return elementObject.GetAttributeString();
                        }

                        public List<IHtmlElement> GetElements()
                        {
                            return elementContainer.GetElements();
                        }
                    }
                    /// <summary>
                    /// Div tag for generated HTML pages.
                    /// </summary>
                    public class Div : IHtmlContainer
                    {
                        // TODO: finish class
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

                        public void AddHtmlElement(IHtmlElement element)
                        {
                            elementContainer.AddHtmlElement(element);
                        }

                        public string GetElementType()
                        {
                            return "body";
                        }

                        public string GetAttributeString()
                        {
                            return elementObject.GetAttributeString();
                        }

                        public List<IHtmlElement> GetElements()
                        {
                            return elementContainer.GetElements();
                        }
                    }
                    /// <summary>
                    /// Header element for generated HTML pages.
                    /// </summary>
                    public class Header : IHtmlTextField
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

                        public string GetText()
                        {
                            return elementText.GetText();
                        }

                        public string GetAttributeString()
                        {
                            return elementObject.GetAttributeString();
                        }
                    }
                    /// <summary>
                    /// Paragraph element for generated HTML pages.
                    /// </summary>
                    public class Paragraph : IHtmlTextField
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

                        public string GetText()
                        {
                            return elementText.GetText();
                        }

                        public string GetAttributeString()
                        {
                            return elementObject.GetAttributeString();
                        }
                    }
                    /// <summary>
                    /// Line break (hr) element for generated HTML pages.
                    /// </summary>
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

                        public string GetAttributeString()
                        {
                            return "";
                        }
                    }
                    /// <summary>
                    /// Line break (br) element for generated HTML pages.
                    /// </summary>
                    public class LineReturn : IHtmlElement
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
                            return "br";
                        }

                        public string GetAttributeString()
                        {
                            return "";
                        }
                    }
                }
                namespace Text
                {
                    /// <summary>
                    /// Generates HTML code to modify appearance of generated text.
                    /// </summary>
                    public class TextModifier
                    {
                        public static string Bold(string text)
                        {
                            return format(text, "b");
                        }
                        public static string Italics(string text)
                        {
                            return format(text, "i");
                        }
                        private static string format(string text, string op)
                        {
                            return string.Format("<{0}>{1}</{0}>", op, text);
                        }
                    }
                }
                public class Page
                {
                    private string pName;
                    private Raw.HtmlItem content;

                    public Page(string name)
                    {
                        pName = name;
                        content = new Raw.HtmlItem();
                    }
                    public string GetName()
                    {
                        return pName;
                    }
                    public void AddElement(Raw.IHtmlElement i)
                    {
                        content.AddHtmlElement(i);
                    }
                    public string GetGeneratedHTML()
                    {
                        return Reporter.getElementHTML(content);
                    }
                }
            }
        }
        /// <summary>
        /// Object for generating HTML report output.
        /// </summary>
        public class Reporter
        {
            private static bool isEnabled = false;
            private static Generation.HTML.Page contentHolder = new Generation.HTML.Page("Report Results");

            public static void AddResult(/*TODO: Add parameters. */)
            {
                if(isEnabled)
                {

                }
            }
            public static void GenerateResults(string filename)
            {
                if(!isEnabled)
                {
                    return;
                }

            }
            public static string getElementHTML(Generation.HTML.Raw.IHtmlContainer element)
            {
                string inner = "";
                foreach(Generation.HTML.Raw.IHtmlElement i in element.GetElements())
                {
                    inner += getElementHTML(i);
                }
                string result = string.Format("<{0} {2}>{1}</{0}>", element.GetElementType(), inner, element.GetAttributeString());
                return result;
            }
            public static string getElementHTML(Generation.HTML.Raw.IHtmlTextField element)
            {
                string result = string.Format("<{0} {2}>{1}</{0}>", element.GetElementType(), element.GetText(), element.GetAttributeString());
                return result;
            }
            public static string getElementHTML(Generation.HTML.Raw.IHtmlElement element)
            {
                string result = string.Format("<{0} {2}></{0}>", element.GetElementType(), element.GetAttributeString());
                return result;
            }
        }
    }
}
