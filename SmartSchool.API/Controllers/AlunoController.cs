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
    public class AlunoController : ControllerBase
    {
        public SmartContext Context { get; }

        public AlunoController(SmartContext context)
        {
            Context = context;
        }

        // GET: api/aluno
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Context.Alunos);
        }

        // GET api/aluno/id
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = Context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("Aluno não encontrado!!!");

            return Ok(aluno);
        }

        // GET api/aluno/byName?nome=Nome&sobrenome=Sobrenome
        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if (aluno == null) return BadRequest("Aluno não encontrado!!!");

            return Ok(aluno);
        }

        // POST api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            Context.Add(aluno);
            Context.SaveChanges();
            return Ok(aluno);
        }

        // PUT api/aluno/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = Context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado");

            Context.Update(aluno);
            Context.SaveChanges();
            return Ok(aluno);
        }

        // PATCH api/aluno/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = Context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado");

            Context.Update(aluno);
            Context.SaveChanges();
            return Ok(aluno);
        }

        // DELETE api/aluno/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = Context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            Context.Remove(aluno);
            Context.SaveChanges();
            return Ok();
        }
    }
}
