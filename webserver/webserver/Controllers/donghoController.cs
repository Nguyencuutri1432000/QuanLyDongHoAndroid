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
    [RoutePrefix("api/dongho")]
    public class donghoController : ApiController
    {
        QL_CUAHANGDONGHOEntities1 db = new QL_CUAHANGDONGHOEntities1();
        [HttpGet]
        [Route("get")]
        public HttpResponseMessage layTatCaSanPham()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var dongho = (from d in db.DONGHOes
                              select new
                              {
                                  IDDONGHO = d.IDDONGHO,
                                  TENDONGHO = d.TENDONGHO,
                                  GIABAN = d.GIABAN,
                                  XUATSU = d.XUATSU,
                                  GIOITINH = d.GIOITINH,
                                  IDLOAI = db.LOAIDONGHOes.Where(x => x.IDLOAI == d.IDLOAI).FirstOrDefault().TENLOAI,
                                  IDTHUONGHIEU = db.THUONGHIEUx.Where(x => x.IDTHUONGHIEU == d.IDTHUONGHIEU).FirstOrDefault().TENTHUONGHIEU,
                                  HINH = d.HINH
                              }).ToList();
                response.Content = new StringContent(JsonConvert.SerializeObject(dongho));
                response.Content.Headers.ContentType = 
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {

                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }

        }
        [HttpGet]
        [Route("get/{id}")]
        public HttpResponseMessage laySanPhamTheoID(string id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(db.DONGHOes.SingleOrDefault(dh => dh.IDDONGHO == id)));
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
