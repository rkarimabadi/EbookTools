namespace EbookTools.LanguageTools
{
    public interface IFarsiNormalizer
    {
        string CharacterRefinement(string text);
        Task<string> Run(string text);
    }
}