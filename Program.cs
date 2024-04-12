using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

public class Program
{
    private static int quantidadeProdutos = 0;
    private static int quantidadeClientes = 0;
    private static int quantidadeFornecedores = 0;
    private static List<Produto> produtos = new List<Produto>();
    private static List<Cliente> clientes = new List<Cliente>();
    private static List<Fornecedor> fornecedores = new List<Fornecedor>();

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
                MenuClientes();
                break;
            case 5:
                MenuFornecedores();
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
                string novoPreco = MenuTool_Input("Novo preço: ", false).Replace(".", ",");
                double novoPrecoDouble = MenuTool_TentarConverterParaDouble(novoPreco);
                if(novoPrecoDouble > 0) produto.preco = novoPrecoDouble;
                break;
            case 3:
                produto.categoria = MenuTool_Input("Nova categoria: ", false);
                if(produto.categoria == "") produto.categoria = "Sem categoria";
                break;
            case 4:
                string novaQuantidadeEstoque = MenuTool_Input("Nova quantidade em estoque: ", false);
                int novaQuantidadeEstoqueInt = MenuTool_TentarConverterParaInteiro(novaQuantidadeEstoque);
                if(novaQuantidadeEstoqueInt > 0) produto.quantidadeEstoque = novaQuantidadeEstoqueInt;
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
            Alert("Nenhum produto cadastrado!");
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
                if(novoNome.Length != 0) cliente.nome = novoNome;
                break;
            case 1:
                string novoCPF = MenuTool_Input("Novo CPF: ", true);
                if(novoCPF.Length != 0) cliente.cpf = novoCPF;
                break;
            case 2:
                cliente.endereco = MenuTool_Input("Novo endereço: ", false);
                break;
            case 3:
                cliente.telefone = MenuTool_Input("Novo telefone: ", false);
                break;
            case 4:
                cliente.email = MenuTool_Input("Novo e-mail: ", false);
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
        List<string> opcoes = new List<string> { "Cadastrar", "Editar", "Excluir", "Listar", "Voltar" };
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
                MenuExcluirFornecedor();
                break;
            case 3:
                MenuListarFornecedores();
                break;
            case -1:
            case 4:
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
                if(novoNome.Length != 0) fornecedor.nome = novoNome;
                break;
            case 1:
                string novoCNPJ = MenuTool_Input("Novo CNPJ: ", true);
                if(novoCNPJ.Length != 0) fornecedor.cnpj = novoCNPJ;
                break;
            case 2:
                fornecedor.endereco = MenuTool_Input("Novo endereço: ", false);
                break;
            case 3:
                fornecedor.telefone = MenuTool_Input("Novo telefone: ", false);
                break;
            case 4:
                fornecedor.email = MenuTool_Input("Novo e-mail: ", false);
                break;
            case -1:
            case 5:
                return;
            default:
                break;
        }
        MenuEditarFornecedor();
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
    public static Cliente? MenuTool_NavegarCliente(string titulo)
    {
        int indice = 0;
        while(true)
        {
            Console.Clear();
            if(titulo != "") Console.WriteLine(titulo);
            Console.WriteLine("Escolha um cliente utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < clientes.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Console.WriteLine("#"+clientes[i].codigo+" - "+clientes[i].nome);
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
            Console.WriteLine("Escolha um fornecedor utilizando as setas ↑ ↓ ou aperte ← para voltar:");
            for(int i = 0; i < fornecedores.Count; i++)
            {
                if(i == indice) Console.Write(">");
                else Console.Write(" ");
                Console.WriteLine("#"+fornecedores[i].codigo+" - "+fornecedores[i].nome);
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
}