using SampleRecapApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleRecapApi.Controllers
{
    public class SamplesController : ApiController
    {
        public List<SampleTable> GetAllSamples() => new FaiTrainingEntities().SampleTables.ToList();

        public HttpResponseMessage GetSample(string id) {
            int sId = int.Parse(id);
            var rec = new FaiTrainingEntities().SampleTables.FirstOrDefault((s) => s.SampleId == sId);
            return Request.CreateResponse<SampleTable>(HttpStatusCode.OK, rec);
        }

        public string AddSample(SampleTable rec)
        {
            var context = new FaiTrainingEntities();
            context.SampleTables.Add(rec);
            context.SaveChanges();
            return "Sample Stored to the database";
        }

        public string DeleteSample(string id)
        {
            var context = new FaiTrainingEntities();
            var sId = int.Parse(id);
            var rec = context.SampleTables.FirstOrDefault((s) => s.SampleId == sId);
            context.SampleTables.Remove(rec);
            context.SaveChanges();
            return "Deleted from the database";
        }

        public string UpdateSample(SampleTable sample)
        {
             var context = new FaiTrainingEntities();
            var rec = context.SampleTables.FirstOrDefault((s) => s.SampleId == sample.SampleId);
            rec.SampleBill = sample.SampleBill;
            rec.SampleDate = sample.SampleDate;
            rec.SampleName = sample.SampleName;
            context.SaveChanges();
            return "Updated to the database";
        }
    }
}
