using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        //Instanciando nosso contexto na classe e atribuindo um nome ao mesmo
        private CodenationContext _context;

        //Construtor
        public UserService(CodenationContext context)
        {
            _context = context;
        }


        public IList<User> FindByAccelerationName(string name)
        {
            //Selecionamos Accelerations pois é a classe que retorna o Name
            return _context.Accelerations.
                Where(x => x.Name == name).
                SelectMany(x => x.Candidates). //Selecionanndo a coleção de Candidatos dentro de Accelerations
                Select(x => x.User). //Selecionando nossa FK dentro de Candidate
                Distinct(). //Ignorando registros iguais
                ToList(); //Listando
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            //Selecionamos Candidates pois é a navegação dentro de User que retorna o CompanyId
            return _context.Candidates.
                Where(x => x.CompanyId == companyId). //Condição de igualdade
                Select(x => x.User).  //Selecionando nossa FK dentro de Candidate
                Distinct(). //Ignorando registros iguais
                ToList(); //Listando
        }

        public User FindById(int id)
        {
            //O Find procura os ID's dentro de Accelerations
            return _context.Users.Find(id);
        }

        public User Save(User user)
        {
            //verificar se é para Add ou Update
            var estado = user.Id == 0 ? EntityState.Added : EntityState.Modified;

            //salvar esse estado no contexto
            _context.Entry(user).State = estado;

            //persistir esses dados - salvá-los
            _context.SaveChanges();

            //retorna o objeto
            return user;
        }
    }
}
