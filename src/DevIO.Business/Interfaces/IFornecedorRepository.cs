using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IFornecedorRepository : IRepository<Fornecedor>
{
    Task<Fornecedor> ObterFornecedorEndereco(Guid id);    
    Task<Fornecedor> ObterFornecedorProdutoEndereco(Guid id);
    
    Task<Endereco> ObterEderecoPorFornecedor(Guid fornecedorId);
    Task RemoverEnderecoFornecedor(Endereco endereco);
}