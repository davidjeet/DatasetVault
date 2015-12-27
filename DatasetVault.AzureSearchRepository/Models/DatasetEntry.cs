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
    public class AzureDatasetEntry : DatasetEntry
    {
        public DateTime? DateCreated { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.Id, this.Title);
        }
    }
}
