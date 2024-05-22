using Domain.Dto;
using Domain.Dto.Organizacao;
using Domain.Models;
using Repository.Organizacao.Setor;

namespace Service.Organizacao;

public class SetorService : ISetorService
{
    private readonly ISetorRepository _repo;

    public SetorService(ISetorRepository setorRepository)
    {
        _repo = setorRepository;
    }

    public Task<BaseListResultDto<OrganizacaoListDto>> Listar(BaseListRequestDto request)
    {
        throw new NotImplementedException();
    }
}