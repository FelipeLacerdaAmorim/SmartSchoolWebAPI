using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.V1.Dtos;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository Repo;
        private readonly IMapper Mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            Repo = repo;
            Mapper = mapper;
        }

        // GET: api/Professor
        [HttpGet]
        public IActionResult Get()
        {
            var professores = Repo.GetAllProfessores(true);

            return Ok(Mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }


        // GET api/Professor/byId/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var prof = Repo.GetProfessorById(id, true);
            if (prof == null) return BadRequest("Professor não encontrado!");

            var professorDto = Mapper.Map<ProfessorDto>(prof);
            return Ok(professorDto);
        }

        // POST api/Professor
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = Mapper.Map<Professor>(model);

            Repo.Add(professor);
            if (Repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", Mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não encontrado!!!");
        }

        // PUT api/Professor/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = Repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            Mapper.Map(model, professor);

            Repo.Update(professor);
            if (Repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", Mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não atualizado!!!");
        }

        // PATCH api/<ProfessorController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = Repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            Mapper.Map(model, professor);

            Repo.Update(professor);
            if (Repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", Mapper.Map<ProfessorDto>(professor));
            }

            return BadRequest("Professor não atualizado!!!");
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = Repo.GetProfessorById(id);
            if (prof == null) return BadRequest("Professor não encontrado");
            Repo.Delete(prof);
            if (Repo.SaveChanges())
            {
                return Ok("Professor deletado");
            }
            return Ok();
        }
    }
}
