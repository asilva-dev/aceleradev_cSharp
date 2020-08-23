using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        //Instanciando nosso contexto na classe e atribuindo um nome ao mesmo
        private CodenationContext _context;

        //Construtor 
        public CandidateService(CodenationContext context)
        {
            _context = context;
        }


        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return _context.Candidates.
                Where(x => x.AccelerationId == accelerationId). //Condição de igualdade
                Distinct().//Ignorando registros duplicados
                ToList(); //Listando
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return _context.Candidates.
                Where(x => x.CompanyId == companyId). //Condição de igualdade
                Distinct(). //Ignorando registros duplicados
                ToList(); //Listando
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            //O Find procura os ID's dentro de Accelerations
            return _context.Candidates.Find(userId, accelerationId, companyId);
        }

        public Candidate Save(Candidate candidate)
        {
           
            // ignorar change tracker
            var existe = _context.Candidates.Find(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);

            if (existe == null)
                _context.Candidates.Add(candidate);
            else
                existe.Status = candidate.Status;

            //persistir os dados
            _context.SaveChanges();

            //retornar o objeto
            return candidate;

        }
    }
}
