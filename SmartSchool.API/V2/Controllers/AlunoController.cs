using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.V2.Dtos;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.V2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class AlunoController : ControllerBase
    {
        public IRepository Repo { get; }
        public IMapper Mapper { get; }

        public AlunoController(IRepository repo, IMapper mapper)
        {
            Repo = repo;
            Mapper = mapper;
        }
        /// <summary>
        /// Método responsável por retornar todos os alunos.
        /// </summary>
        /// <returns></returns>
        // GET: api/aluno
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = Repo.GetAllAlunos(true);
            return Ok(Mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        /// <summary>
        /// Método para exibir no Postman o formato padrão do AlunoRegistrarDto(Entrada).
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }

        /// <summary>
        /// Método que retorna o aluno que possui a Id passada.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = Repo.GetAlunoById(id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado!!!");

            var alunoDto = Mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        // POST api/aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = Mapper.Map<Aluno>(model);

            Repo.Add(aluno);
            if (Repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", Mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado");        
        }

        // PUT api/aluno/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = Repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            Mapper.Map(model, aluno);

            Repo.Update(aluno);
            if (Repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", Mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        // PATCH api/aluno/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = Repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            Mapper.Map(model, aluno);

            Repo.Update(aluno);
            if (Repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", Mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não atualizado");
        }

        // DELETE api/aluno/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = Repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");
            
            Repo.Delete(aluno);
            if (Repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }

            return BadRequest("Aluno não deletado");
        }
    }
}
