using System;
using System.Xml.Serialization;

namespace Biblioteka
{
    [XmlRoot("klient")]
    public class Klient
    {
        public Klient()
        {
            
        }
        public string nazwisko { get; set; }
        public string kartakredytowa { get; set; }
        public string haslo { get; set; }
    }
}
