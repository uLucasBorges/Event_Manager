namespace EventManager_Web.Models
{
    public class Local
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public Local()
        {


        }

        public Local(int id, string nome, string endereco)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
        }
    }

}
