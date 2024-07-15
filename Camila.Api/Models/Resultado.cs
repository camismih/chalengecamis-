namespace Camila.Api.Models;

/// <summary>
/// Define o resultado de uma operação
/// </summary>
public record class Resultado
{
    /// <summary>
    /// Define se a operação  aconteceu com sucesso
    /// </summary>
    public bool ComSucesso { get; protected init; }
    
    /// <summary>
    /// Define a mensagem em caso de ter ocorrido um erro
    /// </summary>
    public string Erro { get; protected init; } = string.Empty;

    /// <summary>
    /// Esconde 
    /// </summary>
    protected Resultado() { }

    /// <summary>
    /// Instancia um <c>Resultado</c> com sucesso
    /// </summary>
    /// <returns>Uma instancia de <c>Resultado</c> que indica resultado</returns>
    public static Resultado Sucesso() => new Resultado() { ComSucesso = true };

    /// <summary>
    /// Instancia um <c>Resultado</c> com falha
    /// </summary>
    /// <param name="erro"></param>
    /// <returns>Uma instancia de <c>Resultado</c> que indica erro</returns>
    public static Resultado Falha(string erro) => new Resultado() { ComSucesso = false, Erro = erro };
}

public record class Resultado<T> : Resultado
{
    /// <summary>
    /// O valor do resultado
    /// </summary>
    public T Valor { get; private init; } = default!;

    /// <summary>
    /// Instancia um <c>Resultado</c> com sucesso
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="valor"></param>
    /// <returns>Um <c>Resultado</c> que indica sucesso</returns>
    public static Resultado<T> Sucesso<T>(T valor) => new() { ComSucesso = true, Valor = valor };

    /// <summary>
    /// Instancia um <c>Resultado</c> com falha
    /// </summary>
    /// <param name="erro"></param>
    /// <returns>Um <c>Resultado</c> que indica um erro</returns>
    public static new Resultado<T> Falha(string erro) => new Resultado<T>() { ComSucesso = false, Erro = erro, Valor = default(T) };
}