using System;
using System.Collections.Generic;
using Biblioteka;
using System.IO;
using System.Xml.Serialization;

namespace Szyfrowanie
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Klient> konta = new List<Klient>
            {
                new Klient(){nazwisko = "Bartek Nowak", kartakredytowa = "1234-5678-9012-3456", haslo = "H45lo"},
                new Klient(){nazwisko = "Bartek Nowak", kartakredytowa = "1234-5678-9012-3456", haslo = "H45lo"},
                new Klient(){nazwisko = "Bartek Nowak", kartakredytowa = "1234-5678-9012-3456", haslo = "H45lo"}
            };

            string sciezka = Path.Combine(Environment.CurrentDirectory, "konta.xml");

            XmlFileWriterReader xmlFileWriterReader = new XmlFileWriterReader();

            xmlFileWriterReader.WriteToXmlFile(konta); 

            List<Klient> lista = xmlFileWriterReader.ReadFromXmlFile();           
            
            System.Console.WriteLine("Podaj haslo");

            string haslo = Console.ReadLine();

                lista.ForEach(c => c.kartakredytowa = c.kartakredytowa.Szyfruj(haslo));

                lista.ForEach(c => c.haslo = c.haslo.HaszujSHA256(haslo));

                lista.ForEach(c => System.Console.WriteLine(c.kartakredytowa));

                xmlFileWriterReader.WriteToXmlFile(lista);

                xmlFileWriterReader.ReadFromXmlFile(); 

                System.Console.WriteLine(lista[0].kartakredytowa.Length);

                lista.ForEach(c => c.kartakredytowa = c.kartakredytowa.Odszyfruj(haslo));

                xmlFileWriterReader.WriteToXmlFile(lista);

                lista = xmlFileWriterReader.ReadFromXmlFile();

        }
    }
}
