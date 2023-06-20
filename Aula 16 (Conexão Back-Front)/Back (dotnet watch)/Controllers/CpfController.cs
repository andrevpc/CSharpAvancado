using Microsoft.AspNetCore.Mvc;

namespace ProjetoWeb.Controllers;

[ApiController]
[Route("cpf")]
public class CpfController : ControllerBase
{
    [HttpGet("{cpf}")]
    // Usa um serviço usando o atributo FromServices que foi cadastrado no Program.cs
    [HttpGet("generate/{region}")]
    public ActionResult<Cpf> Generate([FromServices] CpfService cpf, int region)
    {
        var result = cpf.Generate(region);
        return result;

        return result;
    }
}
