using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetVault.Common.Models;
using Microsoft.Azure.Search.Models;

namespace DatasetVault.AzureSearch.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class AzureDatasetEntry 
    {
        public string DatasetEntryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Category { get; set; }
        public bool? IsImported { get; set; }
        public DateTime? DateCreated { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.DatasetEntryId, this.Title);
        }
    }
}
