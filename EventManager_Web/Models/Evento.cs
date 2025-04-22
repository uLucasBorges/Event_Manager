namespace EventManager_Web.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public int LocalId { get; set; } // Relacionamento com Locais
        public int CapacidadeMaxima { get; set; }
        public string imagemURL { get; set; }
        public string Descricao { get; set; }
        public string Feedback { get; set; }


        public Evento()
        {

        }

        public Evento(int id, string nome, DateTime data, int localId, int capacidadeMaxima, string imagemURL, string descricao, string feedback)
        {
            Id = id;
            Nome = nome;
            Data = data;
            LocalId = localId;
            CapacidadeMaxima = capacidadeMaxima;
            this.imagemURL = imagemURL;
            Descricao = descricao;
            Feedback = feedback;
        }
    }
}
