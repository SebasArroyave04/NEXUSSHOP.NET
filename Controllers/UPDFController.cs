using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prototipo_web.Models;
using prototipo_web.Repositorio;

namespace prototipo_web.Controllers
{
    public class UPDFController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IRepositorioPDF repositorioPDF;

        public UPDFController(IConfiguration configuration, IRepositorioPDF repositorioPDF)
        {
            this.configuration = configuration;
            this.repositorioPDF = repositorioPDF;
        }
        public IActionResult ListadoUsuarioPdf()
        {
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            iText.Layout.Document document = new iText.Layout.Document(pdf);

            // Título del documento
            document.Add(new Paragraph("Listado de Usuarios Registrados")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(7, true); // 4 columnas
            table.AddHeaderCell("Identificacion");
            table.AddHeaderCell("Nombres");
            table.AddHeaderCell("Apellidos");
            table.AddHeaderCell("FNacimiento");
            table.AddHeaderCell("SSexo");
            table.AddHeaderCell("Correo");
            table.AddHeaderCell("Contrasena");

            // Llenar la tabla con datos
            RegistrarUsuario pdfUsuario = new RegistrarUsuario();
            var personas = repositorioPDF.HacerPDF(pdfUsuario);
            foreach (var persona in personas)
            {
                table.AddCell(persona.Identificacion);
                table.AddCell(persona.Nombres);
                table.AddCell(persona.Apellidos);
                table.AddCell(persona.FNacimiento);
                table.AddCell(persona.SSexo.ToString());
                table.AddCell(persona.Correo);
                table.AddCell(persona.Contrasena);
            }

            document.Add(table);
            document.Close();

            byte[] pdfBytes = stream.ToArray();
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoPersonas.pdf");
            return File(pdfBytes, "application/pdf");
        }
    }
}
