using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Estacionamento.Web.Controllers.ClienteController;

namespace Estacionamento.Web.Helpers
{
    public static class HtmlHelper
    {
        public static IHtmlContent Pagination<T>(this IHtmlHelper helper, ResponsePagination<T> pagination, string controller) where T : class
        {
            var content = new HtmlContentBuilder()
                    .AppendHtml("<nav>")
                    .AppendHtml("<ul class=\"pagination\">")

                    .AppendHtml("<li class=\"page-item\">")
                    .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.CurrentPage - 1}&ItemsPerPage=10\">{pagination.CurrentPage}</a>")
                    .AppendHtml("</li>")

                    .AppendHtml("<li class=\"page-item\">")
                    .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.CurrentPage}&ItemsPerPage=10\">{pagination.CurrentPage}</a>")
                    .AppendHtml("</li>")

                    .AppendHtml("<li class=\"page-item\">")
                    .AppendHtml($"<a class=\"page-link\" href=\"?currentPage={pagination.CurrentPage + 1}&ItemsPerPage=10\">{pagination.CurrentPage}</a>")
                    .AppendHtml("</li>")

                    .AppendHtml("</ul>")
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
