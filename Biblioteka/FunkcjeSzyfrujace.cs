using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Biblioteka
{
    public static class KodowanieListy
    {
        private static List<ClientSalt> clientSalts = new List<ClientSalt>();
        private static readonly byte[] sol = Encoding.Unicode.GetBytes("7BANANOW");

        private static readonly int iteracje = 2000;

        public static string Szyfruj(this string jawnytekst, string haslo)
        {
            
            foreach (var item in sol)
               item.ToString("X");  

            byte[] jawneBajty = Encoding.Unicode.GetBytes(jawnytekst);

            Aes aes = Aes.Create();

            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(haslo, sol, iteracje); //algorytm haszujący z iteracjami

            aes.IV = rfc.GetBytes(16); //wyznacz wektor inicjujący algorytmu AES

            aes.Key = rfc.GetBytes(32); //wyznacz klucz algorytmu AES

            MemoryStream ms = new MemoryStream();

            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(jawneBajty, 0, jawneBajty.Length);
            }

            return Convert.ToBase64String(ms.ToArray());

        }

        public static string Odszyfruj(this string zaszyfrowanyTekst, string haslo)
        {
            byte[] zaszyfrowaneBajty = Convert.FromBase64String(zaszyfrowanyTekst);

            Aes aes = Aes.Create();

            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(haslo, sol, iteracje);

            aes.IV = rfc.GetBytes(16);

            aes.Key = rfc.GetBytes(32);

            MemoryStream ms = new MemoryStream();

            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(zaszyfrowaneBajty, 0, zaszyfrowaneBajty.Length);
            }

            return Encoding.Unicode.GetString(ms.ToArray());
        }



        public static string HaszujSHA256(this string kontohaslo,  string haslo)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create(); // Generator liczb losowych do kryptografii

            Byte[] bajtySoli = new byte[16]; // tablica typu bajt

            rng.GetBytes(bajtySoli); //wypełnij tablicę losowymi wartościami

            string tekstSoli = Convert.ToBase64String(bajtySoli);

            SHA256 sha = SHA256.Create();

            string soloneHaslo = haslo + tekstSoli;

            string skrotSolonegoHasla = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(soloneHaslo)));

            ClientSalt clientSalt = new ClientSalt
            {
                SkrotSolonegoHasla = skrotSolonegoHasla,
                Sol = tekstSoli
            };

            clientSalts.Add(clientSalt);

            return skrotSolonegoHasla ;
        }

        //Metoda umożliwiająca sprawdzenie hasła
        /*
        public static bool Sprawdz(string haslo)
        {
            SHA256 sha = SHA256.Create();

            string soloneHaslo = haslo + uzytkownik.Sol;

            string skrotSolonegoHasla = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(soloneHaslo)));

            return uzytkownik.SkrotSolonegoHasla == skrotSolonegoHasla;
        }
        */

    }

}