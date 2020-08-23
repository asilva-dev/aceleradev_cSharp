using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        //importando a interface da service
        private IChallengeService _challengeService;
        private readonly IMapper _mapper;

        //construtor que recebe a service e atribui a uma variável
        public ChallengeController(IChallengeService challengeService, IMapper mapper)
        {
            _challengeService = challengeService;
            _mapper = mapper;
        }

        // GET: api/challenge
        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId.HasValue && userId.HasValue)
            {
                var challenge = _challengeService.FindByAccelerationIdAndUserId(accelerationId.Value, userId.Value).ToList();
                var retorno = _mapper.Map<List<ChallengeDTO>>(challenge);

                return Ok(retorno);
            }

            else
                return NoContent();
        }

        // POST: api/challenge
        [HttpPost]
        public ActionResult<ChallengeDTO> Post([FromBody]ChallengeDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var challenge = _mapper.Map<Models.Challenge>(value);
            //Salvar
            var retorno = _challengeService.Save(challenge);
            //mapear Model para Dto
            return Ok(_mapper.Map<ChallengeDTO>(retorno));
        }
    }
}
