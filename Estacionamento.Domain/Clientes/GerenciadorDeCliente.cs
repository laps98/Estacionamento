﻿using Estacionamento.Domain.Context;
using Estacionamento.Domain.Pagination;
using System;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Domain.Clientes;

public interface IGerenciadorDeCliente
{
    public void Save(Cliente cliente);

    public void Update(Cliente cliente);

    public Cliente Get(int id);

    public void Delete(int id);

    ResponsePagination<Cliente> Buscar(QueryFilter filter);
}

internal class GerenciadorDeCliente : IGerenciadorDeCliente
{
    private readonly IEstacionamentoContext _context;

    public GerenciadorDeCliente(IEstacionamentoContext context)
    {
        _context = context;
    }

    public void Save(Cliente cliente)
    {
        try
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public void Update(Cliente cliente)
    {
        if (cliente.Id == 0)
        {
            throw new Exception("Erro inesperado ao atualizar Cliente");
        }
        else
        {
            _context.Clientes.Update(cliente);
        }
        _context.SaveChanges();
    }

    public Cliente Get(int id)
    {
        return _context.Clientes.First(q => q.Id == id);
    }

    public void Delete(int id)
    {
        var cliente = _context.Clientes.FirstOrDefault(q => q.Id == id);
        if (cliente == null)
            throw new Exception("Este cliente não existe");

        _context.Clientes.Remove(cliente);
        _context.SaveChanges();
    }

    public ResponsePagination<Cliente> Buscar(QueryFilter filter)
    {
        var lista = _context.Clientes.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).ToList();
        var contador = _context.Clientes.Count();

        return new ResponsePagination<Cliente>(filter)
        {
            Items = lista,
            Total = contador,
        };
    }
}