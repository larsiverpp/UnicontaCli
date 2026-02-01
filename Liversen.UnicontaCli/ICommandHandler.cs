using System.Threading.Tasks;

namespace Liversen.UnicontaCli;

interface ICommandHandler<in TParameters>
{
    Task Run(TParameters parameters);
}
