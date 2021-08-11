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
    [RoutePrefix("api/spyt")]
    public class sanphamyeuthichController : ApiController
    {
         QL_CUAHANGDONGHOEntities1 db = new QL_CUAHANGDONGHOEntities1();
        [HttpGet]
        [Route("{email}")]
        public HttpResponseMessage xemSPTY([FromUri] string email)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var lsp = (from sp in db.DONGHOYEUTHICHes.Where(x => x.IDKHACHHANG == email)
                           select new
                           {
                               ID = sp.IDDONGHO
                           }).ToList();
                var list = new List<sanphamyeuthich>();
                foreach (var item in lsp)
                {
                    sanphamyeuthich spyt = new sanphamyeuthich();

                    var s1 = db.DONGHOes.Where(x => x.IDDONGHO == item.ID).SingleOrDefault();
                    spyt.MOTA = string.Empty;
                    spyt.MOTA +="Thương hiệu:" + db.THUONGHIEUx.Where(x => x.IDTHUONGHIEU == s1.IDTHUONGHIEU).SingleOrDefault().TENTHUONGHIEU;
                    spyt.MOTA += ", Loại đồng hồ:" + db.LOAIDONGHOes.Where(x => x.IDLOAI == s1.IDLOAI).SingleOrDefault().TENLOAI;
                    spyt.MOTA += ", Xuất xứ:" + s1.XUATSU;
                    spyt.MOTA += ", Giới tính:" + s1.GIOITINH;
                    spyt.IDDONGHO = s1.IDDONGHO;
                    spyt.TENDONGHO = s1.TENDONGHO;
                    spyt.GIABAN = (double) s1.GIABAN;
                    spyt.HINH = s1.HINH;
                    list.Add(spyt);

                }
                response.Content = new StringContent(JsonConvert.SerializeObject(list));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpGet]
        [Route("{email}/{id}")]
        public HttpResponseMessage themSPYT([FromUri] string email, [FromUri] string id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                DONGHOYEUTHICH d =new DONGHOYEUTHICH();
                d.IDDONGHO= id;
                d.IDKHACHHANG=email;
                d.NGAYTHEM = DateTime.Now;
                db.DONGHOYEUTHICHes.Add(d);
                db.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject(new List<string>()));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpGet]
        [Route("find/{email}/{id}")]
        public HttpResponseMessage findSPYT([FromUri] string email, [FromUri] string id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(db.DONGHOYEUTHICHes.Where(x=>x.IDDONGHO == id && x.IDKHACHHANG==email).FirstOrDefault()));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpGet]
        [Route("delete/{email}/{id}")]
        public HttpResponseMessage xoaSPYT([FromUri] string email, [FromUri] string id)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                db.DONGHOYEUTHICHes.Remove(db.DONGHOYEUTHICHes.Where(x => x.IDDONGHO == id && x.IDKHACHHANG == email).FirstOrDefault());
                db.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject(new List<string>()));
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
