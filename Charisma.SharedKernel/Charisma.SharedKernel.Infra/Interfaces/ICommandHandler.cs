using System.Threading.Tasks;

namespace Charisma.SharedKernel.Core.Interfaces
{

    public interface ICommandHandler<in TCommand> where TCommand : Command
    {
        Task HandleAsync(TCommand command);
    }
}
