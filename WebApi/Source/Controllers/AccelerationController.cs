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
    public class AccelerationController : ControllerBase
    {
        //importando a interface da service
        private IAccelerationService _accelerationService;
        private readonly IMapper _mapper;

        //construtor que recebe a service e atribui a uma variável
        public AccelerationController(IAccelerationService accelerationService, IMapper mapper)
        {
            _accelerationService = accelerationService;
            _mapper = mapper;
        }

        // GET: api/acceleration/{id}
        [HttpGet("{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
        {
            var acceleration = _accelerationService.FindById(id);

            if (acceleration != null)
            {
                // Substituir mapeamento de objeto manual por mapeamento com AutoMapper

                return Ok(_mapper.Map<AccelerationDTO>(acceleration));
            }
            else
                return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if (companyId.HasValue)
            {
                var acceleration = _accelerationService.FindByCompanyId(companyId.Value).ToList();
                var retorno = _mapper.Map<List<AccelerationDTO>>(acceleration);

                return Ok(retorno);
            }
            else
                return NoContent();
        }

        // POST: api/Acceleration
        [HttpPost]
        public ActionResult<AccelerationDTO> Post([FromBody]AccelerationDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var acceleration = _mapper.Map<Models.Acceleration>(value);
            //Salvar
            var retorno = _accelerationService.Save(acceleration);
            //mapear Model para Dto
            return Ok(_mapper.Map<AccelerationDTO>(retorno));
        }
    }
}
