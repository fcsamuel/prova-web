using Business.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.IR
{
    public class Helper
    {
        public static decimal CalculaPrecoVenda(decimal valor, float porcentagemLucro)
        {
            return valor + (valor * (decimal)porcentagemLucro / 100);
        }

        public static bool IsCadastroCompleto(Produto produto)
        {
            if (!String.IsNullOrEmpty(produto.DsProduto) &&
                produto.CdMarca.HasValue &&
                !String.IsNullOrEmpty(produto.DsCategoriaProduto) &&
                produto.NrValor.HasValue)
            {
                return true;
            } else
            {
                return false;
            }

        }

        public static decimal AplicaValorDesconto(decimal valorProduto, decimal valorDesconto)
        {
            if (DescontoValorIsValido(valorProduto, valorDesconto))
            {
                return valorProduto - (valorDesconto);
            } else
            {
                return 0;
            }
        }

        public static bool DescontoValorIsValido(decimal valorProduto, decimal valorDesconto)
        {
            if (valorDesconto > 0 &&
                valorDesconto < valorProduto &&
                valorProduto > 0)
            {
                return true;
            }
            return false;
        }
        public static bool DescontoPorcentIsValido(decimal valorProduto, float porcentagem)
        {
            if (porcentagem > 0 &&
                porcentagem < 100 &&
                valorProduto > 0)
            {
                return true;
            }
            return false;
        }

        public static decimal AplicaPorcentDesconto(decimal vlProduto, float vlPorcentagem)
        {
            if (DescontoPorcentIsValido(vlProduto, vlPorcentagem))
            {
                var desconto = vlProduto - (vlProduto * (decimal)vlPorcentagem / 100);
                return vlProduto - desconto;
            }
            else return vlProduto;
        }

        public static bool validaPrecoCusto(decimal vlProduto, decimal vlCusto)
        {
            if (vlCusto < vlProduto)
            {
                return true;
            }
            return false;
        }

    }
}
