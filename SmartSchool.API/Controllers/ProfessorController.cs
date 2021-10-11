using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        public SmartContext Context { get; }

        public ProfessorController(SmartContext context)
        {
            Context = context;
        }

        // GET: api/Professor
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Context.Professores);
        }

        // GET api/Professor/byId/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var prof = Context.Professores.FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado!");
            return Ok(prof);
        }

        // GET api/Professor/byName?nome=Paulo
        [HttpGet("{byName}")]
        public IActionResult GetByName(string nome)
        {
            var prof = Context.Professores.FirstOrDefault(p => p.Nome.Contains(nome));
            if (prof == null) return BadRequest("Não encontrado!");
            return Ok(prof);
        }

        // POST api/Professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {

            Context.Add(professor);
            Context.SaveChanges();
            return Ok(professor);
        }

        // PUT api/Professor/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = Context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado");
            Context.Update(professor);
            Context.SaveChanges();
            return Ok(professor);
        }

        // PATCH api/<ProfessorController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = Context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado");
            Context.Update(professor);
            Context.SaveChanges();
            return Ok(professor);
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = Context.Professores.FirstOrDefault(p => p.Id == id);
            if (prof == null) return BadRequest("Professor não encontrado");
            Context.Remove(prof);
            Context.SaveChanges();
            return Ok();
        }
    }
}
