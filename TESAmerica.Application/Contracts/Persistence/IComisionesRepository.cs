using TESAmerica.Application.Features.Comisiones;

namespace TESAmerica.Application.Contracts.Persistence;

public interface IComisionesRepository
{
    Task<List<ComisionResponse>> comisiones(ComisionesCommand command);
}