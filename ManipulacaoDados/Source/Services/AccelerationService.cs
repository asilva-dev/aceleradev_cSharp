using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        //Instanciando nosso contexto na classe e atribuindo um nome ao mesmo
        private CodenationContext _context;

        //Construtor 
        public AccelerationService(CodenationContext context)
        {
            _context = context;
        }


        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            //Selecionamos Candidates pois é a navegação dentro de Acceleration que retorna o CompanyId
            return _context.Candidates.
                Where(x => x.CompanyId == companyId). //condição de igualdade
                Select(x => x.Acceleration). //Selecionando nossa FK dentro de Candidates
                Distinct(). //Ignorando registros iguais
                ToList(); //Listando 
        }

        public Acceleration FindById(int id)
        {
            //O Find procura os ID's dentro de Accelerations
            return _context.Accelerations.Find(id);
        }

        public Acceleration Save(Acceleration acceleration)
        {
            //verificar se é para Add ou Update
            var estado = acceleration.Id == 0 ? EntityState.Added : EntityState.Modified;

            //salvar esse estado no contexto
            _context.Entry(acceleration).State = estado;

            //persistir esses dados - salvá-los
            _context.SaveChanges();

            //retorna o objeto
            return acceleration;
        }
    }
}
