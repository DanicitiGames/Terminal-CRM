using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

public class Dados
{
    public List<Produto> produtos { get; set; }
    public List<Venda> vendas {get; set; }
    public List<Compra> compras { get; set; }
    public List<Cliente> clientes { get; set; }
    public List<Fornecedor> fornecedores { get; set; }
    public List<Historico> historicos { get; set; }
    public int quantidadeProdutos { get; set; }
    public int quantidadeVendas { get; set; }
    public int quantidadeCompras { get; set; }
    public int quantidadeClientes { get; set; }
    public int quantidadeFornecedores { get; set; }
    public int quantidadeHistorico { get; set; }

    public Dados(List<Produto> produtos, List<Venda> vendas, List<Compra> compras, List<Cliente> clientes, List<Fornecedor> fornecedores, List<Historico> historicos, int quantidadeProdutos, int quantidadeVendas, int quantidadeCompras, int quantidadeClientes, int quantidadeFornecedores, int quantidadeHistorico)
    {
        this.produtos = produtos;
        this.vendas = vendas;
        this.compras = compras;
        this.clientes = clientes;
        this.fornecedores = fornecedores;
        this.historicos = historicos;
        this.quantidadeProdutos = quantidadeProdutos;
        this.quantidadeVendas = quantidadeVendas;
        this.quantidadeCompras = quantidadeCompras;
        this.quantidadeClientes = quantidadeClientes;
        this.quantidadeFornecedores = quantidadeFornecedores;
        this.quantidadeHistorico = quantidadeHistorico;
    }
}

public class Produto
{
    public int codigo { get; private set; }
    public int codigoFornecedor { get; set; }
    public string nome { get; set; }
    public string descricao { get; set; }
    public double preco { get; set; }
    public string categoria { get; set; }
    public int quantidadeEstoque { get; set; }
    public bool permitirVendaSemEstoque { get; set; }

    public Produto(int codigo, string nome, string descricao, double preco, string categoria, int quantidadeEstoque, bool permitirVendaSemEstoque)
    {
        this.codigo = codigo;
        this.nome = nome;
        this.descricao = descricao;
        this.preco = preco;
        this.categoria = categoria;
        this.quantidadeEstoque = quantidadeEstoque;
        this.permitirVendaSemEstoque = permitirVendaSemEstoque;
    }
}

public class Venda
{
    public int codigo { get; private set; }
    public int codigoProduto { get; set; }
    public int codigoCliente { get; set; }
    public string data { get; set; }
    public int quantidade { get; set; }
    public double valorTotal { get; set; }
    public int situacao { get; set; }

    public Venda(int codigo, int codigoProduto, int codigoCliente, string data, int quantidade, double valorTotal, int situacao)
    {
        this.codigo = codigo;
        this.codigoProduto = codigoProduto;
        this.codigoCliente = codigoCliente;
        this.data = data;
        this.quantidade = quantidade;
        this.valorTotal = valorTotal;
        this.situacao = situacao;
    }
}

public class Compra
{
    public int codigo { get; private set; }
    public int codigoProduto { get; set; }
    public int codigoFornecedor { get; set; }
    public string data { get; set; }
    public int quantidade { get; set; }
    public double valorTotal { get; set; }
    public int situacao { get; set; }

    public Compra(int codigo, int codigoProduto, int codigoFornecedor, string data, int quantidade, double valorTotal, int situacao)
    {
        this.codigo = codigo;
        this.codigoProduto = codigoProduto;
        this.codigoFornecedor = codigoFornecedor;
        this.data = data;
        this.quantidade = quantidade;
        this.valorTotal = valorTotal;
        this.situacao = situacao;
    }
}

public class Cliente
{
    public int codigo { get; private set; }
    public string nome { get; set; }
    public string cpf { get; set; }
    public string endereco { get; set; }
    public string telefone { get; set; }
    public string email { get; set; }

    public Cliente(int codigo, string nome, string cpf, string endereco, string telefone, string email)
    {
        this.codigo = codigo;
        this.nome = nome;
        this.cpf = cpf;
        this.endereco = endereco;
        this.telefone = telefone;
        this.email = email;
    }
}

public class Fornecedor
{
    public int codigo { get; private set; }
    public string nome { get; set; }
    public string cnpj { get; set; }
    public string endereco { get; set; }
    public string telefone { get; set; }
    public string email { get; set; }

    public Fornecedor(int codigo, string nome, string cnpj, string endereco, string telefone, string email)
    {
        this.codigo = codigo;
        this.nome = nome;
        this.cnpj = cnpj;
        this.endereco = endereco;
        this.telefone = telefone;
        this.email = email;
    }
}

public class Historico
{
    public int codigo { get; private set; }
    public string data { get; set; }
    public string descricao { get; set; }

    public Historico(int codigo, string data, string descricao)
    {
        this.codigo = codigo;
        this.data = data;
        this.descricao = descricao;
    }
}

public class Program
{
    private static string caminhoSalvamento = "dados.json";

    private static int quantidadeProdutos, quantidadeVendas, quantidadeCompras, quantidadeClientes, quantidadeFornecedores, quantidadeHistorico = 0;
    private static List<Produto> produtos = new List<Produto>();
    private static List<Venda> vendas = new List<Venda>();
    private static List<Compra> compras = new List<Compra>();
    private static List<Cliente> clientes = new List<Cliente>();
    private static List<Fornecedor> fornecedores = new List<Fornecedor>();
    private static List<Historico> historicos = new List<Historico>();

	public static void Main()
	{
        CarregarDados();
        while (true) Menu();
	}

    public static void Menu()
    {
        List<string> opcoes = new List<string> { "Produtos", "Estoque", "Vendas", "Compras", "Clientes", "Fornecedores", "Relatórios", "Histórico", "Configurações", "Sair" };
        int indice = MenuTool_Navegar("Sistema de gerenciamento de vendas:", opcoes);

        switch(indice)
        {
            case 0:
                MenuProdutos();
                break;
            case 1:
                MenuEstoque();
                break;
            case 2:
                MenuVendas();
                break;
            case 3:
                MenuCompras();
                break;
            case 4:
                MenuClientes();
                break;
            case 5:
                MenuFornecedores();
                break;
            case 6:
                MenuRelatorios();
                break;
            case 7:
                MenuHistorico();
                break;
            case 8:
                MenuConfiguracoes();
                break;
            case 9:
                Environment.Exit(0);
                break;
        }
    }

    public static void MenuProdutos()
    {
        List<string> opcoes = new List<string> { "Cadastrar", "Editar", "Excluir", "Listar", "Voltar" };
        int indice = MenuTool_Navegar("Produtos:", opcoes);

        switch (indice)
        {
            case 0:
                MenuCadastrarProduto();
                break;
            case 1:
                MenuEditarProduto();
                break;
            case 2:
                MenuExcluirProduto();
                break;
            case 3:
                MenuListarProdutos();
                break;
            case -1:
            case 4:
                return;
        }
        MenuProdutos();
    }

    public static void MenuCadastrarProduto()
    {
        Console.Clear();
        Console.WriteLine("Cadastrar Produto:");
        string? nome, descricao, preco, categoria, quantidadeEstoque, permitirVendaSemEstoque;
        
        nome = MenuTool_Input("Nome: ", true);
        descricao = MenuTool_Input("Descrição: ", false);
        preco = MenuTool_Input("Preço: ", false).Replace(".", ",");
        categoria = MenuTool_Input("Categoria: (ex: Eletrônicos, Roupas, Alimentos) ", false);
        quantidadeEstoque = MenuTool_Input("Quantidade em estoque: ", false);
        permitirVendaSemEstoque = MenuTool_Input("Permitir vendas sem estoque? (s/n) ", false);

        if(categoria == "") categoria = "Sem categoria";

        quantidadeProdutos++;
        Produto produto = new Produto(quantidadeProdutos, nome, descricao, MenuTool_TentarConverterParaDouble(preco), categoria, MenuTool_TentarConverterParaInteiro(quantidadeEstoque), permitirVendaSemEstoque.ToLower() == "s");
        produtos.Add(produto);

        AdicionarHistorico($"Produto cadastrado: {produto.nome}");

        SalvarDados();
        Alert("Produto cadastrado com sucesso!");
    }
    
    public static void MenuEditarProduto()
    {
        Console.Clear();
        if(produtos.Count == 0)
        {
            Alert("Nenhum produto cadastrado!");
            return;
        }

        Produto? produto = MenuTool_NavegarProduto("Editar produto:", false, false);
        if(produto == null) return;

        List<string> opcoes = new List<string> { $"Nome: {produto.nome}", $"Descrição: {produto.descricao}", $"Preço: {Real(produto.preco)}", $"Categoria: {produto.categoria}", $"Quantidade em estoque: {produto.quantidadeEstoque}", $"Permitir vendas sem estoque? {(produto.permitirVendaSemEstoque ? "Sim" : "Não")}", "Voltar" };
        int indice = MenuTool_Navegar("Editar produto:", opcoes);

        switch(indice)
        {
            case 0:
                string novoNome = MenuTool_Input("Novo nome: ", true);
                if(novoNome.Length == 0) break;
                produto.nome = novoNome;
                AdicionarHistorico($"Produto editado: {produto.nome}");
                SalvarDados();
                break;
            case 1:
                produto.descricao = MenuTool_Input("Nova descrição: ", false);
                AdicionarHistorico($"Produto editado: {produto.nome}");
                SalvarDados();
                break;
            case 2:
                string novoPreco = MenuTool_Input("Novo preço: ", false).Replace(".", ",");
                double novoPrecoDouble = MenuTool_TentarConverterParaDouble(novoPreco);
                if(novoPrecoDouble <= 0) break; 
                produto.preco = novoPrecoDouble;
                AdicionarHistorico($"Produto editado: {produto.nome}");
                SalvarDados();
                break;
            case 3:
                produto.categoria = MenuTool_Input("Nova categoria: ", false);
                if(produto.categoria == "") produto.categoria = "Sem categoria";
                AdicionarHistorico($"Produto editado: {produto.nome}");
                SalvarDados();
                break;
            case 4:
                string novaQuantidadeEstoque = MenuTool_Input("Nova quantidade em estoque: ", false);
                int novaQuantidadeEstoqueInt = MenuTool_TentarConverterParaInteiro(novaQuantidadeEstoque);
                if(novaQuantidadeEstoqueInt <= 0)  break;
                produto.quantidadeEstoque = novaQuantidadeEstoqueInt;
                AdicionarHistorico($"Produto editado: {produto.nome}");
                SalvarDados();
                break;
            case 5:
                string permitirVendaSemEstoque = MenuTool_Input("Permitir vendas sem estoque? (s/n) ", false);
                produto.permitirVendaSemEstoque = permitirVendaSemEstoque.ToLower() == "s";
                AdicionarHistorico($"Produto editado: {produto.nome}");
                SalvarDados();
                break;
            case -1:
            case 6:
                return;
            default:
                break;
        }
        MenuEditarProduto();
    }

    public static void MenuExcluirProduto()
    {
        Console.Clear();

        if(produtos.Count == 0)
        {
            Alert("Nenhum produto cadastrado!");
            return;
        }

        Produto? produto = MenuTool_NavegarProduto("Excluir produto:", false, false);
        if(produto == null) return;

        bool resposta = MenuTool_Confirmar($"Deseja realmente excluir este produto ({produto.nome})?");
        if(!resposta) return;
        produtos.Remove(produto);
        AdicionarHistorico($"Produto excluído: {produto.nome}");
        SalvarDados();
    }
    
    public static void MenuListarProdutos()
    {
        Console.Clear();
        if(produtos.Count == 0)
        {
            Alert("Nenhum produto cadastrado!");
            return;
        }
        Console.WriteLine("Produtos:");
        List<List<Produto>> categorias = new List<List<Produto>>();
        foreach(Produto produto in produtos)
        {
            bool categoriaEncontrada = false;
            foreach(List<Produto> categoria in categorias)
            {
                if(categoria[0].categoria == produto.categoria)
                {
                    categoria.Add(produto);
                    categoriaEncontrada = true;
                    break;
                }
            }
            if(!categoriaEncontrada)
            {
                List<Produto> novaCategoria = new List<Produto>();
                novaCategoria.Add(produto);
                categorias.Add(novaCategoria);
            }
        }
        foreach(List<Produto> categoria in categorias)
        {
            Console.WriteLine($"\n|({categoria.Count}) {categoria[0].categoria}");
            foreach(Produto produto in categoria)
            {
                if(produto.descricao != "") Console.WriteLine($"|#{produto.codigo} - {produto.nome} ({produto.descricao})");
                else Console.WriteLine($"|#{produto.codigo} - {produto.nome}");
            }
        }
        Console.WriteLine();
        PressioneQualquerTecla();
    }

    public static void MenuEstoque()
    {
        List<string> opcoes = new List<string> { "Adicionar", "Reduzir", "Listar", "Voltar" };
        int indice = MenuTool_Navegar("Estoque:", opcoes);
        
        switch(indice)
        {
            case 0:
                MenuAdicionarEstoque();
                break;
            case 1:
                MenuReduzirEstoque();
                break;
            case 2:
                MenuListarEstoque();
                break;
            case -1:
            case 3:
                return;
        }
        MenuEstoque();
    }

    public static void MenuAdicionarEstoque()
    {
        Console.Clear();
        if(produtos.Count == 0)
        {
            Alert("Nenhum produto cadastrado!");
            return;
        }

        Produto? produto = MenuTool_NavegarProduto("Adicionar ao estoque:", true, false);
        if(produto == null) return;

        Console.WriteLine($"Produto: {produto.nome}\n");
        string quantidade = MenuTool_Input("Quantidade a ser adicionada: ", true);
        int quantidadeInt = MenuTool_TentarConverterParaInteiro(quantidade);
        Console.WriteLine();

        if(IntVerificadorPositiva(quantidadeInt) == false) return;

        produto.quantidadeEstoque += quantidadeInt;
        AdicionarHistorico($"Adicionado ao estoque: {produto.nome} ({quantidadeInt})");
        SalvarDados();
        Alert("Estoque atualizado com sucesso!");
    }

    public static void MenuReduzirEstoque()
    {
        Console.Clear();
        if(produtos.Count == 0)
        {
            Alert("Nenhum produto cadastrado!");
            return;
        }

        Produto? produto = MenuTool_NavegarProduto("Reduzir do estoque:", true, false);
        if(produto == null) return;

        Console.WriteLine($"Produto: {produto.nome}\n");
        string quantidade = MenuTool_Input("Quantidade a ser reduzida: ", true);
        int quantidadeInt = MenuTool_TentarConverterParaInteiro(quantidade);
        Console.WriteLine();

        if(IntVerificadorPositiva(quantidadeInt) == false) return;
        if(quantidadeInt > produto.quantidadeEstoque)
        {
            Alert("Quantidade maior que o estoque!");
            return;
        }

        produto.quantidadeEstoque -= quantidadeInt;
        AdicionarHistorico($"Reduzido do estoque: {produto.nome} ({quantidadeInt})");
        SalvarDados();
        Alert("Estoque atualizado com sucesso!");
    }

    public static void MenuListarEstoque()
    {
        Console.Clear();
        if(produtos.Count == 0)
        {
            Alert("Nenhum produto cadastrado!");
            return;
        }
        Console.WriteLine("Estoque:");
        foreach(Produto produto in produtos)
        {
            Console.WriteLine($"#{produto.codigo} - {produto.nome} ({produto.quantidadeEstoque})");
        }
        Console.WriteLine();
        PressioneQualquerTecla();
    }

    public static void MenuVendas()
    {
        if(produtos.Count == 0)
        {
            Alert("Você precisa ter pelo menos um produto cadastrado!");
            return;
        }
        if(clientes.Count == 0)
        {
            Alert("Você precisa ter pelo menos um cliente cadastrado!");
            return;
        }

        List<string> opcoes = new List<string> { "Registrar venda", "Listar", "Voltar" };
        int indice = MenuTool_Navegar("Vendas:", opcoes);

        switch(indice)
        {
            case 0:
                MenuRegistrarVenda();
                break;
            case 1:
                MenuListarVendas();
                break;
            case -1:
            case 2:
                return;
        }
        MenuVendas();
    }

    public static void MenuRegistrarVenda()
    {
        Console.Clear();
        Console.WriteLine("Registrar Venda:");
        Produto? produto = MenuTool_NavegarProduto("Escolha um produto:", true, true);
        if(produto == null) return;

        if(produto.quantidadeEstoque == 0 && !produto.permitirVendaSemEstoque)
        {
            Alert("Produto sem estoque! Venda cancelada!");
            return;
        }

        Cliente? cliente = MenuTool_NavegarCliente("Escolha um cliente:");
        if(cliente == null) return;

        Console.WriteLine($"Produto: {produto.nome}\nCliente: {cliente.nome}\n");
        string quantidade = MenuTool_Input("Quantidade: ", true);
        int quantidadeInt = MenuTool_TentarConverterParaInteiro(quantidade);
        Console.WriteLine();

        if(IntVerificadorPositiva(quantidadeInt) == false) return;
        if(quantidadeInt > produto.quantidadeEstoque && !produto.permitirVendaSemEstoque)
        {
            Alert("Quantidade maior que o estoque! Venda cancelada!");
            return;
        }
        if(quantidadeInt < 1)
        {
            Alert("Quantidade inválida! Venda cancelada!");
            return;
        }

        double valorTotal = quantidadeInt * produto.preco;
        List<string> opcoes = new List<string> { "Sim", "Não" };
        int indice = MenuTool_Navegar($"Valor total: {Real(valorTotal)}\nDeseja alterar o valor?", opcoes);

        if(indice == 0)
        {
            string novoValor = MenuTool_Input("Novo valor: ", true).Replace(".", ",");
            double novoValorDouble = MenuTool_TentarConverterParaDouble(novoValor);
            if(novoValorDouble <= 0)
            {
                Alert("Valor inválido! Venda cancelada!");
                return;
            }
            valorTotal = novoValorDouble;
        }

        produto.quantidadeEstoque -= quantidadeInt;
        quantidadeVendas++;
        Venda venda = new Venda(quantidadeVendas, produto.codigo, cliente.codigo, HorarioAtual(), quantidadeInt, valorTotal, 1);
        vendas.Add(venda);
        AdicionarHistorico($"Venda registrada: {produto.nome} ({quantidadeInt}) - {cliente.nome}");
        SalvarDados();
        Alert("Venda registrada com sucesso!");
    }

    public static void MenuListarVendas()
    {
        Console.Clear();
        if(vendas.Count == 0)
        {
            Alert("Nenhuma venda registrada!");
            return;
        }
        Venda? venda = MenuTool_NavegarVenda("Vendas:");
        if(venda == null) return;

        Produto produto = produtos.Find(p => p.codigo == venda.codigoProduto);
        Cliente cliente = clientes.Find(c => c.codigo == venda.codigoCliente);

        Console.WriteLine($"Venda #{venda.codigo}\nProduto: {produto.nome} ({venda.quantidade})\nCliente: {cliente.nome}\nData: {venda.data}\nValor total: {Real(venda.valorTotal)}\n");
        List<string> opcoes = new List<string> { "Cancelar", "Voltar" };
        int indice = MenuTool_Navegar("Venda:", opcoes);
        if(indice == 1) return;
        venda.situacao = 0;
    }

    public static void MenuCompras()
    {
        if(produtos.Count == 0)
        {
            Alert("Você precisa ter pelo menos um produto cadastrado!");
            return;
        }
        if(fornecedores.Count == 0)
        {
            Alert("Você precisa ter pelo menos um fornecedor cadastrado!");
            return;
        }

        List<string> opcoes = new List<string> { "Registrar compra", "Listar", "Voltar" };
        int indice = MenuTool_Navegar("Compras:", opcoes);

        switch(indice)
        {
            case 0:
                MenuRegistrarCompra();
                break;
            case 1:
                MenuListarCompras();
                break;
            case -1:
            case 2:
                return;
        }
        MenuCompras();
    }

    public static void MenuRegistrarCompra()
    {
        Console.Clear();
        Console.WriteLine("Registrar Compra:");
        Produto? produto = MenuTool_NavegarProduto("Escolha um produto:", true, true);
        if(produto == null) return;

        Fornecedor? fornecedor = MenuTool_NavegarFornecedor("Escolha um fornecedor:");
        if(fornecedor == null) return;

        Console.WriteLine($"Produto: {produto.nome}\nFornecedor: {fornecedor.nome}\n");
        string quantidade = MenuTool_Input("Quantidade: ", true);
        int quantidadeInt = MenuTool_TentarConverterParaInteiro(quantidade);
        Console.WriteLine();

        if(IntVerificadorPositiva(quantidadeInt) == false) return;
        if(quantidadeInt < 1)
        {
            Alert("Quantidade inválida! Compra cancelada!");
            return;
        }

        double valorTotal = quantidadeInt * produto.preco;
        List<string> opcoes = new List<string> { "Sim", "Não" };
        int indice = MenuTool_Navegar($"Valor total: {Real(valorTotal)}\nDeseja alterar o valor?", opcoes);

        if(indice == 0)
        {
            string novoValor = MenuTool_Input("Novo valor: ", true).Replace(".", ",");
            double novoValorDouble = MenuTool_TentarConverterParaDouble(novoValor);
            if(novoValorDouble <= 0)
            {
                Alert("Valor inválido! Compra cancelada!");
                return;
            }
            valorTotal = novoValorDouble;
        }

        produto.quantidadeEstoque += quantidadeInt;
        quantidadeCompras++;
        Compra compra = new Compra(quantidadeCompras, produto.codigo, fornecedor.codigo, HorarioAtual(), quantidadeInt, valorTotal, 1);
        compras.Add(compra);
        AdicionarHistorico($"Compra registrada: {produto.nome} ({quantidadeInt}) - {fornecedor.nome}");
        SalvarDados();
        Alert("Compra registrada com sucesso!");
    }

    public static void MenuListarCompras()
    {
        Console.Clear();
        if(compras.Count == 0)
        {
            Alert("Nenhuma compra registrada!");
            return;
        }
        Compra? compra = MenuTool_NavegarCompra("Compras:");
        if(compra == null) return;

        Produto produto = produtos.Find(p => p.codigo == compra.codigoProduto);
        Fornecedor fornecedor = fornecedores.Find(f => f.codigo == compra.codigoFornecedor);

        Console.WriteLine($"Compra #{compra.codigo}\nProduto: {produto.nome} ({compra.quantidade})\nFornecedor: {fornecedor.nome}\nData: {compra.data}\nValor total: {Real(compra.valorTotal)}\n");
        List<string> opcoes = new List<string> { "Cancelar", "Voltar" };
        int indice = MenuTool_Navegar("Compra:", opcoes);
        if(indice == 1) return;
        compra.situacao = 0;
    }

    public static void MenuClientes()
    {
        List<string> opcoes = new List<string> { "Cadastrar", "Editar", "Excluir", "Listar", "Voltar" };
        int indice = MenuTool_Navegar("Clientes:", opcoes);

        switch(indice)
        {
            case 0:
                MenuCadastrarCliente();
                break;
            case 1:
                MenuEditarCliente();
                break;
            case 2:
                MenuExcluirCliente();
                break;
            case 3:
                MenuListarClientes();
                break;
            case -1:
            case 4:
                return;
        }
        MenuClientes();
    }

    public static void MenuCadastrarCliente()
    {
        Console.Clear();
        Console.WriteLine("Cadastrar Cliente:");
        string nome, cpf, endereco, telefone, email;
        
        nome = MenuTool_Input("Nome: ", true);
        cpf = MenuTool_Input("CPF: ", false);
        endereco = MenuTool_Input("Endereço: ", false);
        telefone = MenuTool_Input("Telefone: ", false);
        email = MenuTool_Input("E-mail: ", false);

        quantidadeClientes++;
        Cliente cliente = new Cliente(quantidadeClientes, nome, cpf, endereco, telefone, email);
        clientes.Add(cliente);

        AdicionarHistorico($"Cliente cadastrado: {cliente.nome}");
        SalvarDados();
        Alert("Cliente cadastrado com sucesso!");
    }

    public static void MenuEditarCliente()
    {
        Console.Clear();
        if(clientes.Count == 0)
        {
            Alert("Nenhum cliente cadastrado!");
            return;
        }

        Cliente? cliente = MenuTool_NavegarCliente("Editar cliente:");
        if(cliente == null) return;

        List<string> opcoes = new List<string> { $"Nome: {cliente.nome}", $"CPF: {cliente.cpf}", $"Endereço: {cliente.endereco}", $"Telefone: {cliente.telefone}", $"E-mail: {cliente.email}", "Voltar" };
        int indice = MenuTool_Navegar("Editar cliente:", opcoes);

        switch(indice)
        {
            case 0:
                string novoNome = MenuTool_Input("Novo nome: ", true);
                if(novoNome.Length == 0) break;
                cliente.nome = novoNome;
                AdicionarHistorico($"Cliente editado: {cliente.nome}");
                SalvarDados();
                break;
            case 1:
                string novoCPF = MenuTool_Input("Novo CPF: ", true);
                if(novoCPF.Length == 0) break;
                cliente.cpf = novoCPF;
                AdicionarHistorico($"Cliente editado: {cliente.nome}");
                SalvarDados();
                break;
            case 2:
                cliente.endereco = MenuTool_Input("Novo endereço: ", false);
                AdicionarHistorico($"Cliente editado: {cliente.nome}");
                SalvarDados();
                break;
            case 3:
                cliente.telefone = MenuTool_Input("Novo telefone: ", false);
                AdicionarHistorico($"Cliente editado: {cliente.nome}");
                SalvarDados();
                break;
            case 4:
                cliente.email = MenuTool_Input("Novo e-mail: ", false);
                AdicionarHistorico($"Cliente editado: {cliente.nome}");
                SalvarDados();
                break;
            case -1:
            case 5:
                return;
            default:
                break;
        }
        MenuEditarCliente();
    }

    public static void MenuExcluirCliente()
    {
        Console.Clear();

        if(clientes.Count == 0)
        {
            Alert("Nenhum cliente cadastrado!");
            return;
        }

        Cliente? cliente = MenuTool_NavegarCliente("Excluir cliente:");
        if(cliente == null) return;

        bool resposta = MenuTool_Confirmar($"Deseja realmente excluir este cliente ({cliente.nome})?");
        if(!resposta) return;
        clientes.Remove(cliente);
        AdicionarHistorico($"Cliente excluído: {cliente.nome}");
        SalvarDados();
    }

    public static void MenuListarClientes()
    {
        Console.Clear();
        if(clientes.Count == 0)
        {
            Alert("Nenhum cliente cadastrado!");
            return;
        }
        Console.WriteLine("Clientes:");
        foreach(Cliente cliente in clientes)
        {
            Console.WriteLine($"#{cliente.codigo} - {cliente.nome}");
        }
        Console.WriteLine();
        PressioneQualquerTecla();
    }

    public static void MenuFornecedores()
    {
        List<string> opcoes = new List<string> { "Cadastrar", "Editar", "Definir produtos X fornecedor", "Excluir", "Listar", "Voltar" };
        int indice = MenuTool_Navegar("Fornecedores:", opcoes);

        switch(indice)
        {
            case 0:
                MenuCadastrarFornecedor();
                break;
            case 1:
                MenuEditarFornecedor();
                break;
            case 2:
                MenuDefinirProdutosFornecedor();
                break;
            case 3:
                MenuExcluirFornecedor();
                break;
            case 4:
                MenuListarFornecedores();
                break;
            case -1:
            case 5:
                return;
        }
        MenuFornecedores();
    }

    public static void MenuCadastrarFornecedor()
    {
        Console.Clear();
        Console.WriteLine("Cadastrar Fornecedor:");
        string nome, cnpj, endereco, telefone, email;
        
        nome = MenuTool_Input("Nome: ", true);
        cnpj = MenuTool_Input("CNPJ: ", false);
        endereco = MenuTool_Input("Endereço: ", false);
        telefone = MenuTool_Input("Telefone: ", false);
        email = MenuTool_Input("E-mail: ", false);

        quantidadeFornecedores++;
        Fornecedor fornecedor = new Fornecedor(quantidadeFornecedores, nome, cnpj, endereco, telefone, email);
        fornecedores.Add(fornecedor);
        
        AdicionarHistorico($"Fornecedor cadastrado: {fornecedor.nome}");
        SalvarDados();
        Alert("Fornecedor cadastrado com sucesso!");
    }

    public static void MenuEditarFornecedor()
    {
        Console.Clear();
        if(fornecedores.Count == 0)
        {
            Alert("Nenhum fornecedor cadastrado!");
            return;
        }

        Fornecedor? fornecedor = MenuTool_NavegarFornecedor("Editar fornecedor:");
        if(fornecedor == null) return;

        List<string> opcoes = new List<string> { $"Nome: {fornecedor.nome}", $"CNPJ: {fornecedor.cnpj}", $"Endereço: {fornecedor.endereco}", $"Telefone: {fornecedor.telefone}", $"E-mail: {fornecedor.email}", "Voltar" };
        int indice = MenuTool_Navegar("Editar fornecedor:", opcoes);

        switch(indice)
        {
            case 0:
                string novoNome = MenuTool_Input("Novo nome: ", true);
                if(novoNome.Length == 0) break;
                fornecedor.nome = novoNome;
                AdicionarHistorico($"Fornecedor editado: {fornecedor.nome}");
                SalvarDados();
                break;
            case 1:
                string novoCNPJ = MenuTool_Input("Novo CNPJ: ", true);
                if(novoCNPJ.Length == 0) break;
                fornecedor.cnpj = novoCNPJ;
                AdicionarHistorico($"Fornecedor editado: {fornecedor.nome}");
                SalvarDados();
                break;
            case 2:
                fornecedor.endereco = MenuTool_Input("Novo endereço: ", false);
                AdicionarHistorico($"Fornecedor editado: {fornecedor.nome}");
                SalvarDados();
                break;
            case 3:
                fornecedor.telefone = MenuTool_Input("Novo telefone: ", false);
                AdicionarHistorico($"Fornecedor editado: {fornecedor.nome}");
                SalvarDados();
                break;
            case 4:
                fornecedor.email = MenuTool_Input("Novo e-mail: ", false);
                AdicionarHistorico($"Fornecedor editado: {fornecedor.nome}");
                SalvarDados();
                break;
            case -1:
            case 5:
                return;
            default:
                break;
        }
        MenuEditarFornecedor();
    }

    public static void MenuDefinirProdutosFornecedor()
    {
        Console.Clear();
        if(fornecedores.Count == 0)
        {
            Alert("Nenhum fornecedor cadastrado!");
            return;
        }
        if(produtos.Count == 0)
        {
            Alert("Nenhum produto cadastrado!");
            return;
        }
        Produto? produto = MenuTool_NavegarProdutoFornecedor();
        if(produto == null) return;

        Fornecedor? fornecedor = MenuTool_NavegarFornecedor("Escolha um fornecedor para o produto:");
        if(fornecedor == null) return;

        produto.codigoFornecedor = fornecedor.codigo;
        AdicionarHistorico($"Produto definido para fornecedor: {produto.nome} ({fornecedor.nome})");
        SalvarDados();
        Alert("Fornecedor definido com sucesso!");
    }

    public static void MenuExcluirFornecedor()
    {
        Console.Clear();

        if(fornecedores.Count == 0)
        {
            Alert("Nenhum fornecedor cadastrado!");
            return;
        }

        Fornecedor? fornecedor = MenuTool_NavegarFornecedor("Excluir fornecedor:");
        if(fornecedor == null) return;

        bool resposta = MenuTool_Confirmar($"Deseja realmente excluir este fornecedor ({fornecedor.nome})?");
        if(!resposta) return;
        fornecedores.Remove(fornecedor);
        AdicionarHistorico($"Fornecedor excluído: {fornecedor.nome}");
        SalvarDados();
    }

    public static void MenuListarFornecedores()
    {
        Console.Clear();
        if(fornecedores.Count == 0)
        {
            Alert("Nenhum fornecedor cadastrado!");
            return;
        }
        Console.WriteLine("Fornecedores:");
        foreach(Fornecedor fornecedor in fornecedores)
        {
            Console.WriteLine($"#{fornecedor.codigo} - {fornecedor.nome}");
        }
        Console.WriteLine();
        PressioneQualquerTecla();
    }

    public static void MenuRelatorios()
    {
        Console.Clear();
        List<string> opcoes = new List<string> { "Gerar relatório cliente", "Gerar relatório fornecedor", "Gerar relatório geral", "Voltar" };
        int indice = MenuTool_Navegar("Relatórios:", opcoes);
        
        switch(indice)
        {
            case 0:
                MenuRelatorioCliente();
                break;
            case 1:
                MenuRelatorioFornecedor();
                break;
            case 2:
                MenuRelatorioGeral();
                break;
            case -1:
            case 3:
                return;
        }
        MenuRelatorios();
    }

    public static void MenuRelatorioCliente()
    {
        Console.Clear();
        if(clientes.Count == 0)
        {
            Alert("Nenhum cliente cadastrado!");
            return;
        }
        Cliente? cliente = MenuTool_NavegarCliente("Escolha um cliente para o relatório:");
        if(cliente == null) return;

        List<Venda> vendasCliente = vendas.FindAll(v => v.codigoCliente == cliente.codigo);
        if(vendasCliente.Count == 0)
        {
            Alert("Nenhuma venda registrada para este cliente!");
            return;
        }

        Console.WriteLine($"Relatório do cliente: {cliente.nome}\n");
        double valorTotal = 0;
        foreach(Venda venda in vendasCliente)
        {
            Produto produto = produtos.Find(p => p.codigo == venda.codigoProduto);
            Console.WriteLine($"#{venda.codigo} - {venda.data} - {produto.nome} ({venda.quantidade}) - {Real(venda.valorTotal)}");
            valorTotal += venda.valorTotal;
        }
        Console.WriteLine($"\nValor total: {Real(valorTotal)}\n");
        PressioneQualquerTecla();
    }

    public static void MenuRelatorioFornecedor()
    {
        Console.Clear();
        if(fornecedores.Count == 0)
        {
            Alert("Nenhum fornecedor cadastrado!");
            return;
        }
        Fornecedor? fornecedor = MenuTool_NavegarFornecedor("Escolha um fornecedor para o relatório:");
        if(fornecedor == null) return;

        List<Compra> comprasFornecedor = compras.FindAll(c => c.codigoFornecedor == fornecedor.codigo);
        if(comprasFornecedor.Count == 0)
        {
            Alert("Nenhuma compra registrada para este fornecedor!");
            return;
        }

        Console.WriteLine($"Relatório do fornecedor: {fornecedor.nome}\n");
        double valorTotal = 0;
        foreach(Compra compra in comprasFornecedor)
        {
            Produto produto = produtos.Find(p => p.codigo == compra.codigoProduto);
            Console.WriteLine($"#{compra.codigo} - {compra.data} - {produto.nome} ({compra.quantidade}) - {Real(compra.valorTotal)}");
            valorTotal += compra.valorTotal;
        }
        Console.WriteLine($"\nValor total: {Real(valorTotal)}\n");
        PressioneQualquerTecla();
    }

    public static void MenuRelatorioGeral()
    {
        Console.Clear();
        if(vendas.Count == 0 && compras.Count == 0)
        {
            Alert("Nenhuma venda ou compra registrada!");
            return;
        }

        Console.WriteLine("Relatório Geral:\nVendas:\n");
        double valorTotalVendas = 0;
        foreach(Venda venda in vendas)
        {
            Produto produto = produtos.Find(p => p.codigo == venda.codigoProduto);
            Cliente cliente = clientes.Find(c => c.codigo == venda.codigoCliente);
            Console.WriteLine($"#{venda.codigo} - {venda.data} - {produto.nome} ({venda.quantidade}) - {cliente.nome} - {Real(venda.valorTotal)}");
            valorTotalVendas += venda.valorTotal;
        }
        Console.WriteLine($"\nValor total vendas: {Real(valorTotalVendas)}\n\nCompras:\n");

        double valorTotalCompras = 0;
        foreach(Compra compra in compras)
        {
            Produto produto = produtos.Find(p => p.codigo == compra.codigoProduto);
            Fornecedor fornecedor = fornecedores.Find(f => f.codigo == compra.codigoFornecedor);
            Console.WriteLine($"#{compra.codigo} - {compra.data} - {produto.nome} ({compra.quantidade}) - {fornecedor.nome} - {Real(compra.valorTotal)}");
            valorTotalCompras += compra.valorTotal;
        }
        Console.WriteLine($"\nValor total compras: {Real(valorTotalCompras)}\n\nSaldo final: {Real(valorTotalVendas - valorTotalCompras)}\n");
        PressioneQualquerTecla();
    }

    public static void MenuHistorico()
    {
        Console.Clear();
        if(historicos.Count == 0)
        {
            Alert("Nenhum histórico!");
            return;
        }
        Console.WriteLine("Histórico:\n");
        foreach(Historico historico in historicos)
        {
            Console.WriteLine($"#{historico.codigo} - {historico.data} - {historico.descricao}");
        }
        Console.WriteLine();
        PressioneQualquerTecla();
    }

    public static void MenuConfiguracoes()
    {
        List<string> opcoes = new List<string> { "Formatar dados", "Voltar" };
        int indice = MenuTool_Navegar("Configurações:", opcoes);

        switch(indice)
        {
            case 0:
                MenuFormatarDados();
                break;
            case -1:
            case 1:
                return;
        }
        MenuConfiguracoes();
    }

    public static void MenuFormatarDados()
    {
        bool resposta = MenuTool_Confirmar("Deseja realmente formatar os dados?");
        if(!resposta) return;

        FormatarDados();
        Alert("Dados formatados com sucesso!");
    }

    public static bool IntVerificadorPositiva(int valor)
    {
        if(valor > 0) return true;
        else if(valor == 0)
        {
            Alert("Valor inválido! ");
            return false;
        }
        else
        {
            Alert("Valor não pode ser negativo! ");
            return false;
        }
    }
    public static string MenuTool_Input(string mensagem, bool obrigatorio = true)
    {
        if(obrigatorio) Console.Write("*");
        while(true)
        {
            Console.Write(mensagem);
            string? input = Console.ReadLine();
            if(obrigatorio && (input == null || input == "")) Console.WriteLine("Campo obrigatório! ");
            else return $"{input}";
        }
    }
    public static void Alert(string mensagem)
    {
        Console.WriteLine($"{mensagem}");
        PressioneQualquerTecla();
    }
    public static void PressioneQualquerTecla()
    {
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
    public static int MenuTool_Navegar(string titulo, List<string> opcoes)
    {
        int indice = 0;
        while(true)
        {
            Console.Clear();
            Console.WriteLine(titulo);
            for(int i = 0; i < opcoes.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Console.WriteLine(opcoes[i]);
            }
            Console.WriteLine();
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if(indice > 0) indice--;
                    else indice = opcoes.Count-1;
                    break;
                case ConsoleKey.DownArrow:
                    if(indice < opcoes.Count-1) indice++;
                    else indice = 0;
                    break;
                case ConsoleKey.Enter:
                    return indice;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Backspace:
                    return -1;
                default:
                    break;
            }
        }
    }
    public static Produto? MenuTool_NavegarProduto(string titulo, bool exibirEstoque, bool exibirPreco)
    {
        int indice = 0;
        while(true)
        {
            Console.Clear();
            if(titulo != "") Console.WriteLine(titulo);
            Console.WriteLine("Escolha utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < produtos.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Console.Write($"#{produtos[i].codigo} - {produtos[i].nome}");
                if(exibirEstoque) Console.Write($" ({produtos[i].quantidadeEstoque})");
                if(exibirPreco) Console.Write($" - {Real(produtos[i].preco)}");
                Console.WriteLine();
            }
            Console.WriteLine();
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if(indice > 0) indice--;
                    else indice = produtos.Count-1;
                    break;
                case ConsoleKey.DownArrow:
                    if(indice < produtos.Count-1) indice++;
                    else indice = 0;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Backspace:
                    return null;
                case ConsoleKey.Enter:
                    Console.Clear();
                    return produtos[indice];
                default:
                    break;
            }
        }
    }
    public static Produto? MenuTool_NavegarProdutoFornecedor()
    {
        int indice = 0;
        while(true)
        {
            Console.Clear();
            Console.WriteLine("Escolha utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < produtos.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Fornecedor? fornecedor = fornecedores.Find(f => f.codigo == produtos[i].codigoFornecedor);
                string fornecedorCodigo = fornecedor != null ? fornecedor.nome : "Sem fornecedor";
                Console.WriteLine($"#{produtos[i].codigo} - {produtos[i].nome} ({fornecedorCodigo})");
            }
            Console.WriteLine();
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if(indice > 0) indice--;
                    else indice = produtos.Count-1;
                    break;
                case ConsoleKey.DownArrow:
                    if(indice < produtos.Count-1) indice++;
                    else indice = 0;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Backspace:
                    return null;
                case ConsoleKey.Enter:
                    Console.Clear();
                    return produtos[indice];
                default:
                    break;
            }
        }
    }
    public static Venda? MenuTool_NavegarVenda(string titulo)
    {
        int indice = 0;
        while(true)
        {
            Console.Clear();
            if(titulo != "") Console.WriteLine(titulo);
            Console.WriteLine("Escolha utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < vendas.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Produto produto = produtos.Find(p => p.codigo == vendas[i].codigoProduto);
                Cliente cliente = clientes.Find(c => c.codigo == vendas[i].codigoCliente);
                Console.WriteLine($"{vendas[i].data} #{vendas[i].codigo} - {produto.nome} ({vendas[i].quantidade}) - {cliente.nome} - {Real(vendas[i].valorTotal)}");
            }
            Console.WriteLine();
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if(indice > 0) indice--;
                    else indice = vendas.Count-1;
                    break;
                case ConsoleKey.DownArrow:
                    if(indice < vendas.Count-1) indice++;
                    else indice = 0;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Backspace:
                    return null;
                case ConsoleKey.Enter:
                    Console.Clear();
                    return vendas[indice];
                default:
                    break;
            }
        }
    }
    public static Compra? MenuTool_NavegarCompra(string titulo)
    {
        int indice = 0;
        while(true)
        {
            Console.Clear();
            if(titulo != "") Console.WriteLine(titulo);
            Console.WriteLine("Escolha utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < compras.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Produto produto = produtos.Find(p => p.codigo == compras[i].codigoProduto);
                Fornecedor fornecedor = fornecedores.Find(f => f.codigo == compras[i].codigoFornecedor);
                Console.WriteLine($"{compras[i].data} #{compras[i].codigo} - {produto.nome} ({compras[i].quantidade}) - {fornecedor.nome} - {Real(compras[i].valorTotal)}");
            }
            Console.WriteLine();
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if(indice > 0) indice--;
                    else indice = compras.Count-1;
                    break;
                case ConsoleKey.DownArrow:
                    if(indice < compras.Count-1) indice++;
                    else indice = 0;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Backspace:
                    return null;
                case ConsoleKey.Enter:
                    Console.Clear();
                    return compras[indice];
                default:
                    break;
            }
        }
    }
    public static Cliente? MenuTool_NavegarCliente(string titulo)
    {
        int indice = 0;
        while(true)
        {
            Console.Clear();
            if(titulo != "") Console.WriteLine(titulo);
            Console.WriteLine("Escolha utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < clientes.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Console.WriteLine($"#{clientes[i].codigo} - {clientes[i].nome}");
            }
            Console.WriteLine();
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if(indice > 0) indice--;
                    else indice = clientes.Count-1;
                    break;
                case ConsoleKey.DownArrow:
                    if(indice < clientes.Count-1) indice++;
                    else indice = 0;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Backspace:
                    return null;
                case ConsoleKey.Enter:
                    Console.Clear();
                    return clientes[indice];
                default:
                    break;
            }
        }
    }
    public static Fornecedor? MenuTool_NavegarFornecedor(string titulo)
    {
        int indice = 0;
        while(true)
        {
            Console.Clear();
            if(titulo != "") Console.WriteLine(titulo);
            Console.WriteLine("Escolha utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < fornecedores.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Console.WriteLine($"#{fornecedores[i].codigo} - {fornecedores[i].nome}");
            }
            Console.WriteLine();
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if(indice > 0) indice--;
                    else indice = fornecedores.Count-1;
                    break;
                case ConsoleKey.DownArrow:
                    if(indice < fornecedores.Count-1) indice++;
                    else indice = 0;
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Backspace:
                    return null;
                case ConsoleKey.Enter:
                    Console.Clear();
                    return fornecedores[indice];
                default:
                    break;
            }
        }
    }
    public static bool MenuTool_Confirmar(string message)
    {
        bool confirm = true;
        while(true)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine($"{(confirm ? ">" : " ")} Sim\n{(confirm ? " " : ">")} Não");
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    confirm = !confirm;
                    break;
                case ConsoleKey.Enter:
                    return confirm;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Backspace:
                    return false;
                default:
                    break;
            }
        }
    }
    public static int MenuTool_TentarConverterParaInteiro(string valor) { try { return int.Parse(valor); } catch { return 0; } }
    public static double MenuTool_TentarConverterParaDouble(string valor) { try { return double.Parse(valor); } catch { return 0; } }
    public static string HorarioAtual() => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    public static string Real(double valor) => $"R{valor.ToString("C")}";

    public static void AdicionarHistorico(string descricao)
    {
        quantidadeHistorico++;
        Historico historico = new Historico(quantidadeHistorico, HorarioAtual(), descricao);
        historicos.Add(historico);
    }

    public static void FormatarDados()
    {
        produtos.Clear();
        vendas.Clear();
        compras.Clear();
        clientes.Clear();
        fornecedores.Clear();
        historicos.Clear();
        quantidadeProdutos = 0;
        quantidadeVendas = 0;
        quantidadeCompras = 0;
        quantidadeClientes = 0;
        quantidadeFornecedores = 0;
        quantidadeHistorico = 0;
        SalvarDados();
    }
    
    public static void SalvarDados()
    {
        Dados dados = new Dados(produtos, vendas, compras, clientes, fornecedores, historicos, quantidadeProdutos, quantidadeVendas, quantidadeCompras, quantidadeClientes, quantidadeFornecedores, quantidadeHistorico);
        string json = JsonSerializer.Serialize(dados);
        File.WriteAllText(caminhoSalvamento, json);
    }

    public static void CarregarDados()
    {
        Console.WriteLine("Carregando dados...");
        if(File.Exists(caminhoSalvamento))
        {
            string json = File.ReadAllText(caminhoSalvamento);
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            Dados? dados = JsonSerializer.Deserialize<Dados>(json, options);
            if(dados != null)
            {
                produtos = dados.produtos;
                vendas = dados.vendas;
                compras = dados.compras;
                clientes = dados.clientes;
                fornecedores = dados.fornecedores;
                historicos = dados.historicos;
                quantidadeProdutos = dados.quantidadeProdutos;
                quantidadeVendas = dados.quantidadeVendas;
                quantidadeCompras = dados.quantidadeCompras;
                quantidadeClientes = dados.quantidadeClientes;
                quantidadeFornecedores = dados.quantidadeFornecedores;
                quantidadeHistorico = dados.quantidadeHistorico;
            }
        }
    }
}