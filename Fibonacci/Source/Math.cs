using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Math
    {
        public static void Main(string[] args)
        { 
        
        }
            public List<int> Fibonacci()
        {   
            //Variáveis utilizadas no calculo
            int num1 = 0;
            int num2 = 1;
            int numFibonacci = 0;

            //criando a lista que recebera o fibonacci
            var list = new List<int>();

            //Adicionando as variaveis na lista do fibonacci
            list.Add(num1);
            list.Add(num2);

            //Controle de fluxo / contador que inicia com 0 e acrescenta no Fibonacci até o limite de 350
            for (int i = 0; numFibonacci < 350; i++)
            {
                numFibonacci = num1 + num2;
                num1 = num2;
                num2 = numFibonacci;
                if (numFibonacci < 350)
                {
                    list.Add(numFibonacci);
                }
            }
            //Repetição que exibe no console os numeros   
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            //retornando a lista
            return list;
            throw new NotImplementedException();
        }
        
        public bool IsFibonacci(int numberToTest)
        {   
            //Verificando se o numero do parametro esta na sequencia
            return Fibonacci().Contains(numberToTest);
        }
    }
}
