using System.Threading.Tasks;

namespace Charisma.SharedKernel.Core.Interfaces
{
    public interface ICommandSender
    {
        Task SendAsync<T>(T command) where T : Command;

    }
}
