using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Pagination;

public class QueryFilter
{

    public string Search { get; set; }

    public string Predicate { get; set; }

    public bool Reverse { get; set; }

    public int ItemsPerPage { get; set; } = 10;


    public int CurrentPage { get; set; } = 1;

}
