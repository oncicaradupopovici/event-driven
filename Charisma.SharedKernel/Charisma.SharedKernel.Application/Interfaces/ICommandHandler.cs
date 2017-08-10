using System.Threading.Tasks;

namespace Charisma.SharedKernel.Application.Interfaces
{

    public interface ICommandHandler<in TCommand> where TCommand : Command
    {
        Task HandleAsync(TCommand command);
    }
}
