using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetVault.AzureSearch.Models;
using DatasetVault.Common.Interfaces;
using Microsoft.Azure.Search.Models;

namespace DatasetVault.AzureSearchRepository.Interfaces
{
    public interface IAzureSearchRepository : ISearchRepository
    {
        DocumentSearchResponse<AzureDatasetEntry> SearchDocuments(string searchText, string filter = null);
    }
}
