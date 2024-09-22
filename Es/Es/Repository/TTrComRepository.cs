using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Es.Models;

namespace Es.Repository;

public class TTrComRepository
{
    private const string indexName = "kibana_sample_data_ecommerce";
    private readonly ElasticsearchClient _elasticsearchClient;
    public TTrComRepository(ElasticsearchClient elasticsearchClient)
    {
        this._elasticsearchClient = elasticsearchClient;
    }

    public async Task<List<TTrComModel>> GetByCustomerFullName(string customerFullName)
    {
        var termQuery = new TermQuery("customer_full_name.keyword") { Value = customerFullName, CaseInsensitive = true };
        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName).Query(termQuery));
        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        
        return result.Documents.ToList();
    }
}