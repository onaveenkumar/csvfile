using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Kernel.Pdf.Canvas.Parser;
using System.IO;
using readdatafrompdf.App_Code;
using Tesseract;

namespace readdatafrompdf
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadimagetext();
        }
        public static void ExtractAllImagesFromPDF()
        {
            string filePath = @"C:\luckyadhar.pdf";

            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                PdfReader pdfReader = new PdfReader(fs);
                PdfDocument pdfDocument = new PdfDocument(pdfReader);

                var eventListener = new ImageEventListener();
                PdfCanvasProcessor canvasProcessor = new PdfCanvasProcessor(eventListener);

                for (int pageNumber = 1; pageNumber <= pdfDocument.GetNumberOfPages(); pageNumber++)
                {
                    // this will invoke ImageEventListener
                    canvasProcessor.ProcessPageContent(pdfDocument.GetPage(pageNumber));
                }
            }
            
        }
        public static void loadimagetext()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Content/20240712164514014.jpg");
            string tessdataPath = HttpContext.Current.Server.MapPath("~/") + System.IO.Path.DirectorySeparatorChar + "tessdata";
            using (TesseractEngine engine = new TesseractEngine(tessdataPath, "eng", EngineMode.Default))
            {
                using (Pix pix = Pix.LoadFromFile(filePath))
                {
                    using (Tesseract.Page page = engine.Process(pix))
                    {
                        string resultvalue = page.GetText();
                    }
                }
            }
        }
    }
}