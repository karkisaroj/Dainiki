using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using Dainiki.Components.Domain.Models;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace Dainiki.Components.Application.Services
{
    public partial class PdfExportService
    {
        public static byte[] GenerateJournalPdf(List<EntriesModel> entries, bool includeMood, bool includeTags, bool includeCategories)
        {
            using var doc = new PdfDocument();
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            // Fonts
            var headerFont = new XFont("Arial", 16, XFontStyle.Bold);
            var titleFont = new XFont("Arial", 12, XFontStyle.Bold);
            var bodyFont = new XFont("Arial", 11, XFontStyle.Regular);
            var metaFont = new XFont("Arial", 10, XFontStyle.Italic);
            var h1Font = new XFont("Arial", 14, XFontStyle.Bold);
            var h2Font = new XFont("Arial", 12, XFontStyle.Bold);
            var h3Font = new XFont("Arial", 11, XFontStyle.Bold);

            int marginLeft = 40;
            int y = 60;

            // Document header
            gfx.DrawString("Dainiki PDF", headerFont, XBrushes.DarkSlateGray, new XPoint(marginLeft, 30));

            foreach (var entry in entries)
            {
                // Entry Title + Date
                gfx.DrawString($"{entry.Date:MMM dd, yyyy} \n - {entry.Title ?? "(Untitled)"}",
                    titleFont, XBrushes.Black, new XPoint(marginLeft, y));
                y += 25;

                // Parse HTML content
                var html = entry.Content ?? "";
                var docHtml = new HtmlDocument();
                docHtml.LoadHtml(html);

                foreach (var node in docHtml.DocumentNode.DescendantsAndSelf())
                {
                    if (node.NodeType != HtmlNodeType.Element && node.NodeType != HtmlNodeType.Text)
                        continue;

                    string text = node.InnerText.Trim();
                    if (string.IsNullOrWhiteSpace(text))
                        continue;

                    XFont font = bodyFont;
                    int indent = 0;

                    switch (node.Name)
                    {
                        case "h1":
                            font = h1Font;
                            y += 6;
                            break;
                        case "h2":
                            font = h2Font;
                            y += 4;
                            break;
                        case "h3":
                            font = h3Font;
                            y += 2;
                            break;
                        case "ul":
                        case "ol":
                            continue; // handled by <li>
                        case "li":
                            text = "• " + text;
                            indent = 20;
                            break;
                        case "br":
                            y += 10;
                            continue;
                        case "p":
                            y += 4;
                            break;
                    }

                    gfx.DrawString(text, font, XBrushes.Black, new XPoint(marginLeft + indent, y));
                    y += 18;

                    // Page overflow
                    if (y > page.Height - 60)
                    {
                        page = doc.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        y = 60;
                    }
                }

                // Optional metadata
                if (includeMood && !string.IsNullOrWhiteSpace(entry.PrimaryMood))
                {
                    gfx.DrawString($"Mood: {entry.PrimaryMood}", metaFont, XBrushes.Gray, new XPoint(marginLeft, y));
                    y += 16;
                }

                if (includeTags && !string.IsNullOrWhiteSpace(entry.Tags))
                {
                    gfx.DrawString($"Tags: {entry.Tags}", metaFont, XBrushes.Gray, new XPoint(marginLeft, y));
                    y += 16;
                }

                if (includeCategories && !string.IsNullOrWhiteSpace(entry.PhaseOfLife))
                {
                    gfx.DrawString($"Phase of Life: {entry.PhaseOfLife}", metaFont, XBrushes.Gray, new XPoint(marginLeft, y));
                    y += 16;
                }

                // Separator
                y += 10;
                gfx.DrawLine(XPens.LightGray, marginLeft, y, page.Width - marginLeft, y);
                y += 20;
            }

            using var ms = new MemoryStream();
            doc.Save(ms, false);
            return ms.ToArray();
        }
    }
}