using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Thunders.TaskGo.Domain.DTO.User;
using Thunders.TaskGo.Service.Services;


[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserService _usuarioService;

    public UserController(IUserService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public string GerarTokenAcesso()
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes("qFBfvoaGAaXMPtqUON63xBiVF9EiSLIEZk14CJszAe0");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "ThundersTaskGoUser"),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }

    [HttpGet]
    [Authorize(Policy = "BearerPolicy")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var response = await _usuarioService.GetUsersAsync();

        return Ok(response);
    }

    [HttpGet]
    [Authorize(Policy = "BearerPolicy")]
    public async Task<IActionResult> GetUserByIdAsync(Guid Id)
    {
        var response = await _usuarioService.GetUserByIdAsync(Id);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Cria um novo usuário com base nos dados fornecidos.")]
    [SwaggerResponse(201, "Criado com sucesso", typeof(NewUserDTO))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    public async Task<IActionResult> AddUserAsync(NewUserDTO model)
    {
        if (ModelState.IsValid)        
            await _usuarioService.AddUsersAsync(model);        

        return Ok(model);
    }
  
    [HttpPost]
    [Authorize(Policy = "BearerPolicy")]
    [SwaggerOperation(Summary = "Atualiza um usuário existente", Description = "Atualiza os detalhes de um usuário existente com base no ID.")]
    [SwaggerResponse(204, "Atualizado com sucesso", typeof(NewUserDTO))]
    [SwaggerResponse(400, "Requisição inválida", typeof(void))]
    [SwaggerResponse(404, "Não encontrado", typeof(void))]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserDTO model)
    {
        if (ModelState.IsValid)
            await _usuarioService.UpdateUsersAsync(model);

        return Ok(model);
    }
}