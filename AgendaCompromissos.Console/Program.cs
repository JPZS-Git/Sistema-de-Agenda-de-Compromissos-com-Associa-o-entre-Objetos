﻿using AgendaCompromissos.Modelo;
using System.Globalization;

CultureInfo culturaBrasileira = new("pt-BR");

Console.WriteLine("===== Agenda de Compromissos =====\n");

string nome = string.Empty;

while (string.IsNullOrWhiteSpace(nome))
{
    Console.Write("Digite o nome do usuário: ");
    nome = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrWhiteSpace(nome))
    {
        Console.WriteLine("O nome não pode estar em branco.");
    }
}

Usuario usuario = new Usuario(nome);

while (true) 
{
    DateTime data;

    while (true)
    {
        Console.WriteLine("Informe a data do compromisso (dd/MM/yyyy):");
        string dataCompromisso = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(dataCompromisso))
        { 
            Console.WriteLine("A data deve ser preenchida.");
            continue;
        }

        bool valido = DateTime.TryParseExact(
                    dataCompromisso,
                    "dd/MM/yyyy",
                    culturaBrasileira,
                    System.Globalization.DateTimeStyles.None,
                    out data);

        if (!valido) 
        {
            Console.WriteLine("Formato inválido. Use o formato dd/MM/yyyy.\n");
            continue;
        }

        break;
    }

    TimeSpan hora;

    while (true)
    {
        Console.WriteLine("Informe a hora do compromisso (HH:mm):");
        string horaCompromisso = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(horaCompromisso))
        { 
            Console.WriteLine("A hora deve ser preenchida.");
            continue;
        }

        bool valido = TimeSpan.TryParseExact(
                    horaCompromisso,
                    "hh\\:mm",
                    culturaBrasileira,
                    System.Globalization.TimeSpanStyles.None,
                    out hora);

        if (!valido) 
        {
            Console.WriteLine("Formato inválido. Use o formato HH:mm.\n");
            continue;
        }

        break;
    }

    string descricao, nomelocal;

    while(true) 
    {
        Console.WriteLine("Informe a descrição do compromisso:");
        descricao = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(descricao))
        { 
            Console.WriteLine("A descrição deve ser preenchida.");
        } else
        {
            break;
        }
    }

    while (true)
    {
        Console.WriteLine("Informe o nome do local do compromisso:");
        nomelocal = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nomelocal))
        { 
            Console.WriteLine("O nome do local deve ser preenchido.");
        } else
        {
            break;
        }
    }

    int capacidade;

    while (true) 
    {
        Console.WriteLine("Informe a capacidade do local:");
        string entradaCompromisso = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(entradaCompromisso))
        { 
            Console.WriteLine("A capacidade deve ser preenchida.");
            continue;
        }
        if (!int.TryParse(entradaCompromisso, out capacidade) )   
        {
            Console.WriteLine("O valor deve ser um numero.");
        } else
        {
            break;
        }
    }

    Local local;

    try
    {
        local = new Local(nomelocal, capacidade);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro ao criar o local: {ex.Message}");
    }


    Compromisso compromisso;

    try
    {
        local = new Local(nomelocal, capacidade);
        compromisso = new Compromisso(data, hora, descricao, usuario, local);
        usuario.AdicionarCompromisso(compromisso);
    } 
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro ao criar compromisso: {ex.Message}\n");
        Console.WriteLine("Insira novamente os dados do compromisso");
        continue;
    }

    while (true)
    {

        Console.WriteLine("Deseja adicionar um participante? (s/n)");
        string resposta = Console.ReadLine()?.ToLower() ?? "n";

        if (resposta == "n") break;

        if (resposta.ToLower() == "s")
        {
            Console.WriteLine("Informe o nome do participante:");
            string nomeParticipante = Console.ReadLine() ?? string.Empty;

        Participante participante = new Participante { Nome = nomeParticipante };
        try
        {
            compromisso.AdicionarParticipante(participante);
        } catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao adicionar participante: {ex.Message}");
            break;
        }
        }
        else
        {
            Console.WriteLine("Resposta inválida.");
        }

    }

    while (true)
    {
        Console.WriteLine("Deseja adicionar uma anotação? (s/n)");
        string resposta = Console.ReadLine()?.ToLower() ?? "n";

        if (resposta == "n") break;

        if (resposta.ToLower() == "s")
        {
            Console.WriteLine("Informe a anotação:");
            string anotacao = Console.ReadLine() ?? string.Empty;

        try
        {
            compromisso.AdicionarAnotacao(anotacao);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao adicionar anotação: {ex.Message}");
        }
        } 
        else
        {
            Console.WriteLine("Resposta inválida.");
        }

    }

    Console.WriteLine("\n===== Compromisso Criado =====");

    Console.WriteLine(usuario);

    Console.WriteLine("\nDeseja adicionar outro compromisso? (s/n)");
    string novoCompromisso = Console.ReadLine()?.ToLower() ?? "n";

    if (novoCompromisso == "n")
    {
        Console.WriteLine("Saindo...");
        break;
    }

    if (novoCompromisso != "s")
    {
        Console.WriteLine("Resposta inválida. Encerrando por segurança.");
        break;
    }

}