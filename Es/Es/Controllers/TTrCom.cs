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

    [HttpPost]
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

    [HttpPost]
    [Route("GetByCustomerFullName")]
    public async Task<IActionResult> GetByCustomerFullName(string customerFullName)
    {
        return Ok(await _repository.GetByCustomerFullName(customerFullName));
    }

    [HttpPost]
    [Route("GetByCustomerIdList")]
    public async Task<IActionResult> GetByCustomerIdList(List<int> customerIdlist)
    {
        return Ok(await _repository.GetByCustomerIdList(customerIdlist));
    }
    [HttpPost]
    [Route("GetByIdList")]
    public async Task<IActionResult> GetByIdList(List<string> idlist)
    {
        return Ok(await _repository.GetByIdList(idlist));
    }

    [HttpPost]
    [Route("GetStartWithCustomerName")]
    public async Task<IActionResult> GetStartWithCustomerName(string prefix)
    {
        return Ok(await _repository.GetStartWithCustomerName(prefix));
    }

    [HttpPost]
    [Route("GetTaxfulTotalPriceByRange")]
    public async Task<IActionResult> GetTaxfulTotalPriceByRange(double from, double to)
    {
        return Ok(await _repository.GetTaxfulTotalPriceByRange(from, to));
    }

    [HttpPost]
    [Route("GetByWildcard")]
    public async Task<IActionResult> GetByWildcard(string wildcard)
    {
        return Ok(await _repository.GetByWildcard(wildcard));
    }

}