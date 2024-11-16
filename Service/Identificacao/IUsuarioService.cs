using Domain.Dto.Identificacao;
using Domain.Dto;
using Domain.Entities.Identificacao;
using Domain.Models;
using Domain.Models.Usuario;

namespace Service.Identificacao;

public interface IUsuarioService
{
    Task<UsuarioEntity?> Logar(string email, string senha);
    Task<BaseListResultDto<UsuarioListDto>> Listar(BaseListRequestModel request);
    Task<UsuarioEntity> Criar(CriarUsuarioModel model);
}