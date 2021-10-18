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
        private readonly IRepository Repo;

        public ProfessorController(IRepository repo)
        {
            Repo = repo;
        }

        // GET: api/Professor
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Repo.GetAllProfessores(true));
        }

        // GET api/Professor/byId/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var prof = Repo.GetProfessorById(id, true);
            if (prof == null) return BadRequest("Professor não encontrado!");
            return Ok(prof);
        }

        // POST api/Professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            Repo.Add(professor);
            Repo.SaveChanges();
            return Ok(professor);
        }

        // PUT api/Professor/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = Repo.GetProfessorById(id);
            if (prof == null) return BadRequest("Professor não encontrado");
            Repo.Update(professor);
            Repo.SaveChanges();
            return Ok(professor);
        }

        // PATCH api/<ProfessorController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = Repo.GetProfessorById(id);
            if (prof == null) return BadRequest("Professor não encontrado");
            Repo.Update(professor);
            Repo.SaveChanges();
            return Ok(professor);
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = Repo.GetProfessorById(id);
            if (prof == null) return BadRequest("Professor não encontrado");
            Repo.Delete(prof);
            Repo.SaveChanges();
            return Ok();
        }
    }
}
