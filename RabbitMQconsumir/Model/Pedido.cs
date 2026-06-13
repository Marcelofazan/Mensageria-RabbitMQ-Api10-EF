using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQconsumir.Model
{
    public class Pedido
    {
        public int Id { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime DataDeCriacao { get; set; }

        // O Entity Framework precisa deste construtor vazio para funcionar
        private Pedido()
        {
        }
        
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
