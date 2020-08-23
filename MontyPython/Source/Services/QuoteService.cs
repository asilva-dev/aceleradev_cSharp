using System;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class QuoteService : IQuoteService
    {
        private ScriptsContext _context;
        private IRandomService _randomService;

        public QuoteService(ScriptsContext context, IRandomService randomService)
        {
            this._context = context;
            this._randomService = randomService;
        }

        public Quote GetAnyQuote()
        {
            //lista inteira de Produtos
            var query = _context.Quotes.ToList();

            // se não retronar valor na lista não retorna valor na requisição
            if (query.Count == 0)
                return null;

            // a partir do tamanho da lista, retorna um valor aleatório que utilizaremos de index
            var RamdomIndex = _randomService.RandomInteger(query.Count);

            // o metodo skip ignora a lista até o index passado como argumento
            var retorno = query
                .Where(x => x.Actor != null)
                .Skip(RamdomIndex).FirstOrDefault();

            //retorno produto aleatorio
            if (retorno != null)
            {
                return retorno;
            }
            return null;
        }

        public Quote GetAnyQuote(string actor)
        {
            var query = _context.Quotes.ToList();

            if (query.Count == 0)
                return null;

            int aleatorio = _randomService.RandomInteger(query.Count());

            var result = query
                .Where(x => x.Actor == actor)
                .Skip(aleatorio)
                .FirstOrDefault();

            if (result != null)
                return result;
            return null;
        }
    }
}