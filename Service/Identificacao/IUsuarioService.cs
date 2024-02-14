using Domain.Dto.Identificacao;
using Domain.Dto;
using Domain.Entities.Identificacao;
using Domain.Models;

namespace Service.Identificacao;

public interface IUsuarioService
{
    Task<UsuarioEntity?> Logar(string email, string senha);
    Task<BaseListResultDto<UsuarioListDto>> Listar(BaseListRequestDto request);
}