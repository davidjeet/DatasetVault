using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetVault.Common.Models;

namespace DatasetVault.Common.Interfaces
{
    public interface ISearchRepository
    {
        bool CreateIndex(string indexName = null);

        bool DeleteIndex(string indexName = null);

        void Populate(IEnumerable<DatasetEntry> documents);
    }
}
