namespace GestaoDeEventos.Models
{
    public class Participante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int EventoId { get; set; } // Relacionamento com Evento
    }
}
