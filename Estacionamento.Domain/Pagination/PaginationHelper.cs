﻿using System;
using System.Collections;
using System.Linq;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Domain.Pagination;

public static class PaginationHelper
{
    public record ResponsePagination<T> : IEnumerable<T> where T : class
    {
        public ResponsePagination(QueryFilter queryFilter)
        {
            CurrentPage = queryFilter.CurrentPage;
        }

        public List<T> Items { get; set; }
        public int Total { get; set; }
        public int CurrentPage { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < Total;

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public ResponsePagination<T> Buscar(IQueryable<T> query, QueryFilter filter)
        {
            var lista = query.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage).ToList();
            var contador = lista.Count();

            return new ResponsePagination<T>(filter)
            {
                Items = lista,
                Total = contador,
            };
        }
    }
}

public static class PaginationHelperII
{
    public static ResponsePagination<T> Paginate<T>(this IQueryable<T> query, QueryFilter filter)
    {
        return new ResponsePagination<T>(query, filter);
    }

    internal static ResponsePagination Paginate(this IQueryable query, QueryFilter filter)
    {
        return (ResponsePagination)Activator.CreateInstance(typeof(ResponsePagination<>)!.MakeGenericType(query.ElementType), query, filter);
    }
}
