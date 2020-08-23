using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        //Instanciando nosso contexto na classe e atribuindo um nome ao mesmo
        private CodenationContext _context;

        //Construtor
        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            //Selecionamos Candidates pois é a classe que retorna o acceleration 
            return _context.Candidates.
                Where(x => x.AccelerationId == accelerationId). //Condição de igualdade
                Select(x => x.User). 
                SelectMany(x => x.Submissions). //Selecionando a nossa coleção de Submissions da classe User
                Where(x => x.ChallengeId == challengeId). //Condição de igualdade
                Distinct(). //ignorando registros iguais
                ToList(); //Listando
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return _context.Submissions.
                Where(x => x.ChallengeId == challengeId).
                Max(x => x.Score); 
        }

        public Submission Save(Submission submission)
        {
            // ignorar change tracker
            var existe = _context.Submissions.Find(submission.UserId, submission.ChallengeId);

            if (existe == null)
                _context.Submissions.Add(submission);
            else
                existe.Score = submission.Score;

            //persistir os dados
            _context.SaveChanges();

            //retornar o objeto
            return submission;
        }
    }
}
