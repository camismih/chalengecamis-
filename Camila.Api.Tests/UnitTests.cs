﻿using Camila.Api.Data;

namespace Camila.Api.Tests;

public class UnitTests
{
    [Fact]
    public void Depositar_Deve_Atualizar_Saldo()
    {
        var saldo = 1000M;
        var deposito = 100M;

        var cliente = Cliente.Create("Camila Marinho", 12345, saldo);

        cliente.Depositar(deposito);

        Assert.Equal(saldo + deposito, cliente.Saldo);
    }

    [Fact]
    public void Sacar_Deve_Atualizar_Saldo()
    {
        var saldo = 1000M;
        var deposito = 100M;

        var cliente = Cliente.Create("Camila Marinho", 12345, saldo);

        cliente.Sacar(deposito);

        Assert.Equal(saldo - deposito, cliente.Saldo);
    }
}