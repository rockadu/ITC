using Domain.Entities.Organizacao;

namespace Domain.Entities.Identificacao;
public class UsuarioEntity
{
    public int Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Apelido { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    string? _Foto;
    public string? Foto { get => _Foto ?? "default.png" ; set => _Foto = value; }
    public bool Ativo { get; set; }
    public int? CodigoCargo { get; set; }
    public int CodigoSetor { get; set; }
    public int CodigoUnidade { get; set; }
    public int? CodigoSuperiorImediato { get; set; }
    public UsuarioEntity? SuperiorImediato { get; set; }
    public CargoEntity Cargo { get; set; } = new CargoEntity();
    public List<PerfilEntity> Perfis { get; set; } = new List<PerfilEntity>();
    public List<PermissaoEntity> Permissoes { get; set; } = new List<PermissaoEntity>();

    public string GerarMenu()
    {
        var _rawMenu = "";



        return _rawMenu;
    }

    private string MenuConfiguracoes()
    {
        var _rawMenuConfiguracoes = "";

        return _rawMenuConfiguracoes;
    }
}