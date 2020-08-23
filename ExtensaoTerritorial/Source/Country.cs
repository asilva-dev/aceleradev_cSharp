using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Country
    { 
        public State[] Top10StatesByArea()
        {
           string[,] Estados = {
                {"AC","Acre","164123.040"},
                { "AL","Alagoas","27778.506"},
                { "AP","Amapá","142828.521"},
                { "AM","Amazonas","1559159.148"},
                { "BA","Bahia","564733.177"},
                { "CE","Ceara","148920.472"},
                { "DF","Distrito Federal","5779.999"},
                { "ES","Espírito Santo","46095.583"},
                { "GO","Góias","340111.783"},
                { "MA","Maranhão","331937.450"},
                { "MT","Mato Grosso","903366.192"},
                { "MS","Mato Grosso do Sul","357145.532"},
                { "MG","Minas Gerais","586522.122"},
                { "PA","Pará","1247954.666"},
                { "PB","Paraiba","56585.000"},
                { "PR","Paraná","199307.922"},
                { "PE","Pernambuco","98311.616"}, 
                { "PI","Piaui","251577.738"},
                { "RJ","Rio de Janeiro","43780.172"}, 
                { "RN","Rio Grande do Norte","52811.047"},
                { "RS","Rio Grande do Sul","281730.223"},
                { "RO","Rondônia","237590.547"},
                { "RR","Roraima","224300.506"}, 
                { "SC","Santa Catarina","95736.165"},
                { "SP","São Paulo","248222.362"},
                { "SE","Sergipe","21915.116"},
                { "TO","Tocantins","277720.520"},
           };
            int estado = (Estados.Length / Estados.GetLength(1));
            //colocando os estados do array do menor para o maior
            for (int a = 0; a < estado; a++)
            {
                for (int b = a + 1; b < estado; b++)
                {
                    if (Convert.ToDouble(Estados[b, Estados.Rank]) > (Convert.ToDouble(Estados[a, Estados.Rank])))
                    {
                        for (int c = 0; c <= Estados.Rank; c++)
                        {
                            string[,] aux = { { "", "", "" } };
                            aux[0, c] = Estados[a, c];
                            Estados[a, c] = Estados[b, c];
                            Estados[b, c] = aux[0, c];
                        }
                        Convert.ToString(Estados);
                    }
                }
            }
            State[] ArrayEstados = {
                new State(Estados[0, 1], Estados[0, 0]),
                new State(Estados[1, 1], Estados[1, 0]),
                new State(Estados[2, 1], Estados[2, 0]),
                new State(Estados[3, 1], Estados[3, 0]),
                new State(Estados[4, 1], Estados[4, 0]),
                new State(Estados[5, 1], Estados[5, 0]),
                new State(Estados[6, 1], Estados[6, 0]),
                new State(Estados[7, 1], Estados[7, 0]),
                new State(Estados[8, 1], Estados[8, 0]),
                new State(Estados[9, 1], Estados[9, 0])
            };
            return ArrayEstados;
        }
    }
}
