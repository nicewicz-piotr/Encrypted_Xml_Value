using System;
using System.Collections.Generic;
using Biblioteka;
using System.IO;
using System.Xml.Serialization;

namespace Biblioteka
{
    public class XmlFileWriterReader
    {
        public string sciezka { get; set; }
        public XmlSerializer  serializer { get; set; }
        public List<Klient> list  = new List<Klient>();

        public XmlFileWriterReader()
        {
            sciezka = Path.Combine(Environment.CurrentDirectory, "konta.xml");
            serializer = new XmlSerializer(typeof(List<Klient>), new XmlRootAttribute("Klienci"));
        }

        public void WriteToXmlFile(List<Klient> list)
        {
            
            FileStream strumien = File.Create(this.sciezka);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Klient>), new XmlRootAttribute("Klienci"));

            serializer.Serialize(strumien, list);

            strumien.Close();

            Console.WriteLine($"Zapisano w {new FileInfo(this.sciezka).DirectoryName} zajętość w bajtach: {new FileInfo(this.sciezka).Length}\n");
        }

        public List<Klient> ReadFromXmlFile()
        {
            System.Console.WriteLine(File.ReadAllText(this.sciezka));

            FileStream odczytaneXml = File.Open(this.sciezka, FileMode.Open);

            List<Klient> odczytaneKonta = (List<Klient>) serializer.Deserialize(odczytaneXml); 

            list.Clear();

            foreach(Klient item in odczytaneKonta)
            {
                Klient klient = new Klient(){nazwisko = item.nazwisko, kartakredytowa = item.kartakredytowa, haslo = item.haslo};
                list.Add(klient);      
            }

            odczytaneXml.Close();

            return list;

        }
    }    
}
