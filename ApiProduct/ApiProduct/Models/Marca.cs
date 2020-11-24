using System;
using System.Collections.Generic;

namespace ApiProduct.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Produto = new HashSet<Produto>();
        }

        public int CdMarca { get; set; }
        public string DsMarca { get; set; }

        public ICollection<Produto> Produto { get; set; }

        public static bool IsDsMarcaValida(string dsMarca)
        {
            if (dsMarca != null && dsMarca.Trim().Length > 3 && dsMarca.Trim().Length < 60)
            {
                return true;
            }
            return false;
        }
    }
}
