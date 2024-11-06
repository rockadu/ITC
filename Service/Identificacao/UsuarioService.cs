using CrossCutting.Utils.HashMd5;
using Domain.Dto;
using Domain.Dto.Identificacao;
using Domain.Entities.Identificacao;
using Domain.Models;
using Domain.Models.Usuario;
using Repository.Identificacao.Usuario;
using System.Text;

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

    public async Task<UsuarioEntity> Criar(CriarUsuarioModel model)
    {
        var _usuario = new UsuarioEntity();

        var _senha = GerarSenha();

        _usuario.Nome = model.Nome;
        _usuario.Email = model.Email;
        _usuario.CodigoUnidade = model.CodigoUnidade;
        _usuario.CodigoSetor = model.CodigoSetor;
        _usuario.CodigoCargo = model.CodigoCargo;
        _usuario.Senha = _md5.CreateMd5(_senha);

        //TODO ENVIAR SENHA

        _usuario = await _repository.Adicionar(_usuario);

        _usuario.Senha = string.Empty;
        return _usuario;
    }

    private string GerarSenha(int tamanho = 8)
    {
        const string letrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
        const string numeros = "0123456789";
        const string caracteresEspeciais = "!@#$%^&*()-_=+<>?";

        // Combina todos os caracteres
        string todosCaracteres = letrasMaiusculas + letrasMinusculas + numeros + caracteresEspeciais;

        var random = new Random();
        var senha = new StringBuilder();

        // Garante que cada tipo de caractere esteja presente
        senha.Append(letrasMaiusculas[random.Next(letrasMaiusculas.Length)]);
        senha.Append(letrasMinusculas[random.Next(letrasMinusculas.Length)]);
        senha.Append(numeros[random.Next(numeros.Length)]);
        senha.Append(caracteresEspeciais[random.Next(caracteresEspeciais.Length)]);

        // Preenche o restante da senha com caracteres aleatórios
        for (int i = 4; i < tamanho; i++)
        {
            senha.Append(todosCaracteres[random.Next(todosCaracteres.Length)]);
        }

        // Embaralha os caracteres para garantir aleatoriedade
        return Embaralhar(senha.ToString());
    }

    private string Embaralhar(string senha)
    {
        var random = new Random();
        var array = senha.ToCharArray();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        return new string(array);
    }
}