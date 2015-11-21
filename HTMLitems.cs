using System;
using System.Collections.Generic;
using System.IO;

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
                            return "div";
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
                    public Raw.HtmlItem content;

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
            private static Data.Database<Data.Models.Devices.Device> db = new Data.Database<Data.Models.Devices.Device>("Report DB");

            public static void AddResult(Data.Database<Data.Models.Devices.Device> data)
            {
                //if(!isEnabled)
                //{
                //    return;
                //}
                db = data;
            }
            /// <summary>
            /// Generates a network security report based on probing.
            /// </summary>
            /// <param name="filename">Directory to write output to.</param>
            public static void GenerateResults(string filename, Data.Database<Data.Models.Devices.Device> indata)
            {
                //if(!isEnabled)
                //{
                //    return;
                //}
                // Create body
                Generation.HTML.Raw.BodyItem rBody = new Generation.HTML.Raw.BodyItem();
                // Get footers
                Generation.HTML.Raw.Div rHeader = ReportingModules.GenerateHeader();
                Generation.HTML.Raw.Div rFooter = ReportingModules.GenerateFooter();
                // Get content
                Generation.HTML.Raw.Div rContent = GenerateBody(indata);
                // Feed all components into body container
                rBody.AddHtmlElement(rHeader);
                rBody.AddHtmlElement(rContent);
                rBody.AddHtmlElement(rFooter);
                contentHolder.content.AddHtmlElement(rBody);
                string output = contentHolder.GetGeneratedHTML();
                // Verify file path exists
                if(!Directory.Exists(filename))
                {
                    Directory.CreateDirectory(filename);
                }
                // Write to file
                File.WriteAllText(filename + "report.html", output);
            }
            public static string getElementHTML(Generation.HTML.Raw.IHtmlElement element)
            {
                Generation.HTML.Raw.IHtmlContainer objTest = element as Generation.HTML.Raw.IHtmlContainer;
                Generation.HTML.Raw.IHtmlTextField objTest2 = element as Generation.HTML.Raw.IHtmlTextField;
                if (objTest != null)
                {
                    string inner = "";
                    foreach (Generation.HTML.Raw.IHtmlElement i in objTest.GetElements())
                    {
                        inner += getElementHTML(i);
                    }
                    string result2 = string.Format("<{0} {2}>{1}</{0}>", element.GetElementType(), inner, element.GetAttributeString());
                    return result2;
                }
                else if (objTest2 != null)
                {
                    string result3 = string.Format("<{0} {2}>{1}</{0}>", element.GetElementType(), objTest2.GetText(), element.GetAttributeString());
                    return result3;
                }
                else
                {
                    string result = string.Format("<{0} {1}></{0}>", element.GetElementType(), element.GetAttributeString());
                    return result;
                }
            }
            private static Generation.HTML.Raw.Div GenerateBody(Data.Database<Data.Models.Devices.Device> indata)
            {
                Generation.HTML.Raw.Div bodyResult = new Generation.HTML.Raw.Div();
                foreach(string dataID in indata.getAllIDs())
                {
                    // Create linebreak
                    Generation.HTML.Raw.LineBreak lr = new Generation.HTML.Raw.LineBreak();
                    // Title
                    Generation.HTML.Raw.Header h1 = new Generation.HTML.Raw.Header();
                    if (!String.IsNullOrEmpty(indata.Read(dataID).getDeviceNetworkName()))
                    {
                        h1.SetText(indata.Read(dataID).getDeviceNetworkName());
                    }
                    else
                    {
                        h1.SetText(indata.Read(dataID).getDeviceIPAddress().ToString());
                    }
                    h1.SetHeaderSize(4);
                    // List of open ports
                    string portString = "";
                    foreach(int i in indata.Read(dataID).getPorts())
                    {
                        portString += i + " ";
                    }
                    Generation.HTML.Raw.Paragraph ports = new Generation.HTML.Raw.Paragraph();
                    ports.SetText(Generation.HTML.Text.TextModifier.Bold("Open ports: ") + portString);
                    // Send all elements to container
                    bodyResult.AddHtmlElement(lr);
                    bodyResult.AddHtmlElement(h1);
                    bodyResult.AddHtmlElement(ports);
                }
                return bodyResult;
            }
        }
        class ReportingModules
        {
            public static Generation.HTML.Raw.Div GenerateHeader()
            {
                // Result container
                Generation.HTML.Raw.Div resultHeader = new Generation.HTML.Raw.Div();
                // Report title
                Generation.HTML.Raw.Header title = new Generation.HTML.Raw.Header();
                title.SetText("Network Analysis Report");
                // Secondary title
                Generation.HTML.Raw.Header title2 = new Generation.HTML.Raw.Header();
                title2.SetText("Gerbil");
                title2.SetHeaderSize(3);
                // Line break
                Generation.HTML.Raw.LineBreak lr = new Generation.HTML.Raw.LineBreak();
                // Add all items to container
                resultHeader.AddHtmlElement(title);
                resultHeader.AddHtmlElement(title2);
                resultHeader.AddHtmlElement(lr);
                // Return result
                return resultHeader;
            }
            public static Generation.HTML.Raw.Div GenerateFooter()
            {
                // Result container
                Generation.HTML.Raw.Div resultFooter = new Generation.HTML.Raw.Div();
                // Line break
                Generation.HTML.Raw.LineBreak lr = new Generation.HTML.Raw.LineBreak();
                // Secondary title
                Generation.HTML.Raw.Header text = new Generation.HTML.Raw.Header();
                text.SetText("v0.1.0");
                text.SetHeaderSize(6);
                // Add all items to footer container
                resultFooter.AddHtmlElement(lr);
                resultFooter.AddHtmlElement(text);
                // Return result
                return resultFooter;
            }
        }
    }
}
