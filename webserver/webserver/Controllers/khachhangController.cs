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
    [RoutePrefix("api/khachhang")]
    public class khachhangController : ApiController
    {
        QL_CUAHANGDONGHOEntities1 db = new QL_CUAHANGDONGHOEntities1();
        [HttpGet]
        [Route("get")]
        public HttpResponseMessage xemTatCaKhachHang()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(db.KHACHHANGs.ToList()));
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
        public HttpResponseMessage layKhachHangTheoID(string id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(db.KHACHHANGs.Where(kh => kh.EMAIL == id).FirstOrDefault()));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {

                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpGet]
        [Route("update/email={email}/hoten={hoten}/gioitinh={gioitinh}/namsinh={namsinh}/sdt={sdt}/diachi={diachi}")]
        public HttpResponseMessage chinhsuathongtin([FromUri]string email, [FromUri]string hoten, [FromUri]string gioitinh, [FromUri]string namsinh,[FromUri] string sdt, [FromUri]string diachi)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                KHACHHANG kh = db.KHACHHANGs.Where(x => x.EMAIL == email).FirstOrDefault();
                kh.HOTEN = hoten;
                kh.GIOITINH = gioitinh;
                kh.NAMSINH = int.Parse(namsinh);
                kh.SDT = sdt;
                kh.NOISINH = diachi;
                db.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject(kh));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {

                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpGet]
        [Route("doimatkhau/email={email}/passcu={passcu}/passmoi={passmoi}")]
        public HttpResponseMessage doimatkhau([FromUri]string email, [FromUri]string passcu, [FromUri]string passmoi)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                KHACHHANG kh = db.KHACHHANGs.Where(x => x.EMAIL == email && x.PASS== passcu).FirstOrDefault();
                kh.PASS = passmoi;
                db.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject(kh));
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
