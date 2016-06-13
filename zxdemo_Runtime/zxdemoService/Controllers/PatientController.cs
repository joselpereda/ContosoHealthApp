using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using zxdemoService.DataObjects;
using zxdemoService.Models;

namespace zxdemoService.Controllers
{
    public class PatientController : TableController<Patient>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            zxdemoContext context = new zxdemoContext();
            DomainManager = new EntityDomainManager<Patient>(context, Request);
        }

        // GET tables/Patient
        public IQueryable<Patient> GetAllPatients()
        {
            return Query();
        }

        // GET tables/Patient/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Patient> GetPatient(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Patient/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Patient> PatchPatient(string id, Delta<Patient> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Patient
        public async Task<IHttpActionResult> PostPatient(Patient patient)
        {
            Patient current = await InsertAsync(patient);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Patient/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePatient(string id)
        {
            return DeleteAsync(id);
        }
    }
}