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
    public class PrPDFController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IRepositorioPDF repositorioPDF;

        public PrPDFController(IConfiguration configuration, IRepositorioPDF repositorioPDF)
        {
            this.configuration = configuration;
            this.repositorioPDF = repositorioPDF;
        }
        public IActionResult ListadoProveedoresPdf()
        {
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            iText.Layout.Document document = new iText.Layout.Document(pdf);

            // Título del documento
            document.Add(new Paragraph("Listado de Proveedores Registrados")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(9, true); // 4 columnas
            table.AddHeaderCell("Nombres");
            table.AddHeaderCell("Identificacion");
            table.AddHeaderCell("Empresa");
            table.AddHeaderCell("CorreoEmpresa");
            table.AddHeaderCell("RUT");
            table.AddHeaderCell("Direccion");
            table.AddHeaderCell("Telefono");
            table.AddHeaderCell("Correoprov");
            table.AddHeaderCell("Comentarios");

            // Llenar la tabla con datos
            ProveedoresModel pdfUsuario = new ProveedoresModel();
            var personas = repositorioPDF.HacerPDF1(pdfUsuario);
            foreach (var persona in personas)
            {
                table.AddCell(persona.Nombres);
                table.AddCell(persona.Identificacion.ToString());
                table.AddCell(persona.Empresa);
                table.AddCell(persona.CorreoEmpresa);
                table.AddCell(persona.RUT.ToString());
                table.AddCell(persona.Direccion);
                table.AddCell(persona.Telefono);
                table.AddCell(persona.Correoprov);
                table.AddCell(persona.Comentarios);
            }

            document.Add(table);
            document.Close();

            byte[] pdfBytes = stream.ToArray();
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoPersonas.pdf");
            return File(pdfBytes, "application/pdf");
        }
    }
}

