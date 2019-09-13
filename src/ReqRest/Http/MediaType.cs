namespace ReqRest.Http
{

    /// <summary>
    ///     Defines an incomplete list of media type information.
    /// </summary>
    /// <remarks>
    ///     While the <see cref="System.Net.Mime.MediaTypeNames"/> class already provides a few
    ///     constants with the same value, the list is rather sparse, especially when targeting
    ///     .NET Standard 2.0. This class aims to extend that list.
    /// </remarks>
    public static class MediaType
    {

        // Include the slash in these private definitions to make the code below simpler.
        private const string Application = "application/";
        private const string Text = "text/";
        private const string Image = "image/";

        public const string ApplicationOctetStream = Application + "octet-stream";
        public const string ApplicationJson = Application + "json";
        public const string ApplicationPdf = Application + "pdf";
        public const string ApplicationRtf = Application + "rtf";
        public const string ApplicationSoapXml = Application + "soap+xml";
        public const string ApplicationJavaScript = Application + "javascript";
        public const string ApplicationZip = Application + "zip";
        public const string ApplicationWwwFormEncoded = Application + "x-www-form-urlencoded";

        public const string TextPlain = Text + "plain";
        public const string TextHtml = Text + "html";
        public const string TextCss = Text + "css";
        public const string TextJavaScript = Text + "javascript";
        public const string TextXml = Text + "xml";
        public const string TextRichText = Text + "richtext";
        public const string TextCsv = Text + "csv";

        public const string ImagePng = Image + "png";
        public const string ImageJpeg = Image + "jpeg";
        public const string ImageTiff = Image + "tiff";
        public const string ImageBmp = Image + "bmp";
        public const string ImageGif = Image + "gif";
        
    }

}
