using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Exceptions;
using Source.Classes;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        //Instanciando na lista as duas classes Time e Jogadores em uma lista do tipo Dictionary
        private Dictionary<long, Time> times;
        private Dictionary<long, Jogadores> jogadores;

        public SoccerTeamsManager()
        {
            times = new Dictionary<long, Time>();
            jogadores = new Dictionary<long, Jogadores>();
        }

        //Incluindo Time
        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {   
            //verificando se ja existe algum time com o mesmo ID
            if(times.ContainsKey(id)){
                throw new UniqueIdentifierException();
            }

            //variavel que instancia o time e os campos da mesma
            var Time = new Time()
            {
                Id                    = id,
                Name                  = name,
                DataCriacao           = createDate,
                CorUniformePrincipal  = mainShirtColor,
                CorUniformeSecundario = secondaryShirtColor
            };

            //Add time novo
            times.Add(id, Time);
        }

        //Incluindo Jogador
        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            //Verificando se não existe o time e retornando a excetion
            if(!times.ContainsKey(teamId))
                throw new TeamNotFoundException(); 
            //Verificando se existe o ID e retornando a excetion
            if(jogadores.ContainsKey(id))
                throw new UniqueIdentifierException();
              

            //Variavel que instancia os jogadores e atribui seus campos
            var Jogador = new Jogadores()
            {
                Id = id,
                TeamId = teamId,
                Name = name,
                BirthDate = birthDate,
                SkillLevel = skillLevel,
                Salary = salary
            };  

            //Adicionando novo jogador
            jogadores.Add(id, Jogador);
        }

        public void SetCaptain(long playerId)
        {
            //Chamando as nossas classes externas
            Jogadores jogador;            
            
            //Verificando se não existe o jogados com o ID e retornando a exception
            if (!jogadores.TryGetValue(playerId, out jogador))
                throw new PlayerNotFoundException();
            
            //Atribuindo um capitão pelo TeamId na variavel jogador do método Addplayer
            times[jogador.TeamId].Capitao = playerId;
            
        }

        public long GetTeamCaptain(long teamId)
        {
            //Instanciando a classe time
            Time time;
            
            //Verificando se o time não existe
            if(!times.TryGetValue(teamId, out time))
                throw new TeamNotFoundException();
            //Verificando se o time não tem um capitão
            if(!time.Capitao.HasValue)
                throw new CaptainNotFoundException();
            return time.Capitao.Value;
        }

        public string GetPlayerName(long playerId)
        {
            Jogadores jogador;

            if(!jogadores.TryGetValue(playerId, out jogador))
                throw new PlayerNotFoundException();

            return jogador.Name;
        }

        public string GetTeamName(long teamId)
        {
            Time time;

            if(!times.TryGetValue(teamId, out time))
                throw new TeamNotFoundException();

            return time.Name;
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            Time time;

            IEnumerable<Jogadores> jogadoresTime;
            jogadoresTime = jogadores.Values.Where(x => x.TeamId == teamId);
           
            if(!times.TryGetValue(teamId, out time))
                throw new TeamNotFoundException();
            
            List<long> listaJogadores = new List<long>(); 

            foreach(Jogadores jogadores in jogadoresTime)
            {
                listaJogadores.Add(jogadores.Id);
            }
            //Ordenando a lista
            listaJogadores.Sort();

            return listaJogadores;
        }

        public long GetBestTeamPlayer(long teamId)
        {
            Time time;

            if(!times.TryGetValue(teamId, out time))
                throw new TeamNotFoundException();

           int melhorSkillLevel = jogadores.Values
                .Where(x => x.TeamId == teamId)
                .Max(x => x.SkillLevel);

           return jogadores.Values.Where(x => x.TeamId == teamId)
                .Where(x => x.SkillLevel == melhorSkillLevel)
                .Min(x => x.Id); 
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            Time time;

            if(!times.TryGetValue(teamId, out time))
                throw new TeamNotFoundException();

            DateTime maisVelho = jogadores.Values
                .Where(x => x.TeamId == teamId)
                .Min(x => x.BirthDate);

            return jogadores.Values
                .Where(x => x.TeamId == teamId)
                .Where(x => x.BirthDate == maisVelho)
                .Min(x => x.Id);
        }

        public List<long> GetTeams()
        {
            List<long> buscarTimes = new List<long>();

            foreach (Time time in times.Values)
            {
                buscarTimes.Add(time.Id);
            }

            buscarTimes.Sort();

            return buscarTimes;
            
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            Time time;

            if(!times.TryGetValue(teamId, out time))
                throw new TeamNotFoundException();
                        
            decimal maiorSalario = jogadores.Values
                .Where(x => x.TeamId == teamId)
                .Max(x => x.Salary);
        
            return jogadores.Values
                .Where(x => x.TeamId == teamId)
                .Where(x => x.Salary == maiorSalario)
                .Min(x => x.Id);
        
        }

        public decimal GetPlayerSalary(long playerId)
        {
            Jogadores jogador;
            
            if(!jogadores.TryGetValue(playerId, out jogador))
            throw new PlayerNotFoundException();

            return jogador.Salary;            
        }

        public List<long> GetTopPlayers(int top)
        {
            IEnumerable<Jogadores> topJogadores = jogadores.Values.OrderByDescending(x => x.SkillLevel).Take(top);
            List<long> listaJogadores = new List<long>();

            foreach (Jogadores jogador in topJogadores)
            {
                listaJogadores.Add(jogador.Id);
            }

            return listaJogadores;
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            Time time;

            if(!times.TryGetValue(teamId, out time))
                throw new TeamNotFoundException();
            
            string camPrincipal  = time.CorUniformePrincipal;
            string camSecundaria = time.CorUniformeSecundario;

            if(!times.TryGetValue(visitorTeamId, out time))
                throw new TeamNotFoundException();

            string advPrincipal  = time.CorUniformePrincipal;
            string advSecundaria = time.CorUniformeSecundario;

            if (camPrincipal == advPrincipal) return advSecundaria;

            return advPrincipal;
         }

    }
}
