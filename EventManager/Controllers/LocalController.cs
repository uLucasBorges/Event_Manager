using EventManager.Services.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/locais")]
[ApiController]
public class LocalController : ControllerBase
{
    private readonly ILocalService _localService;

    public LocalController(ILocalService localService)
    {
        _localService = localService;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Local>> ListarLocais()
    {
        var locais = _localService.ListarLocais();
        return Ok(locais);
    }

    [HttpPost]
    public async Task<ActionResult> CriarLocal([FromBody] Local local)
    {
        try
        {
            await _localService.CriarLocal(local);
            return Ok("Local criado com sucesso!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> AtualizarLocal(int id, [FromBody] Local local)
    {
        if (id != local.Id)
            return BadRequest("Erro: ID do local inconsistente.");

        try
        {
            await _localService.AtualizarLocal(local);
            return Ok("Local atualizado com sucesso!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletarLocal(int id)
    {
        try
        {
            await _localService.DeletarLocal(id);
            return Ok("Local removido com sucesso!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
