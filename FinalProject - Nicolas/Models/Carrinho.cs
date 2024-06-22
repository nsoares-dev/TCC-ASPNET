using System.ComponentModel;

namespace FinalProject___Nicolas.Models
{
    public class Carrinho
    {
        [DisplayName("Carrinho")]
        public int CarrinhoId { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set;}
        public ICollection<ItensPedido>? ItensPedidos { get; set; }
        public double TotalCarrinho { get; set; }
    }
}
