using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class Produto
{
    public int codigo { get; private set; }
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

public class Program
{
    private static int quantidadeProdutos = 0;
    private static List<Produto> produtos = new List<Produto>();

	public static void Main()
	{
        while (true) Menu();
	}

    public static void Menu()
    {
        List<string> opcoes = new List<string> { "Produtos", "Estoque", "Vendas", "Compras", "Clientes", "Fornecedores", "Relatórios", "Configurações", "Sair" };
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
                //Vendas();
                break;
            case 3:
                //Compras();
                break;
            case 4:
                //Clientes();
                break;
            case 5:
                //Fornecedores();
                break;
            case 6:
                //Relatorios();
                break;
            case 7:
                //Configuracoes();
                break;
            case 8:
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
        
        Console.WriteLine("Produto cadastrado com sucesso!\n\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
    
    public static void MenuEditarProduto()
    {
        Console.Clear();
        if(produtos.Count == 0)
        {
            Erro("Nenhum produto cadastrado!");
            return;
        }

        Produto? produto = MenuTool_NavegarProduto("Editar produto:", false, false);
        if(produto == null) return;

        List<string> opcoes = new List<string> { $"Nome: {produto.nome}", $"Descrição: {produto.descricao}", $"Preço: {produto.preco}", $"Categoria: {produto.categoria}", $"Quantidade em estoque: {produto.quantidadeEstoque}", $"Permitir vendas sem estoque? {(produto.permitirVendaSemEstoque ? "Sim" : "Não")}", "Voltar" };
        int indice = MenuTool_Navegar("Editar produto:", opcoes);

        switch(indice)
        {
            case 0:
                string novoNome = MenuTool_Input("Novo nome: ", true);
                if(novoNome.Length != 0) produto.nome = novoNome;
                break;
            case 1:
                produto.descricao = MenuTool_Input("Nova descrição: ", false);
                break;
            case 2:
                string? novoPreco = MenuTool_Input("Novo preço: ", false).Replace(".", ",");
                if(novoPreco.Length != 0) produto.preco = MenuTool_TentarConverterParaDouble(novoPreco);
                break;
            case 3:
                produto.categoria = MenuTool_Input("Nova categoria: ", false);
                if(produto.categoria == "") produto.categoria = "Sem categoria";
                break;
            case 4:
                string novaQuantidadeEstoque = MenuTool_Input("Nova quantidade em estoque: ", false);
                if(novaQuantidadeEstoque.Length != 0) produto.quantidadeEstoque = MenuTool_TentarConverterParaInteiro(novaQuantidadeEstoque);
                break;
            case 5:
                string permitirVendaSemEstoque = MenuTool_Input("Permitir vendas sem estoque? (s/n) ", false);
                produto.permitirVendaSemEstoque = permitirVendaSemEstoque.ToLower() == "s";
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
            Erro("Nenhum produto cadastrado!");
            return;
        }

        Produto? produto = MenuTool_NavegarProduto("Excluir produto:", false, false);
        if(produto == null) return;

        bool resposta = MenuTool_Confirmar($"Deseja realmente excluir este produto ({produto.nome})?");
        if(!resposta) return;
        produtos.Remove(produto);
    }
    
    public static void MenuListarProdutos()
    {
        Console.Clear();
        if(produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado!\n\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
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
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    public static void MenuEstoque()
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("Estoque:");
            Console.WriteLine("1 - Adicionar\n2 - Remover\n3 - Listar\n4 - Voltar\nEscolha uma opção: ");
            switch(Console.ReadLine())
            {
                case "1":
                    MenuAdicionarEstoque();
                    break;
                case "2":
                    //RemoverEstoque();
                    break;
                case "3":
                    //ListarEstoque();
                    break;
                case "4":
                    return;
                default:
                   break;
            }
        }
    }

    public static void MenuAdicionarEstoque()
    {
        if(produtos.Count == 0)
        {
            Console.Clear();
            Erro("Nenhum produto cadastrado!");
            return;
        }

        Produto? produto = MenuTool_NavegarProduto("Adicionar ao estoque:", true, false);
        if(produto == null) return;

        Console.WriteLine($"Produto: {produto.nome}");
        Console.Write("Quantidade a ser adicionada: ");
        string? quantidade = Console.ReadLine();
        if(quantidade == null || quantidade == "") return;

        int quantidadeInt = MenuTool_TentarConverterParaInteiro(quantidade);
        if(quantidadeInt == 0)
        {
            Erro("Valor inválido!");
            return;
        }
        if(quantidadeInt < 0)
        {
            Erro("Valor não pode ser negativo!");
            return;
        }

        produto.quantidadeEstoque += quantidadeInt;
        Console.WriteLine("Estoque atualizado com sucesso!\n\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
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

    public static void Erro(string mensagem)
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
            Console.WriteLine("Escolha um produto utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < produtos.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Console.Write("#"+produtos[i].codigo+" - "+produtos[i].nome);
                if(exibirEstoque) Console.Write(" ("+produtos[i].quantidadeEstoque+")");
                if(exibirPreco) Console.Write(" - R$"+produtos[i].preco);
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


}