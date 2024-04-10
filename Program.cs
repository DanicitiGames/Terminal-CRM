using System;
using System.Collections.Generic;
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
        Console.Clear();
        Console.Write("1 - Produtos\n2 - Estoque\n3 - Vendas\n4 - Compras\n5 - Clientes\n6 - Fornecedores\n7 - Relatórios\n8 - Configurações\n9 - Sair\nEscolha uma opção: ");
        string opcao = Console.ReadLine()+"";
        switch(opcao)
        {
            case "1":
                MenuProdutos();
                break;
            case "2":
                //Estoque();
                break;
            case "3":
                //Vendas();
                break;
            case "4":
                //Compras();
                break;
            case "5":
                //Clientes();
                break;
            case "6":
                //Fornecedores();
                break;
            case "7":
                //Relatorios();
                break;
            case "8":
                //Configuracoes();
                break;
            case "9":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }

    public static void MenuProdutos()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("1 - Cadastrar\n2 - Editar\n3 - Excluir\n4 - Listar\n5 - Voltar\nEscolha uma opção: ");
            string? opcao = Console.ReadLine();
            switch(opcao)
            {
                case "1":
                    MenuCadastrarProduto();
                    break;
                case "2":
                    MenuEditarProduto();
                    break;
                case "3":
                    MenuExcluirProduto();
                    break;
                case "4":
                    MenuListarProdutos();
                    break;
                case "5":
                    return;
                default:
                    break;
            }
        }
    }

    public static void MenuCadastrarProduto()
    {
        Console.Clear();
        Console.WriteLine("Cadastrar Produto:");
        string? nome, descricao, preco, categoria, quantidadeEstoque, permitirVendaSemEstoque;
        Console.Write("Nome: ");
        while(true)
        {
            nome = Console.ReadLine();
            if(nome != null && nome.Length != 0) break;
        }
        Console.Write("Descrição: ");
        descricao = Console.ReadLine()+"";
        Console.Write("Preço: ");
        preco = ("0"+Console.ReadLine()).Replace(".", ",");
        Console.Write("Categoria: (ex: Eletrônicos, Roupas, Alimentos) ");
        categoria = Console.ReadLine();
        Console.Write("Quantidade em estoque: ");
        quantidadeEstoque = Console.ReadLine();
        Console.Write("Permitir vendas sem estoque? (s/n) ");
        permitirVendaSemEstoque = ""+Console.ReadLine();

        if(preco == "") preco = "0";
        if(categoria == "" || categoria == null) categoria = "Sem categoria";
        if(quantidadeEstoque == "") quantidadeEstoque = "0";
        if(permitirVendaSemEstoque.ToLower() != "s") permitirVendaSemEstoque = "n";

        quantidadeProdutos++;
        Produto produto = new Produto(quantidadeProdutos, nome, descricao, double.Parse(0+preco), categoria, int.Parse(0+quantidadeEstoque), permitirVendaSemEstoque == "s");
        produtos.Add(produto);
        
        Console.WriteLine("Produto cadastrado com sucesso!\n\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
    
    public static void MenuEditarProduto()
    {
        Console.Clear();
        Console.WriteLine("Editar Produto:");
        if(produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado!\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Produtos:");
        foreach(Produto produto in produtos)
        {
            if(produto.descricao != "") Console.WriteLine($"#{produto.codigo} - {produto.nome} ({produto.descricao})");
            else Console.WriteLine($"#{produto.codigo} - {produto.nome}");
        }

        Console.Write("\nDigite o código do produto que deseja editar (deixe em branco para voltar): ");
        string? codigo = Console.ReadLine();
        if(codigo == null || codigo == "") return;
        Produto? produtoEditado = produtos.Find(produto => produto.codigo == int.Parse(codigo));
        if(produtoEditado == null)
        {
            Console.WriteLine("Produto não encontrado!\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }
        Console.Clear();
        Console.Write($"0-Voltar\n1-Nome: {produtoEditado.nome}\n2-Descrição: {produtoEditado.descricao}\n3-Preço: {produtoEditado.preco}\n4-Categoria: {produtoEditado.categoria}\n5-Quantidade em estoque: {produtoEditado.quantidadeEstoque}\n6-Permitir vendas sem estoque? {(produtoEditado.permitirVendaSemEstoque ? "Sim" : "Não")}");
        Console.Write("\n\nQual campo deseja editar? (ex: 1, 2, 3...) ");
        string? campo = Console.ReadLine();
        if(campo == null || campo == "") return;
        switch(campo)
        {
            case "1":
                Console.Write("Novo nome: ");
                string? novoNome = Console.ReadLine();
                if(novoNome != null && novoNome.Length != 0) produtoEditado.nome = novoNome;
                break;
            case "2":
                Console.Write("Nova descrição: ");
                produtoEditado.descricao = ""+Console.ReadLine();
                break;
            case "3":
                Console.Write("Novo preço: ");
                string? novoPreco = ("0"+Console.ReadLine()).Replace(".", ",");
                if(novoPreco != null && novoPreco.Length != 0) produtoEditado.preco = double.Parse(novoPreco);
                break;
            case "4":
                Console.Write("Nova categoria: ");
                produtoEditado.categoria = ""+Console.ReadLine();
                if(produtoEditado.categoria == "") produtoEditado.categoria = "Sem categoria";
                break;
            case "5":
                Console.Write("Nova quantidade em estoque: ");
                string novaQuantidadeEstoque = "0"+Console.ReadLine();
                if(novaQuantidadeEstoque != null && novaQuantidadeEstoque.Length != 0) produtoEditado.quantidadeEstoque = int.Parse(novaQuantidadeEstoque);
                break;
            case "6":
                Console.Write("Permitir vendas sem estoque? (s/n) ");
                string permitirVendaSemEstoque = ""+Console.ReadLine();
                if(permitirVendaSemEstoque.ToLower() == "s") produtoEditado.permitirVendaSemEstoque = true;
                else produtoEditado.permitirVendaSemEstoque = false;
                break;
            default:
                break;
        }
    }

    public static void MenuExcluirProduto()
    {
        Console.Clear();

        if(produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado!\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Produtos:");

        foreach(Produto produto in produtos)
        {
            if(produto.descricao != "") Console.WriteLine($"#{produto.codigo} - {produto.nome} ({produto.descricao})");
            else Console.WriteLine($"#{produto.codigo} - {produto.nome}");
        }
        Console.Write("\nDigite o código do produto que deseja excluir (deixe em branco para voltar): ");
        string? codigo = Console.ReadLine();
        if(codigo == null || codigo == "") return;
        Produto? produtoExcluido = produtos.Find(produto => produto.codigo == int.Parse(codigo));
        if(produtoExcluido == null)
        {
            Console.WriteLine("Produto não encontrado!\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
            return;
        }
        produtos.Remove(produtoExcluido);
        Console.WriteLine("Produto excluído com sucesso!\n\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
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
}