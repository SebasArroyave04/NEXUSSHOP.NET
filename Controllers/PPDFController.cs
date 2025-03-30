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
    public class PPDFController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IRepositorioPDF repositorioPDF;

        public PPDFController(IConfiguration configuration, IRepositorioPDF repositorioPDF)
        {
            this.configuration = configuration;
            this.repositorioPDF = repositorioPDF;
        }

        public IActionResult ListadoProductosPdf()
        {
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Título del documento
            document.Add(new Paragraph("Listado de Usuarios Registrados")
                .SetFontSize(18)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));

            //   tabla con encabezados
            Table table = new Table(10, true); // 4 columnas
            table.AddHeaderCell("codigo");
            table.AddHeaderCell("nombre");
            table.AddHeaderCell("descripcion");
            table.AddHeaderCell("precio");
            table.AddHeaderCell("unidades");
            table.AddHeaderCell("estado");
            table.AddHeaderCell("urlimage");
            table.AddHeaderCell("marca");
            table.AddHeaderCell("modelo");
            table.AddHeaderCell("accesorios");

            // Llenar la tabla con datos
            ProductosModel pdfProductos = new ProductosModel();
            var personas = repositorioPDF.HacerPDF3(pdfProductos);
            foreach (var persona in personas)
            {
                table.AddCell(persona.codigo);
                table.AddCell(persona.nombre);
                table.AddCell(persona.descripcion);
                table.AddCell(persona.precio);
                table.AddCell(persona.unidades);
                table.AddCell(persona.estado);
                table.AddCell(persona.urlimage);
                table.AddCell(persona.marca);
                table.AddCell(persona.modelo);
                table.AddCell(persona.accesorios);
            }

            document.Add(table);
            document.Close();

            byte[] pdfBytes = stream.ToArray();
            Response.Headers.Add("Content-Disposition", "inline; filename=ListadoPersonas.pdf");
            return File(pdfBytes, "application/pdf");
        }
    }
}
