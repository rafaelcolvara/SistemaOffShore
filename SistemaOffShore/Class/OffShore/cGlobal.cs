using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SistemaOffShore.Class
{
    public static class cGlobal
    {
        #region VARIAVEIS GLOBAL
        public static string query;
        public static int existline;
        public static string userlogado;
        public static int userSetor;
        public static int iduserlogado;
        public static bool editando;
        public static List<string> caminho_arq = new List<string>();
        public static List<string> ar_query = new List<string>();
        #endregion
        
        #region CALCULA E RETORNA O TAMANHO DO ARQUIVO
        public static string TamanhoAmigavel(long bytes)
        {
            if (bytes < 0) throw new ArgumentException("bytes");

            double humano;
            string sufixo;

            if (bytes >= 1152921504606846976L) // Exabyte (1024^6)
            {
                humano = bytes >> 50;
                sufixo = "EB";
            }
            else if (bytes >= 1125899906842624L) // Petabyte (1024^5)
            {
                humano = bytes >> 40;
                sufixo = "PB";
            }
            else if (bytes >= 1099511627776L) // Terabyte (1024^4)
            {
                humano = bytes >> 30;
                sufixo = "TB";
            }
            else if (bytes >= 1073741824) // Gigabyte (1024^3)
            {
                humano = bytes >> 20;
                sufixo = "GB";
            }
            else if (bytes >= 1048576) // Megabyte (1024^2)
            {
                humano = bytes >> 10;
                sufixo = "MB";
            }
            else if (bytes >= 1024) // Kilobyte (1024^1)
            {
                humano = bytes;
                sufixo = "KB";
            }
            else return bytes.ToString("0 B"); // Byte

            humano /= 1024;
            return humano.ToString("0.## ") + sufixo;
        }
        #endregion

        #region VERIFICA SE O APLICATIVO JÁ ESTÁ SENDO EXECUTADO
        public static bool VerificaProgramaEmExecucao()
        {
            return Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1;
        }
        #endregion
    }
}
