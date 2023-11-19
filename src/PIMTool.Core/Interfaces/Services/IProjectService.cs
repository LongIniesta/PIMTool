using PIMTool.Core.Domain.Entities;

namespace PIMTool.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project?> GetAsync(decimal id, CancellationToken cancellationToken = default);
    }
}