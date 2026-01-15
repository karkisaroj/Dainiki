using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dainiki.Components.Models
{
    public class EntriesModel
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        
        public string PrimaryMood { get; set; } = string.Empty;

        public List<string?> SecondaryMoods { get; set; } = [];

        public string PhaseOfLife { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;


    }
}
