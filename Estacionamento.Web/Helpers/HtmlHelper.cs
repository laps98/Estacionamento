﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Estacionamento.Web.Controllers.ClienteController;

namespace Estacionamento.Web.Helpers
{
    public static class HtmlHelper
    {
        public static IHtmlContent Pagination<T>(this IHtmlHelper helper, ResponsePagination<T> pagination) where T : class
        {
            var content = new HtmlContentBuilder()
                    .AppendHtml("<ol class='content-body'><li>")
                    .AppendHtml(helper.ActionLink("Home", "Index", "Home"))
                    .AppendHtml("</li>");


            content.AppendHtml("O total da listagem é :" + pagination.Total);
            content.AppendHtml("A pagina atual é :" + pagination.CurrentPage);

            return content;
        }
    }
}