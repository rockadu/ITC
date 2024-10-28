using Domain.Dto;
using Domain.Dto.Organizacao;
using Domain.Models;

namespace Service.Organizacao;

public interface IOrganizacaoService
{
    Task<BaseListResultDto<SetorListDto>> ListarSetores(BaseListRequestDto request);
    Task<BaseListResultDto<CargoListDto>> ListarCargos(BaseListRequestDto request);
    Task<BaseListResultDto<UnidadeListDto>> ListarUnidades(BaseListRequestDto request);
}