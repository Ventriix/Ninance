using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Ninance_v2.Core.InvoiceGenerator;

namespace Ninance_v2.Core
{

    [XmlRoot("InvoiceProfile")]
    public class InvoiceProfile
    {
        [XmlIgnore]
        public string DisplayName
        {
            get
            {
                return Address.CompanyName == "" ? Address.PersonName + ", " + Address.Street + ", " + Address.State + ", " + Address.Postcode + " " + Address.City + ", " + Address.CountryName + ", " + Address.Email + ", " + Address.Phone : Address.CompanyName + ", " + Address.PersonName + ", " + Address.Street + ", " + Address.State + ", " + Address.Postcode + " " + Address.City + ", " + Address.CountryName + ", " + Address.Email + ", " + Address.Phone;
            }
        }

        [XmlAttribute("Id")]
        public int Id { get; set; }

        public Address Address { get; set; }
        public string VatNumber { get; set; }

        public InvoiceProfile(Address Address, string VatNumber, int Id)
        {
            this.Address = Address;
            this.VatNumber = VatNumber;
            this.Id = Id;
        }

        public InvoiceProfile() { }
    }

    public class InvoiceProfileHandler
    {
        private string _targetFilePath = "Data/profiles.xml";

        public InvoiceProfileHandler()
        {
            if (!File.Exists(_targetFilePath))
                new XDocument(new XElement("Profiles")).Save(_targetFilePath);
        }

        public void SaveProfile(Address address, string vatNumber)
        {
            InvoiceProfile invoiceProfile = new InvoiceProfile(address, vatNumber, ListProfiles().Count() + 1);

            XDocument document = XDocument.Load(_targetFilePath);
            XElement invoiceElement;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceProfile));
                    xmlSerializer.Serialize(streamWriter, invoiceProfile);
                    invoiceElement = XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
                }
            }

            document.Element("Profiles").Add(invoiceElement);
            document.Save(_targetFilePath);
        }

        public InvoiceProfile GetProfile(int id)
        {
            XDocument document = XDocument.Load(_targetFilePath);
            XElement profile = document.Element("Profiles").Elements("InvoiceProfile").First(p => int.Parse(p.Attribute("Id").Value) == id);

            if (profile == null)
                return null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceProfile));
            return (InvoiceProfile) xmlSerializer.Deserialize(profile.CreateReader());
        }

        public List<InvoiceProfile> ListProfiles()
        {
            XDocument document = XDocument.Load(_targetFilePath);
            List<XElement> unserializedProfiles = document.Element("Profiles").Elements("InvoiceProfile").ToList();
            List<InvoiceProfile> serializedProfiles = new List<InvoiceProfile>();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceProfile));

            foreach (XElement element in unserializedProfiles)
                serializedProfiles.Add((InvoiceProfile)xmlSerializer.Deserialize(element.CreateReader()));

            return serializedProfiles;
        }
    }
}
