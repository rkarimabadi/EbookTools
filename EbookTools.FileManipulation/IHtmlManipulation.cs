
namespace EbookTools.FileManipulation
{
    public interface IHtmlManipulation
    {
        Task<string> LoadHtmlFile(Stream fileStream);
        Task<string> TraverseAndDo(string htmlContent, OnNodeVisitCallBack callback);
    }
}