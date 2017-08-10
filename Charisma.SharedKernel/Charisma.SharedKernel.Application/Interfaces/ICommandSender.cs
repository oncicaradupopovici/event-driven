using System.Threading.Tasks;

namespace Charisma.SharedKernel.Application.Interfaces
{
    public interface ICommandSender
    {
        Task SendAsync<T>(T command) where T : Command;

    }
}
