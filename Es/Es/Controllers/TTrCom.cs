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


    // GET
    [HttpPost]
    [Route("GetByCustomerFullName")]
    public async Task<IActionResult> GetByCustomerFullName(string customerFullName)
    {
        return Ok(await _repository.GetByCustomerFullName(customerFullName));
    }
}