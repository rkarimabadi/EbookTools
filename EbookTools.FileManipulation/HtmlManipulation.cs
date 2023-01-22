using HtmlAgilityPack;
using System.Net.Http.Headers;

namespace EbookTools.FileManipulation
{
    public delegate Task<string> OnNodeVisitCallBack(string innerText);
    public class HtmlManipulation : IHtmlManipulation
    {
        public async Task<string> LoadHtmlFile(Stream fileStream)
        {
            try
            {
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                return await fileContent.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> TraverseAndDo(string htmlContent, OnNodeVisitCallBack callback)
        {
            try
            {
                var page = new HtmlDocument();
                page.LoadHtml(htmlContent);
                HtmlNode j = page.DocumentNode;
                foreach (HtmlNode node in j.ChildNodes)
                {
                    await checkNode(node, callback);
                }
                using (StringWriter writer = new StringWriter())
                {
                    page.Save(writer);
                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private async Task checkNode(HtmlNode node, OnNodeVisitCallBack callback)
        {
            foreach (HtmlNode n in node.ChildNodes)
            {
                if (n.HasChildNodes)
                {
                    await checkNode(n, callback);
                }
                else
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(n.InnerText.Trim()) && !string.IsNullOrWhiteSpace(n.InnerText.Trim()))
                            n.InnerHtml = await callback(n.InnerText);
                    }
                    catch (Exception ex)
                    {
                        n.InnerHtml = "Visit Node Error:" + ex.Message;
                    }
                }
            }
        }
    }
}