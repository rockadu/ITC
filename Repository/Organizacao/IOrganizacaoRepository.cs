using Domain.Dto.Organizacao;
using Domain.Dto;
using Domain.Models;

namespace Repository.Organizacao;

public interface IOrganizacaoRepository
{
    Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestDto request);
    Task<BaseListResultDto<SetorListDto>> ListarCargos(BaseListRequestDto request);
    Task<BaseListResultDto<SetorListDto>> ListarUnidades(BaseListRequestDto request);
}