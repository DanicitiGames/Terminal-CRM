using System;
using System.Text.RegularExpressions;

public class Produto
{
    public string nome { get; private set; }
    public string descricao { get; private set; }

    public Produto(string nome, string descricao)
    {
        this.nome = nome;
        this.descricao = descricao;
    }

}

public class Program
{
	public static void Main()
	{
        //Empresa empresa = new Empresa("Empresa 1");
        //empresa.definirCNPJ("00.000.000/0000-00");
        //empresa.definirTelefone("(00) 0000-0000");
        //empresa.definirEmail("abc@ca.dw");
        //empresa.definirEndereco("Rua 1, 123");
        while (true) Menu();
	}

    public static void Menu()
    {
        Console.Clear();
        Console.WriteLine("1 - Produtos");
        Console.WriteLine("2 - Estoque");
        Console.WriteLine("3 - Vendas");
        Console.WriteLine("4 - Compras");
        Console.WriteLine("5 - Clientes");
        Console.WriteLine("6 - Fornecedores");
        Console.WriteLine("7 - Relatórios");
        Console.WriteLine("8 - Configurações");
        Console.WriteLine("9 - Sair");
        Console.Write("Escolha uma opção: ");
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
            Console.WriteLine("1 - Cadastrar");
            Console.WriteLine("2 - Editar");
            Console.WriteLine("3 - Excluir");
            Console.WriteLine("4 - Listar");
            Console.WriteLine("5 - Voltar");
            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine();
            switch(opcao)
            {
                case "1":
                    MenuCadastrarProduto();
                    break;
                case "2":
                    Console.WriteLine("Editar");
                    break;
                case "3":
                    Console.WriteLine("Excluir");
                    break;
                case "4":
                    Console.WriteLine("Listar");
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
        while(true)
        {
            nome = Console.ReadLine();
            if(nome != null && nome.Length != 0) break;
        }
        Console.Write("Descrição: ");
        descricao = Console.ReadLine();
        Console.Write("Preço: ");
        preco = Console.ReadLine();
        Console.Write("Categoria: (ex: Eletrônicos, Roupas, Alimentos) ");
        categoria = Console.ReadLine();
        Console.Write("Quantidade em estoque: ");
        quantidadeEstoque = Console.ReadLine();
        Console.Write("Permitir vendas sem estoque? (s/n) ");
        permitirVendaSemEstoque = Console.ReadLine();

        if(descricao == null) descricao = "";
        if(preco == null) preco = "0";
        if(categoria == null) categoria = "Sem categoria";
        if(quantidadeEstoque == null) quantidadeEstoque = "0";
        if(permitirVendaSemEstoque != "s") permitirVendaSemEstoque = "n";

        Produto produto = new Produto(nome, descricao);
        Console.WriteLine("Produto cadastrado com sucesso!");
        Console.WriteLine("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}