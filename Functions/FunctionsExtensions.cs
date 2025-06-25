using System;

namespace Functions
{
    public static class FunctionsExtensions
    {
        public static string FormatCNPJCPF(this string CnpjCpf)
        {
            CnpjCpf = WithoutMask(CnpjCpf);

            if (CnpjCpf.Length > 11)
                return CnpjCpf = FormatCNPJ(CnpjCpf);
            else
                return CnpjCpf = FormatCPF(CnpjCpf);
        }

        public static string FormatCNPJ(string CNPJ)
        {
            return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatCPF(string CPF)
        {
            return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }

        public static string WithoutMask(string Codigo)
        {
            return Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
    }
}