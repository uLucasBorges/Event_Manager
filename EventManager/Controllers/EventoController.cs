using EventManager.Services.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/eventos")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventoController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Evento>> ListarEventos()
    {
        var resultado = _eventoService.ListarEventos();
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public ActionResult<Evento> ObterEventoPorId(int id)
    {
        var evento = _eventoService.ObterEventoPorId(id);
        if (evento == null)
            return NotFound("Evento não encontrado.");

        return Ok(evento);
    }

    [HttpPost]
    public async Task<IActionResult> CriarEvento([FromBody] Evento evento)
    {
        await _eventoService.CriarEvento(evento);
        //if (resultado.StartsWith("Erro"))
        //    return BadRequest(resultado);

        return Ok();
    }


    [HttpPost("atualizar")]
    public async Task<IActionResult> AtualizarEvento([FromBody] Evento evento)
    {
        await _eventoService.AtualizarEvento(evento);
        //if (resultado.StartsWith("Erro"))
        //    return BadRequest(resultado);

        return Ok();
    }

    [HttpPost("deletar/{eventoId}")]
    public async Task<IActionResult> DeletarEvento([FromRoute] int eventoId)
    {
         await _eventoService.DeletarEvento(eventoId);

        return Ok();
    }

}
