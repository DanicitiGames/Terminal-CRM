using System;
using System.Text.RegularExpressions;

public class Empresa
{
    public string nome { get; private set; }
    public string? cnpj { get; private set; }
    public string? telefone { get; private set; }
    public string? email { get; private set; }
    public string? endereco { get; private set; }

    public Empresa(string nome, string? cnpj = null, string? telefone = null, string? email = null, string? endereco = null)
    {
        this.nome = nome;
        this.cnpj = cnpj;
        this.telefone = telefone;
        this.email = email;
        this.endereco = endereco;
    }

    public bool definirCNPJ(string cnpj)
    {
        string numeros = Regex.Replace(cnpj, "[^0-9]", "");
        if(numeros.Length != 14 && cnpj.Length != 0) return false;
        this.cnpj = numeros;
        return true;
    }

    public bool definirTelefone(string telefone)
    {
        string numeros = Regex.Replace(telefone, "[^0-9]", "");
        if(numeros.Length < 8 && telefone.Length != 0) return false;
        this.telefone = numeros;
        return true;
    }

    public bool definirEmail(string email)
    {
        if(!email.Contains("@") && email.Length != 0) return false;
        this.email = email;
        return true;
    }

    public bool definirEndereco(string endereco)
    {
        this.endereco = endereco;
        return true;
    }

    public bool definirNome(string nome)
    {
        if(nome.Length == 0) return false;
        this.nome = nome;
        return true;
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
        
    }
}