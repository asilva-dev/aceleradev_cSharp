using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        //importando a interface da service
        private ICandidateService _candidateService;
        private readonly IMapper _mapper;

        //construtor que recebe a service e atribui a uma variável
        public CandidateController(ICandidateService candidateService, IMapper mapper)
        {
            _candidateService = candidateService;
            _mapper = mapper;
        }

        // GET: api/candidate
        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {
            var candidate = _candidateService.FindById(userId, accelerationId, companyId);

            if (candidate != null)
            {
                // Substituir mapeamento de objeto manual por mapeamento com AutoMapper

                return Ok(_mapper.Map<CandidateDTO>(candidate));
            }
            else
                return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? accelerationId = null, int? companyId = null)
        {
            if (companyId.HasValue && accelerationId == null)
            {
                var company = _candidateService.FindByCompanyId(companyId.Value).ToList();
                var retorno = _mapper.Map<List<CandidateDTO>>(company);

                return Ok(retorno);
            }
            else if (accelerationId.HasValue && companyId == null)
            {
                var acceleration = _candidateService.FindByAccelerationId(accelerationId.Value).ToList();
                var retorno = _mapper.Map<List<CandidateDTO>>(acceleration);

                return Ok(retorno);
            }
            else
                return NoContent();
        }

        // POST: api/candidate
        [HttpPost]
        public ActionResult<CandidateDTO> Post([FromBody]CandidateDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var candidate = _mapper.Map<Candidate>(value);
            //Salvar
            var retorno = _candidateService.Save(candidate);
            //mapear Model para Dto
            return Ok(_mapper.Map<CandidateDTO>(retorno));
        }
    }
}
