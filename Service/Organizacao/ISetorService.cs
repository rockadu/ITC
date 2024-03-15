using Domain.Dto;
using Domain.Dto.Organizacao;
using Domain.Models;

namespace Service.Organizacao;

public interface ISetorService
{
    Task<BaseListResultDto<OrganizacaoListDto>> Listar(BaseListRequestDto request);
}