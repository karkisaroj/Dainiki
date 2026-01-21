using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudBlazor;

namespace Dainiki.Components.Theme
{
    public class AppTheme : MudTheme
    {
        public AppTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#0f172a",
                Secondary = "#475569",
                Tertiary = "#64748b",
                Background = "#f9fafb",
                Surface = "#ffffff",
                AppbarBackground = "#f9fafb",
                AppbarText = "#0f172a",
                DrawerBackground = "#f9fafb",
                DrawerText = "#334155",
                TextPrimary = "#0f172a",
                TextSecondary = "#64748b",
                Divider = "#e5e7eb",
                LinesDefault = "#e5e7eb",
                LinesInputs = "#d1d5db",
                Success = "#16a34a",
                Warning = "#ca8a04",
                Error = "#dc2626",
                Info = "#2563eb"
            };
            PaletteDark = new PaletteDark()
            {
                Primary = "#ffffff",
                TextPrimary = "#ffffff",
                TextSecondary = "#d1d5db",
                Background = "#0b0c0d",
                Surface = "#121314",
                AppbarText = "#ffffff",
                DrawerText = "#d1d5db",
                Divider = "#1f2937",
                LinesDefault = "#1f2937",
                LinesInputs = "#2a2f36"
            };
            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                AppbarHeight = "64px",
                DefaultBorderRadius = "8px"
            };
        }
    }
}

