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
    public class CompanyController : ControllerBase
    {
        //importando a interface da service
        private ICompanyService _companyService;
        private readonly IMapper _mapper;

        //construtor que recebe a service e atribui a uma variável
    
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET: api/company
        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            var company = _companyService.FindById(id);

            if (company != null)
            {
                // Substituir mapeamento de objeto manual por mapeamento com AutoMapper

                return Ok(_mapper.Map<CompanyDTO>(company));
            }
            else
                return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId.HasValue && userId == null)
            {
                var acceleration = _companyService.FindByAccelerationId(accelerationId.Value).ToList();
                var retorno = _mapper.Map<List<CompanyDTO>>(acceleration);

                return Ok(retorno);
            }

            else if (userId.HasValue && accelerationId == null)
            {
                var user = _companyService.FindByUserId(userId.Value).ToList();
                var retorno = _mapper.Map<List<CompanyDTO>>(user);
                return Ok(retorno);
            }

            else
                return NoContent();
        }

        // POST: api/company
        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody]CompanyDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var candidate = _mapper.Map<Company>(value);
            //Salvar
            var retorno = _companyService.Save(candidate);
            //mapear Model para Dto
            return Ok(_mapper.Map<CompanyDTO>(retorno));
        }
    }
}
