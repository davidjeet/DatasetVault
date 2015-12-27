using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using DatasetVault.AzureSearch.Models;
using DatasetVault.AzureSearchRepository.Interfaces;
using DatasetVault.Common;
using DatasetVault.Common.Interfaces;
using DatasetVault.Common.Models;
using Microsoft.Azure;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;


namespace DatasetVault.AzureSearchRepository
{
    public class AzureSearchService : IAzureSearchRepository
    {
        private readonly SearchServiceClient _searchClient;
        private SearchIndexClient _indexClient;
        private readonly string _indexName;

        public AzureSearchService(string indexName = null)
        {
            string searchServiceName = DVConfiguration.Instance.SearchServiceName;
            string apiKey = DVConfiguration.Instance.SearchServiceApiKey;
            _indexName = indexName ?? DVConfiguration.Instance.SearchIndexName;

            // Create an HTTP reference to the catalog index
            _searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
        }

        public bool CreateIndex(string indexName = null)
        {
            // Create the Azure Search index based on the included schema  
            bool success = false;
            try
            {
                indexName = indexName ?? DVConfiguration.Instance.SearchIndexName;
                var definition = new Index()
                {
                    Name = DVConfiguration.Instance.SearchIndexName,
                    Fields = new[]
                    {
                        new Field("id", DataType.Int32)          { IsKey = true,  IsSearchable = false, IsFilterable = false, IsSortable = false, IsFacetable = false, IsRetrievable = true},
                        new Field("title", DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = true,  IsSortable = true,  IsFacetable = false, IsRetrievable = true},
                        new Field("description", DataType.String)   { IsKey = false, IsSearchable = true,  IsFilterable = true,  IsSortable = true,  IsFacetable = false, IsRetrievable = true},
                        new Field("notes", DataType.String)         { IsKey = false, IsSearchable = true,  IsFilterable = true,  IsSortable = true,  IsFacetable = false, IsRetrievable = true},
                        new Field("category", DataType.String)       { IsKey = false, IsSearchable = true, IsFilterable = true,  IsSortable = true,  IsFacetable = true,  IsRetrievable = true},
                        new Field("isImported", DataType.Boolean)    { IsKey = false, IsSearchable = false,  IsFilterable = true,  IsSortable = true,  IsFacetable = false, IsRetrievable = true},
                        new Field("dateCreated", DataType.DateTimeOffset) { IsKey = false, IsSearchable = false, IsFilterable = true,  IsSortable = true,  IsFacetable = true,  IsRetrievable = true}
                    }
                };

                _searchClient.Indexes.Create(definition);
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating index: {0}\r\n", ex.Message.ToString());
            }

            return success;
        }

        public bool DeleteIndex(string indexName = null)
        {
            // Delete the index if it exists
            try
            {
                indexName = indexName ?? DVConfiguration.Instance.SearchIndexName;
                if (_searchClient.Indexes.Exists(indexName))
                { 
                    _searchClient.Indexes.Delete(indexName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting index: {0}\r\n", ex.Message.ToString());
                return false;
            }

            return true;
        }

        public void Populate(IEnumerable<DatasetEntry> documents)
        {
            _indexClient = _searchClient.Indexes.GetClient(DVConfiguration.Instance.SearchIndexName);
            try
            {
                _indexClient.Documents.Index(IndexBatch.Create(documents.Select(doc => IndexAction.Create(doc.Map()))));
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Console.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexResponse.Results.Where(r => !r.Succeeded).Select(r => r.Key)));
            }

            // Wait a while for indexing to complete. Delays like this are typically only necessary in demos, tests, and sample applications.
            Thread.Sleep(2000);
        }

        public DocumentSearchResponse<AzureDatasetEntry> SearchDocuments(string searchText, string filter = null)
        {
            _indexClient = _searchClient.Indexes.GetClient(DVConfiguration.Instance.SearchIndexName);

            // Execute search based on search text and optional filter
            var sp = new SearchParameters();

            if (!String.IsNullOrEmpty(filter))
            {
                sp.Filter = filter;
            }

            DocumentSearchResponse<AzureDatasetEntry> response = _indexClient.Documents.Search<AzureDatasetEntry>(searchText, sp);
            foreach (SearchResult<AzureDatasetEntry> result in response)
            {
                Console.WriteLine(result.Document);
            }

            return response;
        }
    }
}
