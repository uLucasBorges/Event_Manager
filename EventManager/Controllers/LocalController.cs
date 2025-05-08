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

    [HttpGet("{id}")]
    public ActionResult<Evento> ObterEventoPorId(int id)
    {
        var evento = _localService.ObterLocalPorId(id);

        if (evento == null)
            return NotFound("Local não encontrado.");

        return Ok(evento);
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

    [HttpPost("atualizar")]
    public async Task<IActionResult> AtualizarEvento([FromBody] Local local)
    {
        await _localService.AtualizarLocal(local);
        //if (resultado.StartsWith("Erro"))
        //    return BadRequest(resultado);

        return Ok();
    }

    [HttpPost("deletar/{localId}")]
    public async Task<IActionResult> DeletarEvento([FromRoute] int localId)
    {
        await _localService.DeletarLocal(localId);

        return Ok();
    }


}
