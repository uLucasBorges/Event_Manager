using EventManager.Services.Interfaces;
using GestaoDeEventos.Models;
using GestaoDeEventos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/participantes")]
[ApiController]
public class ParticipanteController : ControllerBase
{
    private readonly IParticipanteService _participanteService;

    public ParticipanteController(IParticipanteService participanteService)
    {
        _participanteService = participanteService;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Participante>> ListarParticipantesPorEvento([FromQuery] int eventoId)
    {
        var participantes = _participanteService.ListarParticipantesPorEvento(eventoId);
        return Ok(participantes);
    }

    [HttpPost]
    public async Task<ActionResult> AdicionarParticipante([FromBody] Participante participante)
    {
        try
        {
            await _participanteService.AdicionarParticipante(participante.Nome, participante.Email, participante.EventoId);
            return Ok("Participante adicionado com sucesso!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> AtualizarParticipante(int id, [FromBody] Participante participante)
    {
        if (id != participante.Id)
            return BadRequest("Erro: ID do participante inconsistente.");

        try
        {
            await _participanteService.AtualizarParticipante(participante);
            return Ok("Participante atualizado com sucesso!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletarParticipante(int id)
    {
        try
        {
            await _participanteService.DeletarParticipante(id);
            return Ok("Participante removido com sucesso!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
