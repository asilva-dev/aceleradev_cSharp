using System.Collections.Generic;

using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        //Instanciando nosso contexto na classe e atribuindo um nome ao mesmo
        private CodenationContext _context;

        //Construtor
        public CompanyService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            //Selecionamos Candidates pois é a navegação dentro de Company que retorna o accelerationId
            return _context.Candidates.
                Where(x => x.AccelerationId == accelerationId). //Condição de igualdade
                Select(x => x.Company). //Selecionando nossa FK dentro de Candidates
                Distinct(). //Ignorando registros iguais
                ToList(); //Listando
        } 

        public Company FindById(int id)
        {
            //O Find procura os ID's dentro de Accelerations
            return _context.Companies.Find(id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            //Selecionamos Candidates pois é a navegação dentro de Company que retorna o userId
            return _context.Candidates.
                Where(x => x.UserId == userId). //Condição de igualdade
                Select(x => x.Company). //Selecionando nossa FK dentro de Candidates
                Distinct(). //Ignorando registros iguais
                ToList(); //Listando
        }

        public Company Save(Company company)
        {
            //verificar se é para Add ou Update
            var estado = company.Id == 0 ? EntityState.Added : EntityState.Modified;

            //salvar esse estado no contexto
            _context.Entry(company).State = estado;

            //persistir esses dados - salvá-los
            _context.SaveChanges();

            //retorna o objeto
            return company;
        }
    }
}