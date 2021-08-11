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
    [RoutePrefix("api/sanphamtheodanhmuc")]
    public class SanPhamTheoDanhMucController : ApiController
    {
        QL_CUAHANGDONGHOEntities1 db = new QL_CUAHANGDONGHOEntities1();

        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage laySanPhamTheoDanhMuc([FromUri] string id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(db.DONGHOes.Where(x => x.IDTHUONGHIEU == id || x.IDLOAI == id).ToList()));
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
