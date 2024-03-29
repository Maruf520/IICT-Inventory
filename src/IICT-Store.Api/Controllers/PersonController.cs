﻿using IICT_Store.Dtos.PersonDtos;
using IICT_Store.Services.PersonServices;
using Microsoft.AspNetCore.Authorization;
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
    public class PersonController : BaseController
    {
        private readonly IPersonService personService;
        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }
        //[Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromForm]CreatePersonDto createPersonDto)
        {
            var person = await personService.CreatePerson(createPersonDto, GetuserId());
            return Ok(person);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var person = await personService.GetAllPerson();
            return Ok(person);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(long id)
        {
            var person = await personService.GetPersonById(id);
            return Ok(person);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(long id,[FromForm] CreatePersonDto createPersonDto)
        {
            var person = await personService.UpdatePerson(id, createPersonDto, GetuserId());
            return Ok(person);
        }
       // [Authorize(Roles = "User, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var person = await personService.DeletePerson(id);
            return Ok(person);
        }
    }
}
