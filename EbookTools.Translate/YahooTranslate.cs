using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EbookTools.Translate
{
    public class YahooTranslate : ITranslator
    {
        private readonly HttpClient HttpClient;
        public YahooTranslate(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        public async Task<string> Translate(string resource)
        {
            //  string fromCulture = from.Name;
            //  string toCulture = to.Name;
            //  string translationMode = string.Concat(fromCulture, "_", toCulture);

            string url = String.Format("http://babelfish.yahoo.com/translate_txt?lp={0}&tt=urltext&intl=1&doit=done&urltext={1}", "en_ar", HttpUtility.UrlEncode(resource));
            string page = await HttpClient.GetStringAsync(url);

            int start = page.IndexOf("<div style=\"padding:0.6em;\">") + "<div style=\"padding:0.6em;\">".Length;
            int finish = page.IndexOf("</div>", start);
            string retVal = page.Substring(start, finish - start);
            return retVal;

        }
    }
}
