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
    [RoutePrefix("api/loaidongho")]
    public class loaidonghoController : ApiController
    {
        QL_CUAHANGDONGHOEntities1 db = new QL_CUAHANGDONGHOEntities1();
        [HttpGet]
        [Route("get")]
        public HttpResponseMessage layTatCaLoai()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var ldh = from l in db.LOAIDONGHOes
                          select new
                          {
                              ID = l.IDLOAI,
                              TEN = l.TENLOAI,
                              HINH = l.HINH
                          };
                response.Content = new StringContent(JsonConvert.SerializeObject(ldh.ToList()));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {

                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }

        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage layLoaiTheoID(string id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(db.LOAIDONGHOes.SingleOrDefault(dh => dh.IDLOAI == id)));
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
