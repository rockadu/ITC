using Domain.Dto.Identificacao;
using Domain.Dto;
using Domain.Entities.Identificacao;
using Domain.Models;

namespace Repository.Identificacao.Usuario;

public interface IUsuarioRepository
{
    Task<UsuarioEntity?> BuscarPorEmailESenha(string email, string senhaMd5);
    Task<BaseListResultDto<UsuarioListDto>> Listar(BaseListRequestDto request);
    Task<UsuarioEntity> Adicionar(UsuarioEntity entity);
}