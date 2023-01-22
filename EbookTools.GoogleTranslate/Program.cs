using EbookTools.FileManipulation;
using EbookTools.GoogleTranslate;
using EbookTools.LanguageTools;
using EbookTools.Translate;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ITranslator, Translator>();
builder.Services.AddScoped<IHtmlManipulation, HtmlManipulation>();
builder.Services.AddScoped<IFarsiNormalizer, FarsiNormalizer>();
await builder.Build().RunAsync();
