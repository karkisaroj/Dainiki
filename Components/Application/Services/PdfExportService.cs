using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using Dainiki.Components.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace Dainiki.Components.Application.Services
{
    public class PdfExportService
    {
        // Convert HTML into structured plain text (headings, lists, paragraphs)
        private string HtmlToStructuredText(string? html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return "(No content)";

            // Headings: make them bigger by prefixing with markers
            html = Regex.Replace(html, "<h1.*?>(.*?)</h1>", m => "\n[H1] " + m.Groups[1].Value + "\n", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<h2.*?>(.*?)</h2>", m => "\n[H2] " + m.Groups[1].Value + "\n", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<h3.*?>(.*?)</h3>", m => "\n[H3] " + m.Groups[1].Value + "\n", RegexOptions.IgnoreCase);

            // Ordered list items: add numbers
            int counter = 1;
            html = Regex.Replace(html, "<ol.*?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "</ol>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<li.*?>(.*?)</li>", m => $"{counter++}. {m.Groups[1].Value}\n", RegexOptions.IgnoreCase);

            // Unordered list items: add bullet
            html = Regex.Replace(html, "<ul.*?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "</ul>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, "<li.*?>(.*?)</li>", m => "• " + m.Groups[1].Value + "\n", RegexOptions.IgnoreCase);

            // Paragraphs: add line breaks
            html = Regex.Replace(html, "<p.*?>(.*?)</p>", m => m.Groups[1].Value + "\n", RegexOptions.IgnoreCase);

            // Remove any leftover tags
            string plain = Regex.Replace(html, "<.*?>", string.Empty);

            // Decode HTML entities (&amp; → &)
            plain = System.Net.WebUtility.HtmlDecode(plain);

            // Clean up multiple newlines
            plain = Regex.Replace(plain, @"\n{2,}", "\n");

            return plain.Trim();
        }

        public byte[] GenerateJournalPdf(List<EntriesModel> entries, bool includeMood, bool includeTags, bool includeCategories)
        {
            using var doc = new PdfDocument();
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            // Normal font
            var font = new XFont("Verdana", 12, XFontStyle.Regular);
            // Heading font
            var headingFont = new XFont("Verdana", 14, XFontStyle.Bold);

            int y = 40; // vertical position

            foreach (var entry in entries)
            {
                // Print date and title
                gfx.DrawString($"{entry.Date:MMM dd, yyyy} - {entry.Title ?? "(Untitled)"}",
                    headingFont, XBrushes.Black, new XPoint(40, y));
                y += 25;

                // Convert HTML to structured text
                var content = HtmlToStructuredText(entry.Content);
                var lines = content.Split('\n');

                // Print each line separately
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // If line starts with [H1]/[H2]/[H3], use heading font
                        if (line.StartsWith("[H1]"))
                            gfx.DrawString(line.Replace("[H1]", "").Trim(), new XFont("Verdana", 16, XFontStyle.Bold), XBrushes.Black, new XPoint(40, y));
                        else if (line.StartsWith("[H2]"))
                            gfx.DrawString(line.Replace("[H2]", "").Trim(), new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(40, y));
                        else if (line.StartsWith("[H3]"))
                            gfx.DrawString(line.Replace("[H3]", "").Trim(), new XFont("Verdana", 12, XFontStyle.Bold), XBrushes.Black, new XPoint(40, y));
                        else
                            gfx.DrawString(line, font, XBrushes.Black, new XPoint(40, y));

                        y += 20;
                    }
                }

                // Print mood, tags, categories if selected
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

                // Add new page if current page is full
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