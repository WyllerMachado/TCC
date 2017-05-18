using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassLogger.Models
{
    public class Codigo
    {
        public int CodigoId { get; set; }
        public int Code { get; private set; }

        [Required]
        public DateTime DataExpiracao { get; set; }

        [Required]
        public TimeSpan IntervaloRenovacao { get; set; }

        public bool Expirado { get; set; }

        // Construtor padrão
        public Codigo()
        {
            GerarCodigo();
        }

        // Gera um código aleatório de 4 dig. entre 1000 e 9999
        public void GerarCodigo()
        {
            Random generator = new Random();
            Code = generator.Next(1000, 9999);
        }

        // Renova o código
        public void RenovarCodigo()
        {
            if (Expirado) return;

            GerarCodigo();

            if ((DateTime.Now + IntervaloRenovacao) < DataExpiracao)
            {
                // Agendar próxima renovação
            }
        }
    }
}