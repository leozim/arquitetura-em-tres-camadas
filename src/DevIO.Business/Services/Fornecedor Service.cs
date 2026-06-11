using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services;

public class FornecedorService : BaseService, IFornecedorService
{
    private readonly IFornecedorRepository _fornecedorRepository;

    public FornecedorService(IFornecedorRepository fornecedorRepository)
    {
        _fornecedorRepository = fornecedorRepository;
    }
    
    public void Dispose()
    {
        _fornecedorRepository?.Dispose();
    }

    public async Task Adicionar(Fornecedor fornecedor)
    {
        // Validar se a entidade é consistente
        if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
            || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco))
        // Validar se ja nao existe outro fornecedor com o mesmo doc.
            if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado");
                return;
            }
        
        await _fornecedorRepository.Adicionar(fornecedor);
    }

    public async Task Atualizar(Fornecedor fornecedor)
    {
        if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

        if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result
            .Any())
        {
            Notificar("Já existe um fornecedor com este documento informado");
            return;
        }
        
        await _fornecedorRepository.Atualizar(fornecedor);
    }

    public async Task Remover(Guid id)
    {
        await _fornecedorRepository.Remover(id);
    }
}