using CrossCutting.Utils.HashMd5;
using Domain.Dto;
using Domain.Dto.Identificacao;
using Domain.Entities.Identificacao;
using Domain.Models;
using Repository.Identificacao.Usuario;

namespace Service.Identificacao;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly IMd5 _md5;

    public UsuarioService(IUsuarioRepository repository, IMd5 md5)
    {
        _repository = repository;
        _md5 = md5;
    }

    public async Task<UsuarioEntity?> Logar(string email, string senha)
    {
        var senhaMd5 = _md5.CreateMd5(senha);
        return await _repository.BuscarPorEmailESenha(email, senhaMd5);
    }

    public async Task<BaseListResultDto<UsuarioListDto>> Listar(BaseListRequestDto request) 
    {
        return await _repository.Listar(request);
    }
}