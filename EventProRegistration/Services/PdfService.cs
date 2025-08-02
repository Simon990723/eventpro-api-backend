using EventProRegistration.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EventProRegistration.Services
{
    public class PdfService
    {
        public static byte[] GenerateInvoicePdf(Invoice invoice)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("EventPro Invoice / Receipt")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(col =>
                        {
                            col.Spacing(20);
                            
                            col.Item().Text($"Invoice Number: {invoice.InvoiceNumber}").Bold();
                            col.Item().Text($"Issued Date: {invoice.IssuedDate:yyyy-MM-dd}");
                            col.Item().Text($"Status: {invoice.Status}").Bold();
                            
                            col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                            col.Item().Text(text =>
                            {
                                text.Span("Billed To:\n").SemiBold();
                                text.Span(invoice.Registrant.Name);
                                text.Span("\n");
                                text.Span(invoice.Registrant.Email);
                            });
                            
                            col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3); 
                                    columns.RelativeColumn(1); 
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("Description").Bold();
                                    header.Cell().AlignRight().Text("Amount").Bold();
                                });

                                table.Cell().Text($"Ticket for: {invoice.Registrant.Event.Name}");
                                table.Cell().AlignRight().Text($"${invoice.Amount:N2}"); 
                            });
                            
                            col.Item().AlignRight().Text($"Total: ${invoice.Amount:N2}").Bold().FontSize(16);
                        });
                    
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Thank you for your registration!");
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}