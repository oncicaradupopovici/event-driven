using System.Threading.Tasks;

namespace Charisma.SharedKernel.Domain.Interfaces
{
    public interface ICommandSender
    {
        Task SendAsync<T>(T command) where T : Command;

    }
}
