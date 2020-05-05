using AngleSharp.Html.Dom;

namespace DLLParcer.Core {
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
