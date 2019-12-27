using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace eaw.build.app.util.xml
{
    internal static class XmlUtility
    {
        internal static T ReadAndValidateXmlFile<T>(string embeddedXsdFileName, string xmlFilePath)
        {
            T xmlData;
            XmlSerializer xsdSchemaSerializer = new XmlSerializer(typeof(XmlSchema));
            XmlSerializer xmlDataSerializer = new XmlSerializer(typeof(T));

            XmlSchemaSet schemas = new XmlSchemaSet();
            XmlSchema schema;
            using (Stream xsdStream = ResourceUtility.GetResourceAsStreamByFileName(embeddedXsdFileName))
            {
                schema = (XmlSchema) xsdSchemaSerializer.Deserialize(xsdStream);
            }

            schemas.Add(schema);
            XmlReaderSettings settings = new XmlReaderSettings
            {
                Schemas = schemas,
                ValidationType = ValidationType.Schema,
                ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints |
                                  XmlSchemaValidationFlags.ReportValidationWarnings |
                                  XmlSchemaValidationFlags.ProcessInlineSchema |
                                  XmlSchemaValidationFlags.ProcessSchemaLocation
            };

            settings.ValidationEventHandler +=
                (sender, arguments) => throw new XmlSchemaValidationException(arguments.Message);

            using (Stream xmlStream = File.OpenRead(xmlFilePath))
            using (XmlReader reader = XmlReader.Create(xmlStream, settings))
            {
                xmlData = (T) xmlDataSerializer.Deserialize(reader);
            }

            if (xmlData != null)
            {
                return xmlData;
            }

            throw new XmlSchemaException("The xml could not be deserialized.");
        }

        internal static void WriteXmlFile<T>(string filePath, T content, bool prettyPrint = true)
        {
            XmlSerializer xmlDataSerializer = new XmlSerializer(typeof(T));
            using (XmlWriter xmlWriter = XmlWriter.Create(filePath, new XmlWriterSettings()
            {
                Encoding = Encoding.UTF8,
                Indent = prettyPrint,
                CheckCharacters = true,
                OmitXmlDeclaration = true
            }))
            {
                xmlDataSerializer.Serialize(xmlWriter, content);
            }
        }
    }
}