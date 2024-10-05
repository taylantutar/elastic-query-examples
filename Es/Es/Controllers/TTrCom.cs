using Es.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Es.Controllers;

public class TTrCom : ControllerBase
{
    private readonly TTrComRepository _repository;

    public TTrCom(TTrComRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll(int from, int size)
    {
        var data = await _repository.GetAll(from, size);
        return Ok(new
        {
            Data = data,
            Size = data.Count
        });
    }

    [HttpGet]
    [Route("GetByCustomerFullName")]
    public async Task<IActionResult> GetByCustomerFullName(string customerFullName)
    {
        return Ok(await _repository.GetByCustomerFullName(customerFullName));
    }

    [HttpGet]
    [Route("GetByCustomerIdList")]
    public async Task<IActionResult> GetByCustomerIdList(List<int> customerIdlist)
    {
        return Ok(await _repository.GetByCustomerIdList(customerIdlist));
    }
    [HttpGet]
    [Route("GetByIdList")]
    public async Task<IActionResult> GetByIdList(List<string> idlist)
    {
        return Ok(await _repository.GetByIdList(idlist));
    }

    [HttpGet]
    [Route("GetStartWithCustomerName")]
    public async Task<IActionResult> GetStartWithCustomerName(string prefix)
    {
        return Ok(await _repository.GetStartWithCustomerName(prefix));
    }

    [HttpGet]
    [Route("GetTaxfulTotalPriceByRange")]
    public async Task<IActionResult> GetTaxfulTotalPriceByRange(double from, double to)
    {
        return Ok(await _repository.GetTaxfulTotalPriceByRange(from, to));
    }

    [HttpGet]
    [Route("GetByWildcard")]
    public async Task<IActionResult> GetByWildcard(string wildcard)
    {
        return Ok(await _repository.GetByWildcard(wildcard));
    }

    [HttpGet]
    [Route("FullTextSearchAsync")]
    public async Task<IActionResult> FullTextSearchAsync(string name)
    {
        return Ok(await _repository.FullTextSearchAsync(name));
    }
    [HttpGet]
    [Route("FullTextSearchWithPrefixAsync")] // Mary Bail --> Mary Bailey
    public async Task<IActionResult> FullTextSearchWithPrefixAsync(string name)
    {
        return Ok(await _repository.FullTextSearchWithPrefixAsync(name));
    }
    [HttpGet]
    [Route("FullTextSearchWithPhraseAsync")] // Mary Bail --> Mary Bailey
    public async Task<IActionResult> FullTextSearchWithPhraseAsync(string name)
    {
        return Ok(await _repository.FullTextSearchWithPhraseAsync(name));
    }

}