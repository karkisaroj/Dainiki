using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using Dainiki.Components.Domain.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace Dainiki.Components.Application.Services
{
    public partial class PdfExportService
    {
        [GeneratedRegex("<.*?>")]
        internal static partial Regex HtmlTagRegex();

        private static string StripHtml(string html) => HtmlTagRegex().Replace(html, string.Empty);

        public static byte[] GenerateJournalPdf(List<EntriesModel> entries, bool includeMood, bool includeTags, bool includeCategories)
        {
            using var doc = new PdfDocument();
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12, XFontStyle.Regular);

            int y = 40;

            foreach (var entry in entries)
            {
                // Title + Date
                gfx.DrawString($"{entry.Date:MMM dd, yyyy} -\n {entry.Title ?? "(Untitled)"}",
                    font, XBrushes.Black, new XPoint(40, y));
                y += 20;

                // Content (HTML stripped)
                var content = PdfExportService.StripHtml(entry.Content ?? "");
                var lines = content.Split('\n');
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        gfx.DrawString(line, font, XBrushes.Black, new XPoint(40, y));
                        y += 20;
                    }
                }

                // Optional metadata
                if (includeMood && !string.IsNullOrWhiteSpace(entry.PrimaryMood))
                {
                    gfx.DrawString($"Mood: {entry.PrimaryMood}", font, XBrushes.Black, new XPoint(40, y));
                    y += 20;
                }

                if (includeTags && !string.IsNullOrWhiteSpace(entry.Tags))
                {
                    gfx.DrawString($"Tags: {entry.Tags}", font, XBrushes.Black, new XPoint(40, y));
                    y += 20;
                }

                if (includeCategories && !string.IsNullOrWhiteSpace(entry.PhaseOfLife))
                {
                    gfx.DrawString($"Phase of Life: {entry.PhaseOfLife}", font, XBrushes.Black, new XPoint(40, y));
                    y += 20;
                }

                y += 20;

                // If new pages are added this will be used
                if (y > page.Height - 60)
                {
                    page = doc.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = 40;
                }
            }

            using var ms = new MemoryStream();
            doc.Save(ms, false);
            return ms.ToArray();
        }
    }
}