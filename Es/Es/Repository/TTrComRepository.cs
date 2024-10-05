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


    public async Task<List<TTrComModel>> GetAll(int from, int size)
    {
        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
                                                                        .Size(size).From(from)
                                                                        .Query(q => q.MatchAll((r) => { })));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }

    public async Task<List<TTrComModel>> GetByCustomerFullName(string customerFullName)
    {
        var termQuery = new TermQuery("customer_full_name.keyword") { Value = customerFullName, CaseInsensitive = true };
        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName).Query(termQuery));

        foreach (var hit in result.Hits)
            hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }

    public async Task<List<TTrComModel>> GetByCustomerIdList(List<int> customerIdlist)
    {
        List<FieldValue> terms = new List<FieldValue>();
        customerIdlist.ForEach(x =>
        {
            terms.Add(x);
        });

        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
                                                                        .Query(q => q
                                                                        .Terms(t => t
                                                                        .Field(f => f.CustomerId)
                                                                        .Term(new TermsQueryField(terms.AsReadOnly())))));


        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }
    public async Task<List<TTrComModel>> GetByIdList(List<string> idlist)
    {
        List<FieldValue> terms = new List<FieldValue>();
        idlist.ForEach(x =>
        {
            terms.Add(x);
        });

        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
                                                                        .Query(q => q
                                                                        .Terms(t => t
                                                                        .Field(f => f.Id)
                                                                        .Term(new TermsQueryField(terms.AsReadOnly())))));


        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }

    public async Task<List<TTrComModel>> GetStartWithCustomerName(string prefix)
    {
        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
                                                                        .Query(q => q
                                                                        .Prefix(p => p
                                                                        .Field(f => f.CustomerFullName.Suffix("keyword"))
                                                                        .Value(prefix))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }

    public async Task<List<TTrComModel>> GetTaxfulTotalPriceByRange(double from, double to)
    {
        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
                                                                        .Query(q => q
                                                                        .Range(p => p
                                                                        .NumberRange(nr => nr
                                                                        .Field(f => f.TaxfulTotalPrice)
                                                                        .Gt(from).Lt(to)))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }

    public async Task<List<TTrComModel>> GetByWildcard(string customerName) // like *,? --> Ma*, Mar? (Mary)
    {
        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
                                                                        .Query(q =>
                                                                        q.Wildcard(w =>
                                                                        w.Field(f => f.CustomerFirstName.Suffix("keyword"))
                                                                        .Wildcard(customerName))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return result.Documents.ToList();
    }

    public async Task<List<TTrComModel>> FullTextSearchAsync(string name)
    {
        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
        .Query(q => q
        .Match(m => m
            .Field(f => f.CustomerFullName)
            .Query(name).Operator(Operator.And))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToList();
    }
    public async Task<List<TTrComModel>> FullTextSearchWithPrefixAsync(string name)
    {


        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
            .Size(50).Query(q => q
                .MatchBoolPrefix(m => m
                    .Field(f => f.CustomerFullName)
                    .Query(name))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToList();
    }
    public async Task<List<TTrComModel>> FullTextSearchWithPhraseAsync(string name)
    {
        var result = await _elasticsearchClient.SearchAsync<TTrComModel>(s => s.Index(indexName)
            .Size(1000).Query(q => q
                .MatchPhrase(m => m
                    .Field(f => f.CustomerFullName)
                    .Query(name))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToList();
    }
}