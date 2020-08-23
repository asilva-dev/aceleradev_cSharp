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
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService service, IMapper mapper)
        {
            _userService = service;
            _mapper = mapper;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {
            if (accelerationName != null && companyId == null)
            {
                var acceleration = _userService.FindByAccelerationName(accelerationName).ToList();
                var retorno = _mapper.Map<List<UserDTO>>(acceleration);

                return Ok(retorno);
            }

            else if (companyId.HasValue && accelerationName == null)
            {
                // mesma coisa do de cima, mas de outra forma
                return Ok(_userService.FindByCompanyId(companyId.Value).
                    Select(x => _mapper.Map<UserDTO>(x)).
                    ToList());
            }

            else
                return NoContent();
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            var user = _userService.FindById(id);

            if (user != null)
            {
                var retorno = _mapper.Map<UserDTO>(user);

                return Ok(retorno);
            }

            else
                return NotFound();
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(value);
            var retorno = _userService.Save(user);

            return Ok(_mapper.Map<UserDTO>(retorno));
        }   
     
    }
}
