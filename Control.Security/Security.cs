using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Control.Security
{
    public static class Security
    {
        internal static string AssinarXml(string xml, string signatureTag, X509Certificate2 Certificate)
        {
            SignedXml signedXml = null;
            XmlDocument doc = null;
            int idFound = 0;
            string algorithmCertificate = null;

            if (Certificate == null)
                throw new Exception("Certificado Digital não encontrado!");

            try
            {
                // Get hash algorithm
                algorithmCertificate = Certificate.GetKeyAlgorithm();

                // Create a new XML document.
                doc = new XmlDocument();

                // Format the document to ignore white spaces.
                doc.PreserveWhitespace = true;

                // Load the passed XML file using it’s name.
                doc.LoadXml(xml);

                // Verifica se a tag a ser assinada existe é única
                int qtdeRefUri = doc.GetElementsByTagName(signatureTag).Count;

                if (qtdeRefUri == 0)
                {
                    // a URI indicada Nao existe
                    throw new Exception(String.Format("Tag de assinatura {0} inexistente", signatureTag.Trim()));
                }
                // Existe mais de uma tag a ser assinada
                else if (qtdeRefUri > 1)
                {
                    // existe mais de uma URI indicada
                    throw new Exception(String.Format("A tag de assinatura {0} não é Única", signatureTag.Trim()));
                }

                // Create a SignedXml object.
                signedXml = new SignedXml(doc);

                // Add the key to the SignedXml document
                signedXml.SigningKey = Certificate.PrivateKey;

                // Create a reference to be signed
                Reference reference = new Reference();

                // pega o uri que deve ser assinada
                XmlAttributeCollection _Uri = doc.GetElementsByTagName(signatureTag).Item(0).Attributes;

                foreach (XmlAttribute _atributo in _Uri)
                {
                    if (_atributo.Name == "Id")
                    {
                        idFound = 1;
                        reference.Uri = "#" + _atributo.InnerText;
                    }
                }

                if (idFound == 0)
                    reference.Uri = string.Empty;

                // Add an enveloped transformation to the reference.
                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);

                XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                reference.AddTransform(c14);

                // Add the reference to the SignedXml object.
                signedXml.AddReference(reference);

                // Create a new KeyInfo object
                KeyInfo keyInfo = new KeyInfo();

                // Load the certificate into a KeyInfoX509Data object
                // and add it to the KeyInfo object.
                keyInfo.AddClause(new KeyInfoX509Data(Certificate));

                // Add the KeyInfo object to the SignedXml object.
                signedXml.KeyInfo = keyInfo;
                signedXml.ComputeSignature();

                // Get the XML representation of the signature and save
                // it to an XmlElement object.
                XmlElement xmlDigitalSignature = signedXml.GetXml();

                // Append the element to the XML document.
                doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

                return doc.InnerXml;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Erro Certificado Digital: {0}", ex.Message));
            }
        }

        public static string AssinaturaDigital(string xml, string signatureTag, X509Certificate2 Certificate)
        {
            return AssinarXml(xml, signatureTag, Certificate);
        }

        public static X509Certificate2 ObterCertificadoDigitalPorNome(string name)
        {
            X509Certificate2 certificado = null;

            //Pega o certificado para o usuario logado na máquina
            //O certificado deve ser instalado pelo usuario que será configurado para a aplicacao no IIS
            X509Store listCertificados = new X509Store(StoreLocation.CurrentUser);

            listCertificados.Open(OpenFlags.IncludeArchived);

            for (int i = 0; i < listCertificados.Certificates.Count; i++)
            {
                X509Certificate2 item = listCertificados.Certificates[i];

                if ((item.Subject.Contains(name)))
                {
                    certificado = item;
                    break;
                }
            }

            return certificado;
        }
    }
}
