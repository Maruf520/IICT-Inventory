using IICT_Store.Dtos.PersonDtos;
using IICT_Store.Services.PersonServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IICT_Store.Api.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromForm]CreatePersonDto createPersonDto)
        {
            var person = await personService.CreatePerson(createPersonDto);
            return Ok(person);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var person = await personService.GetAllPerson();
            return Ok(person);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(long id)
        {
            var person = await personService.GetPersonById(id);
            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(long id, CreatePersonDto createPersonDto)
        {
            var person = await personService.UpdatePerson(id, createPersonDto);
            return Ok(person);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var person = await personService.DeletePerson(id);
            return Ok(person);
        }
    }
}
