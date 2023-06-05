using PersonaService.Context;
using PersonaService.Interfaces;
using PersonaService.Models;
using System.Transactions;

namespace PersonaService.Repositories
{
    public class PersonaRepository : IPersona
    {
        private PersonaContext _PersonaContext;

        public PersonaRepository(PersonaContext PersonaContext)
        {
            _PersonaContext = PersonaContext;
        }

        public async Task<ServiceResponse<string>> AddPersona(MPersona IPersona)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                await _PersonaContext.AddAsync(IPersona);
                Commit();
                response.Data = "Save Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;

        }

        public void Commit()
        {
            _PersonaContext.SaveChanges();
        }

        public async Task<ServiceResponse<string>> DeletePersona(int PersonaId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(PersonaId);
                _PersonaContext.MPersonas.Remove(responseData.Data);
                Commit();
                response.Data = "Delete Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

        public async Task<ServiceResponse<string>> EditPersona(int PersonaId, MPersona mPersonaNew)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            try
            {
                var responseData = await GetById(PersonaId);
                MPersona mPersonaOld = responseData.Data;
                ModelHelper.CopyProperty(mPersonaNew, ref mPersonaOld);
                Commit();
                response.Data = "Edit Success";
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }
            await Task.Yield();

            return response;
        }

        public async Task<IQueryable<MPersona>> GetAllPersona()
        {
            await Task.Yield();
            return _PersonaContext.MPersonas;
        }

        public async Task<ServiceResponse<MPersona>> GetById(int PersonaId)
        {
            ServiceResponse<MPersona> response = new ServiceResponse<MPersona>();
            try
            {
                MPersona PersonaData = _PersonaContext.MPersonas.Where(w => w.PersonaNo == PersonaId).FirstOrDefault();
                response.Data = PersonaData;

                if (PersonaData != null)
                {
                    response.TotalData = 1;
                }

                await Task.Yield();
                response.Success();
            }
            catch (Exception e)
            {
                response.Fail(e);
            }

            return response;
        }

    }
}
