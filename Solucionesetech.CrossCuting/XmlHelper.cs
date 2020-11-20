using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Solucionesetech.CrossCutting
{
    public class XmlHelper
    {
        private static string RetrieveXslTemplate(string xsltfile)
        {
            using (StreamReader sr = new StreamReader(xsltfile))
            {
                return sr.ReadToEnd();
            }

        }
        public static string  GetHtmlFromXslt(string xsltfile, object sourceObject)
        {
            string xsl = RetrieveXslTemplate(xsltfile);
            return  XmlHelper.PrepareHTMLFromXSL(sourceObject, xsl);
        }
        private static string PrepareHTMLFromXSL(object sourceObject, string xsl)
        {
            XmlReader xmlReader = null;
            string htmlContent = string.Empty;
            //Load the XSL into a xml reader 
            StringReader sr = new StringReader(xsl);
            TextReader tr = sr;
            xmlReader = XmlReader.Create(tr);
            htmlContent = PrepareContent(sourceObject, xmlReader);
            return htmlContent;
        }

        private static string PrepareContent(object sourceObject, XmlReader xmlReader)
        {

            //Serialize the input object and load into xml document 
            StringWriter swwriter = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(sourceObject.GetType());
            serializer.Serialize(swwriter, sourceObject);
            StringReader sr = new StringReader(swwriter.ToString());
            XPathDocument doc = new XPathDocument(sr);

            //output content is written to xmlwriter. initialize it with all settings 
            StringBuilder sbhtmlString = new StringBuilder();
            StringWriter swWriter = new StringWriter();
            XmlWriter writer = XmlWriter.Create(sbhtmlString);
            XmlTextWriter xmlwriter = new XmlTextWriter(swWriter);
            xmlwriter.Formatting = Formatting.Indented;

            XslCompiledTransform xslTransform = new XslCompiledTransform();
            xslTransform.Load(xmlReader);
            xslTransform.Transform(doc, xmlwriter);

            xmlwriter.Flush();
            writer.Flush();
            swWriter.Flush();

            if (swWriter != null)
            {
                swWriter.Close();
                swWriter.Dispose();
            }
            if (xslTransform != null)
            {
                xslTransform = null;
            }
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }
            if (xmlReader != null)
            {
                xmlReader.Close();
                xmlReader = null;
            }
            if (doc != null)
            {
                doc = null;
            }
            if (xmlwriter != null)
            {
                xmlwriter.Close();
                xmlwriter = null;
            }
            if (serializer != null)
            {
                serializer = null;
            }
            return swWriter.ToString();
        }

        public static string SerializeObject<T>(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}
