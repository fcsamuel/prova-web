using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduct.Models
{
    public class ImagemProduto
    {
        public int CdImagem { get; set; }
        public int DsUrl { get; set; }
        public int CdProduto { get; set; }
        
        public Produto CdProdutoNavigation { get; set; }
    }
}
