using Domain.Dto.Organizacao;
using Domain.Dto;
using Domain.Models;

namespace Repository.Organizacao;

public interface IOrganizacaoRepository
{
    Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestDto request);
    Task<BaseListResultDto<CargoListDto>> ListarCargos(BaseListRequestDto request);
    Task<BaseListResultDto<UnidadeListDto>> ListarUnidades(BaseListRequestDto request);
}