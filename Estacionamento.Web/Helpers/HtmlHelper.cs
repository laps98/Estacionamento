using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Estacionamento.Domain.Pagination.PaginationHelper;

namespace Estacionamento.Web.Helpers
{
    public static class HtmlHelper
    {
        public static IHtmlContent Pagination<T>(this IHtmlHelper helper, ResponsePagination<T> pagination, string controller) where T : class
        {
            var content = new HtmlContentBuilder()
                    .AppendHtml("<nav>")
                    .AppendHtml("<ul class=\"pagination\">");
            if (pagination.HasPreviousPage)
                content.AppendHtml("<li class=\"page-item\">")
                    .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.CurrentPage - 1}&ItemsPerPage={pagination.ItemsPerPage}\">Anterior</a>")
                    .AppendHtml("</li>");
            content.AppendHtml("<li class=\"page-item\">")
                .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.CurrentPage}&ItemsPerPage={pagination.ItemsPerPage}\">{pagination.CurrentPage}</a>")
                .AppendHtml("</li>");
            if ((pagination.CurrentPage + 1) <= pagination.Total)
                content.AppendHtml("<li class=\"page-item\">")
                .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.CurrentPage + 1}&ItemsPerPage={pagination.ItemsPerPage}\">{pagination.CurrentPage + 1}</a>")
                .AppendHtml("</li>");
            if ((pagination.CurrentPage + 2) <= pagination.Total)
                content.AppendHtml("<li class=\"page-item\">")
                .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.CurrentPage + 2}&ItemsPerPage={pagination.ItemsPerPage}\">{pagination.CurrentPage + 2}</a>")
                .AppendHtml("</li>");
            if (pagination.Total > 5)
                content.AppendHtml("<li class=\"page-item\">")
                .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.Total}&ItemsPerPage={pagination.ItemsPerPage}\">{pagination.Total}</a>")
                .AppendHtml("</li>");
            if (pagination.HasNextPage)
                content.AppendHtml("<li class=\"page-item\">")
                .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.CurrentPage + 1}&ItemsPerPage={pagination.ItemsPerPage}\">Próximo</a>")
                .AppendHtml("</li>");
            content.AppendHtml("</ul>")
            .AppendHtml("</nav>");

            /*
            content.AppendHtml("<ol class='content-body'><li  class=\"page-item\">")
                .AppendHtml(helper.ActionLink("Prev", "Index", controller, new {@class = "page-link" }))
                .AppendHtml("</li>");

            */
            return content;
        }
    }
}
