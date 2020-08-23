using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        //Instanciando nosso contexto na classe e atribuindo um nome ao mesmo
        private CodenationContext _context;

        //Construtor
        public ChallengeService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            //Selecionamos Users pois é UserId
            return _context.Users.
                Where(x => x.Id == userId). //Condição de igualdade
                SelectMany(x => x.Candidates). //Selecionando a coleção de Candidatos 
                Where(x => x.AccelerationId == accelerationId). //Condição de Igualdade
                Select(x => x.Acceleration.Challenge). //Selecionando nossa FK dentro de Acceleration 
                Distinct(). //Ignorando registros duplicados
                ToList(); //Listando 
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            //verificar se é para Add ou Update
            var estado = challenge.Id == 0 ? EntityState.Added : EntityState.Modified;

            //salvar esse estado no contexto
            _context.Entry(challenge).State = estado;

            //persistir esses dados - salvá-los
            _context.SaveChanges();

            //retorna o objeto
            return challenge;
        }
    }
}