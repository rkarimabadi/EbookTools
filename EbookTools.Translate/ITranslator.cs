
namespace EbookTools.Translate
{
    public interface ITranslator
    {

        Task<string> Translate(string input);
    }
}