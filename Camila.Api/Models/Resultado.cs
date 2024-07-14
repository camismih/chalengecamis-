namespace Camila.Api.Models;

public record class Resultado
{
    public bool ComSucesso { get; protected init; }
    public string Erro { get; protected init; } = string.Empty;

    public Resultado() { }

    public static Resultado Sucesso() => new Resultado() { ComSucesso = true };
    public static Resultado Falha(string erro) => new Resultado() { ComSucesso = false, Erro = erro };
}

public record class Resultado<T> : Resultado
{
    public T Valor { get; private init; }

    public static Resultado<T> Sucesso(T valor) => new Resultado<T>() { ComSucesso = true, Valor = valor };
    public static Resultado<T> Falha(string erro) => new() { ComSucesso = false, Erro = erro };    
}