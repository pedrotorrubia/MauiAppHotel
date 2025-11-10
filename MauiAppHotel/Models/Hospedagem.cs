using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppHotel.Models
{
    public class Hospedagem
    {
        public Quarto? QuartoSelecionado { get; set; }
        public int QtdAdultos { get; set; }
        public int QtdCriancas { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }

        public int EstadiaDias { get; set; }
        public double ValorTotal { get; set; }
    }
}
