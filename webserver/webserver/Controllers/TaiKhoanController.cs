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
     [RoutePrefix("api/taikhoan")]
    public class TaiKhoanController : ApiController
    {
        QL_CUAHANGDONGHOEntities1 db = new QL_CUAHANGDONGHOEntities1();
        [HttpGet]
        [Route("dangnhap/{email}/{password}")]
        public HttpResponseMessage dangnhap([FromUri] string email, [FromUri]string password)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(db.KHACHHANGs.Where(x=>x.EMAIL==email && x.PASS == password).FirstOrDefault()));
                response.Content.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpGet]
        [Route("dangky/{email}/{password}")]
        public HttpResponseMessage dangky([FromUri] string email, [FromUri] string password)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                KHACHHANG kh = new KHACHHANG();
                kh.EMAIL = email;
                kh.PASS = password;
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject(kh));
                response.Content.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }

        
    }
}
