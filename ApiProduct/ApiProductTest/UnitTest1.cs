using Business.Domain;
using ApiProduct.Services;
using Business.Domain;
using Business.IR;
using Newtonsoft.Json;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Net.Http;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Teste_Soma()
        {
            Assert.GreaterOrEqual(1, 1, "Valore diferentes");
        }

        [Test]
        public void Teste_Multiplicacao()
        {
            //Processamento preparatório para os testes 
            Assert.AreEqual(MultiplicaRegistros(58, 126), 7308, "Valor da multiplicação incorreta!");
            Assert.AreEqual(MultiplicaRegistros(2, 58.69), 117.38, "Valor da multiplicação incorreta!");
            Assert.AreEqual(MultiplicaRegistros(544, 2), 1088, "Valor da multiplicação incorreta!");
            Assert.AreEqual(MultiplicaRegistros(128, 236), 30208, "Valor da multiplicação incorreta!");
            //Assert.AreEqual(MultiplicaRegistros(965, 4548.5), 7308.5, "Valor da multiplicação incorreta!");
            //Assert.AreEqual(MultiplicaRegistros(59, 487.7), 7308.5, "Valor da multiplicação incorreta!");
            //Assert.AreEqual(MultiplicaRegistros(1, 44), 7308.5, "Valor da multiplicação incorreta!");
        }

        [TestCase(58, 126, 7308)]
        [TestCase(2, 58.69, 117.38)]
        [TestCase(544, 2, 1088)]
        [TestCase(128, 236, 30208)]
        public void Teste_Multiplicacao_Multiplos(double v1, double v2, double result)
        {
            //Processamento preparatório para os testes 
            Assert.AreEqual(MultiplicaRegistros(v1, v2), result, "Valor da multiplicação incorreta!");
        }


        public double MultiplicaRegistros(double v1, double v2) => v1 * v2;


        [Test]
        public void Verifica_inss_funcionario()
        {
            Pessoa pessoa = new Pessoa(1, "Marcelo", 1045, 0);
            CalcIR calcIR = new CalcIR();
            calcIR.CalculaInss(pessoa);
            Assert.AreEqual(pessoa.Inss, 78.38);

            pessoa.Salario = 2000;
            calcIR.CalculaInss(pessoa);
            Assert.AreEqual(pessoa.Inss, 164.33);
        }

        [Test]
        public void Verifica_inss_funcionario_sem_instancia()
        {
            Pessoa pessoa = new Pessoa();
            CalcIR calcIR = new CalcIR();
            calcIR.CalculaInss(pessoa);
            Assert.AreEqual(pessoa.Inss, 0);

        }
        /*
        [Test]
        public void Verifica_ir_faixa_15()
        {
            Pessoa pessoa = new Pessoa(1, "Marcelo", 3000, 1);
            CalcIR calcIR = new CalcIR();
            Assert.AreEqual(calcIR.RetornaIR(pessoa), 43.23);
        }
        */
        /*
        [Test]
        public void tipos_assert()
        {
            string teste = string.Empty;

            //Assert.AreNotEqual(10, 10);//Testa valores diferentes
            //Assert.Greater();//Maior
            //Assert.IsEmpty(teste);
            //Assert.IsTrue(string.IsNullOrEmpty(teste));
            //Assert.NotNull();
            //var ex = Assert.Throws<ArgumentNullException>(() => foo.Bar(null));
            //Assert.That(ex.ParamName, Is.EqualTo("bar"));
            Assert.That(1 == 2);
        }
        */
        [Test]
        public async System.Threading.Tasks.Task TestMockHttpAsync()
        {
            var mockHttp = new MockHttpMessageHandler();

            var jsonMock = ApiProductTest.Properties.Resources.JsonMockServicosBancarios;

            var url = "https://olinda.bcb.gov.br/olinda/servico/Informes_ListaValoresDeServicoBancario/versao/v1/odata/GruposConsolidados?%24format=json&%24top=100";

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("https://olinda.bcb.gov.br/olinda/servico/Informes_ListaValoresDeServicoBancario/versao/v1/odata/*")
                    .Respond("application/json", jsonMock); // Respond with JSON

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            var dadosRetorno = await ServicoBancarioService.RetornaServicosBancariosGet(url, client);

            var servicosBanc = JsonConvert.DeserializeObject<Root>(dadosRetorno);



            //var response = await client.GetAsync("");
            // or without async: var response = client.GetAsync("http://localhost/api/user/1234").Result;

            //var json = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(servicosBanc.value.Count, 2);
            //Assert.AreEqual(json, jsonMock);

        }
        /*
        private HttpClient GetHttpClient()
        {
            var mockHttp = new MockHttpMessageHandler();

            var jsonMock = ApiProductTest.Properties.Resources.JsonMockServicosBancarios;

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("https://olinda.bcb.gov.br/olinda/servico/Informes_ListaValoresDeServicoBancario/versao/v1/odata/*")
                    .Respond("application/json", jsonMock); // Respond with JSON

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();
        }
        */

        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("teste valido teste valido teste valido", true)]
        [TestCase("teste                                                                              ", false)]
        public void Verifica_ds_produto(string dsProduto, bool isValid)
        {
            Assert.AreEqual(Produto.IsDsProdutoValida(dsProduto), isValid);
        }

        [Test]
        public void Verifica_categoria_produto()
        {
            Produto produto = new Business.Domain.Produto();
            produto.NrValor = 500;
            produto.setCategoriaProduto(produto.NrValor ?? 0);
            Assert.AreEqual(produto.DsCategoriaProduto, "PC");
            produto.NrValor = 5000;
            produto.setCategoriaProduto(produto.NrValor ?? 0);
            Assert.AreEqual(produto.DsCategoriaProduto, "AV");
            produto.NrValor = 100000;
            produto.setCategoriaProduto(produto.NrValor ?? 0);
            Assert.AreEqual(produto.DsCategoriaProduto, "PL");
            produto.NrValor = -9999;
            produto.setCategoriaProduto(produto.NrValor ?? 0);
            Assert.AreEqual(produto.DsCategoriaProduto, "SC");
            produto.NrValor = 0;
            produto.setCategoriaProduto(produto.NrValor ?? 0);
            Assert.AreEqual(produto.DsCategoriaProduto, "SC");
        }

        [Test]
        public void Verifica_valor_produto()
        {
            Produto produto = new Produto();
            produto.NrValor = 0;
            Assert.AreEqual(produto.isValorProdutoValido(), true);
            produto.NrValor = 500;
            Assert.AreEqual(produto.isValorProdutoValido(), true);
            produto.NrValor = 999999999.01M;
            Assert.AreEqual(produto.isValorProdutoValido(), false);
            produto.NrValor = -1;
            Assert.AreEqual(produto.isValorProdutoValido(), false);
        }

        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("abc", false)]
        [TestCase("Masca teste", true)]
        [TestCase("Masca teste                                                             ", true)]
        [TestCase("TESTE GIGANTE TESTE GIGANTE TESTE GIGANTE TESTE GIGANTE TESTE GIGANTE TESTE GIGANTE TESTE GIGANTE TESTE GIGANTE TESTE GIGANTE TESTE GIGANTE", false)]
        public void Valida_ds_marca(string dsMarca, bool result)
        {
            Assert.AreEqual(Marca.IsDsMarcaValida(dsMarca),result);

        }

        [TestCase("")]
        [TestCase(null)]
        public void Valida_marca(string dsMarca)
        {
            Assert.IsFalse(Marca.IsDsMarcaValida(dsMarca));

        }

        [Test]
        public void Calcula_preco_venda_produto()
        {
            Assert.AreEqual(Helper.CalculaPrecoVenda(25, 10), 27.5M);
            Assert.AreNotEqual(Helper.CalculaPrecoVenda(10, 10), 5M);
        }

        [Test]
        public void Verifica_cadastro_completo()
        {
            Produto produto = new Produto();
            produto.CdProduto = 1;
            produto.DsProduto = "Produto teste";
            produto.CdMarca = 1;
            produto.DsCategoriaProduto = "PC";
            produto.NrValor = 25.00M;
            Assert.IsTrue(Helper.IsCadastroCompleto(produto));

            produto = new Produto();
            produto.CdProduto = 1;
            produto.DsCategoriaProduto = "PC";
            produto.NrValor = 25.00M;
            Assert.IsFalse(Helper.IsCadastroCompleto(produto));
        }

        [Test]
        public void aplica_desconto_valor()
        {
            Produto produto = new Produto();
            produto.NrValor = 255.50M;
            Assert.AreEqual(Helper.AplicaValorDesconto(produto.NrValor ?? 0, 270), 0);
            produto = new Produto();
            produto.NrValor = 180.20M;
            Assert.AreEqual(Helper.AplicaValorDesconto(produto.NrValor ?? 0, 50), 130.20);
            produto.NrValor = 0;
            Assert.AreEqual(Helper.AplicaValorDesconto(produto.NrValor ?? 0, 50), 0);
        }

        [Test]
        public void aplica_desconto_porcent()
        {
            Produto produto = new Produto();
            produto.NrValor = 260.50M;
            Assert.AreEqual(Helper.AplicaPorcentDesconto(produto.NrValor ?? 0, 50), 130.25);
            produto.NrValor = 260.50M;
            Assert.AreEqual(Helper.AplicaPorcentDesconto(produto.NrValor ?? 0, 120), 260.50M);
        }

        [Test]
        public void valida_preco_custo()
        {
            Assert.IsFalse(Helper.validaPrecoCusto(100.50M, 105.00M));
            Assert.IsTrue(Helper.validaPrecoCusto(100.50M, 40.25M));
        }

        [Test]
        public async System.Threading.Tasks.Task Valida_get_count()
        {
            var mockHttp = new MockHttpMessageHandler();

            var jsonMock = ApiProductTest.Properties.Resources.JsonMockServicosBancarios;

            var url = "";

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("https://olinda.bcb.gov.br/olinda/servico/Informes_ListaValoresDeServicoBancario/versao/v1/odata/*")
                    .Respond("application/json", jsonMock); // Respond with JSON

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            var dadosRetorno = await ServicoBancarioService.RetornaServicosBancariosGet(url, client);

            //var servicosBanc = JsonConvert.DeserializeObject<Root>(dadosRetorno);



            //var response = await client.GetAsync("");
            // or without async: var response = client.GetAsync("http://localhost/api/user/1234").Result;

            //var json = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(servicosBanc.value.Count, 2);
            //Assert.AreEqual(json, jsonMock);

        }
    }
}