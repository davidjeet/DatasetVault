using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetVault.Common.Models
{
    public class DatasetEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Category { get; set; }
        public bool? IsImported { get; set; }
    }
}
