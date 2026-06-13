using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitMQprodutor.Model
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime DataDeCriacao { get; set; }

        public Pedido(int id, Usuario usuario, DateTime dataDeCriacao)
        {
            Id = id;
            Usuario = usuario;
            DataDeCriacao = dataDeCriacao;
        }

        public override string ToString()
            => $"Id do Pedido:{Id}," +
                   $" Usuário:{Usuario.Nome}," +
                   $" Data de Criação:{DataDeCriacao:dd/MM/yyyy}";

    }
}
