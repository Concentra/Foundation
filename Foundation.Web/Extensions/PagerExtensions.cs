using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Foundation.Web.Paging;

namespace Foundation.Web.Extensions
{
    public static class PagerExtensions
    {
        public static MvcHtmlString PageLinks(
            this HtmlHelper html, 
            PagingInfoViewModel pagingInfoViewModel,
            Func<object, string> urlActionDelegate,
            int linksToShow = 0)
        {
            var currentPage = pagingInfoViewModel.PageNumber + 1;
            var totalPages = pagingInfoViewModel.TotalPages;
            linksToShow = (linksToShow == 0) ? totalPages : linksToShow;
            
            if (pagingInfoViewModel.TotalPages <= 1)
            {
                return MvcHtmlString.Create(string.Empty);
            }


            var result = new StringBuilder();
            var stringWriter = new StringWriter(result);
            string linkBlock = string.Empty;
                            
            using (var textWriter = new NavHtmlTextWritter(stringWriter))
            {
                {
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "pagination");
                    textWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
                    {

                        if (currentPage > 1)
                        {
                            pagingInfoViewModel.PageNumber = 1;
                            PageItem(textWriter, BasePagingExtensions.CreatePageLink(urlActionDelegate, pagingInfoViewModel, "First"));


                            pagingInfoViewModel.PageNumber = currentPage - 1;
                            PageItem(textWriter, BasePagingExtensions.CreatePageLink(urlActionDelegate, pagingInfoViewModel, "Previouse"));
                        }

                        // create page links
                        int start = 1;
                        int end = totalPages;
                        start = PagerPounds(linksToShow, totalPages, currentPage, start, ref end);

                        for (int i = start; i <= end; i++)
                        {
                            pagingInfoViewModel.PageNumber = i;
                            if (i == currentPage)
                            {
                                PageItem(textWriter, BasePagingExtensions.CreatePageLink(urlActionDelegate, pagingInfoViewModel, i.ToString()), "active");
                            }
                            else
                            {
                                PageItem(textWriter, BasePagingExtensions.CreatePageLink(urlActionDelegate, pagingInfoViewModel, i.ToString()));
                            }

                        }


                        // create 'Next >>' link
                        if (currentPage < totalPages)
                        {
                            pagingInfoViewModel.PageNumber = currentPage + 1;
                            PageItem(textWriter, BasePagingExtensions.CreatePageLink(urlActionDelegate, pagingInfoViewModel, "Next"));

                            pagingInfoViewModel.PageNumber = totalPages;
                            PageItem(textWriter, BasePagingExtensions.CreatePageLink(urlActionDelegate, pagingInfoViewModel, "Last"));
                        }


                        textWriter.RenderEndTag();
                    }
                }
            }


            return MvcHtmlString.Create(result.ToString());
        }

        private static int PagerPounds(int linksToShow, int totalPages, int currentPage, int start, ref int end)
        {
            if (totalPages > linksToShow)
            {
                if (currentPage > (linksToShow/2))
                {
                    start = (currentPage - (linksToShow/2)) + 1;
                    end = start + linksToShow - 1;
                }
                else
                {
                    end = linksToShow;
                }

                if (end > totalPages)
                {
                    end = totalPages;
                    start = end - linksToShow + 1;
                }
            }
            return start;
        }

        private static void PageItem(NavHtmlTextWritter textWriter, MvcHtmlString linkBlock, string cssClass = "")
        {
            if (cssClass != string.Empty)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
            }

            textWriter.RenderBeginTag(HtmlTextWriterTag.Li);
            textWriter.Write(linkBlock);
            textWriter.RenderEndTag();
        }
      
    }
}
