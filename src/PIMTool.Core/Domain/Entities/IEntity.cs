namespace PIMTool.Core.Domain.Entities;

public interface IEntity
{
    public decimal Id { get; set; }
    public byte[] Version { get; set; }
}