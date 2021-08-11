using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webserver.Models;

namespace webserver.Controllers
{
    [RoutePrefix("api/thuonghieu")]
    public class thuonghieuController : ApiController
    {
        QL_CUAHANGDONGHOEntities1 db = new QL_CUAHANGDONGHOEntities1();
        [HttpGet]
        [Route("get")]
        public HttpResponseMessage layTatCaThuongHieu()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var th = from t in db.THUONGHIEUx
                         select new
                         {
                             ID = t.IDTHUONGHIEU,
                             TEN=t.TENTHUONGHIEU,
                             HINH=t.HINH
                         };
                response.Content = new StringContent(JsonConvert.SerializeObject(th.ToList()));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }

        }
    }
}
