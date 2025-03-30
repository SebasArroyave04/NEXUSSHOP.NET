using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prototipo_web.Models;
using prototipo_web.Repositorio;

namespace prototipo_web.Controllers
{
    public class CpdfController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly IRepositorioPDF repositorioPDF;

        public CpdfController(IConfiguration configuration, IRepositorioPDF repositorioPDF)
        {
            this.configuration = configuration;
            this.repositorioPDF = repositorioPDF;
        }

        public IActionResult ListadoContactosPdf()
        {
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Título del documento
            document.Add(new Paragraph("Listado de Contactanos")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(3, true); // 4 columnas
            table.AddHeaderCell("Nombre");
            table.AddHeaderCell("Correo");
            table.AddHeaderCell("Mensaje");

            // Llenar la tabla con datos
            ContactanosModel pdfcontactanos = new ContactanosModel();
            var personas = repositorioPDF.HacerPDF2(pdfcontactanos);
            foreach (var persona in personas)
            {
                table.AddCell(persona.Nombre);
                table.AddCell(persona.Correo);
                table.AddCell(persona.Mensaje);
            }

            document.Add(table);
            document.Close();

            byte[] pdfBytes = stream.ToArray();
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoPersonas.pdf");
            return File(pdfBytes, "application/pdf");
        }
    }
}
