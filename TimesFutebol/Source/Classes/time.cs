using System;

namespace Source.Classes
{
	public class Time
	{
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DataCriacao { get; set; }
        public string CorUniformePrincipal { get; set; }
        public string CorUniformeSecundario { get; set; }
        public long? Capitao { get; set; }

    }

}
