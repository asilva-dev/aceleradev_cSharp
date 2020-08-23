using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {
        private const string numeros = " 0123456789";
        private const string alfabeto = "abcdefghijklmnopqrstuvwxyz";
        private int qtdCasas = 3;

        public string Crypt(string message)
        {
            //retorno caso a mensagem seja vazia
           if (message == null)
            {
                throw new ArgumentNullException();
            }

           //declarando uma string que pode ter seu conteudo alterado
           StringBuilder msgFinal = new StringBuilder();
           
           //array que recebe o texto a ser criptografado
           char[] msg;
           //ToLower utilizado para converter para minusculo
           msg = message.ToLower().ToCharArray();

            foreach (char c in msg)
            {

                if (inStr(c, alfabeto))
                {
                    msgFinal.Append(correrCasa(c));
                }
                else if (inStr(c, numeros))
                {
                    msgFinal.Append(c);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            return msgFinal.ToString();          
        }
        public string Decrypt(string cryptedMessage)
        {   
            //retornando null caso cryptedMessage esteja vazia
            if (cryptedMessage == null)
            {
                throw new ArgumentNullException();
            }
              

            StringBuilder msgFinal = new StringBuilder();
            char[] msg;
            msg = cryptedMessage.ToLower().ToCharArray();
            
            foreach (char c in msg)
            {
                if (inStr(c, alfabeto))
                {
                    msgFinal.Append(VoltaCasas(c));
                }
                else if (inStr(c, numeros))
                {
                    msgFinal.Append(c);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            return msgFinal.ToString();
        }
    
        private char correrCasa(char c)
        {
            int vAscii = Convert.ToInt32(c) + qtdCasas;

            if (vAscii > Convert.ToInt32(alfabeto[alfabeto.Length-1]))
            {
                vAscii = vAscii - alfabeto.Length;
            }
            return Convert.ToChar(vAscii);
        }

        private char VoltaCasas(char c)
        {
            int vAscii = Convert.ToInt32(c) - qtdCasas;

            if (vAscii < Convert.ToInt32(alfabeto[0]))
            {
                vAscii = vAscii + alfabeto.Length;
            }
            return Convert.ToChar(vAscii);
        }

        private bool inStr(char pLetra, string pTexto)
        {          
           for (int i = 0; i < pTexto.Length; i++)
            {
                if (pLetra == pTexto[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}