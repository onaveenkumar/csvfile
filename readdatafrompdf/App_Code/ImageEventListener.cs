using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Xobject;
using System.IO;

namespace readdatafrompdf.App_Code
{
    public class ImageEventListener : IEventListener
    {
        public void EventOccurred(IEventData eventData, EventType type)
        {
            if (eventData is ImageRenderInfo imageRenderInfo)
            {
                try
                {
                    string extractImagToDir = HttpContext.Current.Server.MapPath("~/Content/");

                    if (imageRenderInfo.GetImage() != null)
                    {
                        PdfImageXObject imageXObject = imageRenderInfo.GetImage();
                        File.WriteAllBytes(extractImagToDir + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg", imageXObject.GetImageBytes());
                    }
                }
                catch (Exception ex)
                {
                    //LogError(ex.Message);
                }
            }
        }
        public ICollection<EventType> GetSupportedEvents()
        {
            return null;
        }
    }
}