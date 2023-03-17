using Microsoft.AspNetCore.Mvc;
using PCFSolution.Models;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using System.Diagnostics;

namespace PCFSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult CreatePDFDocument()
        {
            string filepath = @"Input.bmp";

            //Create a new PDF document

            PdfDocument doc = new PdfDocument();

            //Add a page to the document

            PdfPage page = doc.Pages.Add();

            //Create PDF graphics for the page

            PdfGraphics graphics = page.Graphics;

            //Load the image from the disk

            FileStream imageStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            PdfBitmap image = PdfBitmapExtension.CreatePdfBitmap(imageStream);

            //Draw the image

            graphics.DrawImage(image, 0, 0);

            MemoryStream ms = new MemoryStream();
            //Save and close the document.            
            doc.Save(ms);
            doc.Close();

            return File(ms.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Sample.pdf");
        }

    }
}