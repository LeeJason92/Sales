using PersonaService.Models;

namespace PersonaService.Interfaces
{
    public interface IPersona
    {
        Task<IQueryable<MPersona>> GetAllPersona();
        Task<ServiceResponse<MPersona>> GetById(int PersonaId);
        Task<ServiceResponse<string>> AddPersona(MPersona IPersona);
        Task<ServiceResponse<string>> EditPersona(int PersonaId, MPersona mPersonaNew);
        Task<ServiceResponse<string>> DeletePersona(int PersonaId);
        void Commit();
    }
}
