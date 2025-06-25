using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Functions
{
    public static class Functions
    {
        public static string MD5Hash(string Password)
        {
            string Hash = "!@#$%¨&*123456789?" + Password;

            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(Hash);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static DiferencaData CalculaDiferencaDatas(DateTime d1, DateTime d2)
        {
            int[] diasDoMes = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            DateTime dataInicial;
            DateTime dataFinal;
            var model = new DiferencaData();

            int incremento;

            if (d1 > d2)
            {
                dataInicial = d2;
                dataFinal = d1;
            }
            else
            {
                dataInicial = d1;
                dataFinal = d2;
            }

            /// Calculo dos dias
            incremento = 0;

            if (dataInicial.Day > dataFinal.Day)
                incremento = diasDoMes[dataInicial.Month - 1];

            /// se for fevereiro
            /// se o dia for menor que o dia de  hoje
            if (incremento == -1)
            {
                if (DateTime.IsLeapYear(dataInicial.Year))
                    incremento = 29;// ano bissexto -> fevereiro contém 29 dias
                else
                    incremento = 28;
            }
            if (incremento != 0)
            {
                model.Dias = (dataFinal.Day + incremento) - dataInicial.Day;
                incremento = 1;
            }
            else
                model.Dias = dataFinal.Day - dataInicial.Day;

            ///calculo do mês
            if ((dataInicial.Month + incremento) > dataFinal.Month)
            {
                model.Meses = (dataFinal.Month + 12) - (dataInicial.Month + incremento);
                incremento = 1;
            }
            else
            {
                model.Meses = (dataFinal.Month) - (dataInicial.Month + incremento);
                incremento = 0;
            }

            /// calculo do ano
            model.Anos = dataFinal.Year - (dataInicial.Year + incremento);

            return model;
        }

        public static List<char> Alfabeto()
        {
            var alfabeto = new List<char>();

            char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
            foreach (var c in alphabet)
            {
                alfabeto.Add(c);
            }

            return alfabeto;
        }

        public static Dictionary<int, string> Meses()
        {
            Dictionary<int, string> meses = new Dictionary<int, string>();

            meses.Add(1, "Janeiro");
            meses.Add(2, "Fevereiro");
            meses.Add(3, "Março");
            meses.Add(4, "Abril");
            meses.Add(5, "Maio");
            meses.Add(6, "Junho");
            meses.Add(7, "Julho");
            meses.Add(8, "Agosto");
            meses.Add(9, "Setembro");
            meses.Add(10, "Outubro");
            meses.Add(11, "Novembro");
            meses.Add(12, "Dezembro");

            return meses;
        }

        public static byte[] GetArrayBytesCertificado(string caminho)
        {
            return File.ReadAllBytes(caminho);//exemplo @"C:\MENDES.pfx"
        }

        public static string FormatXml(string xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        public static int GetRandom()
        {
            var rand = new Random();
            return rand.Next(11111111, 99999999);
        }

        public static byte[] ConverteBase64ToByte(string arquivo)
        {
            return Convert.FromBase64String(arquivo);
        }

        public static DirectoryInfo TryGetDirectoryInfo(string currentPath = null)
        {
            return new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
        }

        public static decimal CalculoPorcentagem(decimal valor, decimal porcentagem)
        {
            return valor * (porcentagem / 100);
        }

        public static decimal CalculoSubtrairPorcentagem(decimal valor, decimal porcentagem)
        {
            var porc = valor * (porcentagem / 100);

            return valor - porc;
        }

        public static T XmlParaClasse<T>(string xml) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            byte[] byteArray = Encoding.ASCII.GetBytes(xml);

            MemoryStream stream = new MemoryStream(byteArray);

            try
            {
                return (T)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }

        public static T ArquivoXmlParaClasse<T>(string arquivo) where T : class
        {
            if (!File.Exists(arquivo))
            {
                throw new FileNotFoundException("Arquivo " + arquivo + " não encontrado.");
            }

            var keyNomeClasseEmUso = typeof(T).FullName;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var stream = new FileStream(arquivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                return (T)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }

        public static string Comprimentar()
        {
            var dataAtual = DateTime.UtcNow;

            if (dataAtual.Hour >= 6 && dataAtual.Hour <= 12)
                return "Bom dia";
            else if (dataAtual.Hour >= 12 && dataAtual.Hour < 18)
                return "Boa tarde";
            else
                return "Boa noite";
        }
    }

    public class DiferencaData
    {
        public int Anos { get; set; }
        public int Meses { get; set; }
        public int Dias { get; set; }
    }
}
