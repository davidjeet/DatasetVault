using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetVault.AzureSearch.Models;
using DatasetVault.Common.Models;

namespace DatasetVault.AzureSearchRepository
{
    public static class Extensions
    {
        public static AzureDatasetEntry Map(this DatasetEntry entry)
        {
            return new AzureDatasetEntry
            {
                DatasetEntryId = Convert.ToString(entry.Id),
                Title = entry.Title,
                Description = entry.Description,
                Notes = entry.Notes,
                Category = entry.Category,
                IsImported = entry.IsImported,
                DateCreated = DateTime.Now
            };

        }

    }
}
