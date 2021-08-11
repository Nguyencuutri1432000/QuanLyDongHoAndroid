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
    [RoutePrefix("api/hoadon")]
    public class hoadonController : ApiController
    {
        QL_CUAHANGDONGHOEntities1 db = new QL_CUAHANGDONGHOEntities1();

        [HttpGet]
        [Route("taohoadon/{email}/{tongtien}")]
        public HttpResponseMessage taohoadon([FromUri] string email, [FromUri] string tongtien)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                HOADON hd = new HOADON();
                if (db.HOADONs.Count() == 0)
                {
                    hd.IDHOADON = 1;
                }
                else
                {
                    hd.IDHOADON = db.HOADONs.OrderByDescending(x => x.IDHOADON).FirstOrDefault().IDHOADON + 1;
                }
                hd.NGAYLAP = DateTime.Now;
                hd.TONGTIEN = double.Parse(tongtien);
                hd.IDKHACHHANG = email;
                db.HOADONs.Add(hd);
                db.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject(hd));
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
        [Route("thanhtoan/{id}/{soluong}")]
        public HttpResponseMessage thanhtoan([FromUri] string id, [FromUri] string soluong)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                CHITIETHOADON ct = new CHITIETHOADON();
                ct.IDDONGHO = id;
                ct.IDHOADON = db.HOADONs.OrderByDescending(x => x.IDHOADON).FirstOrDefault().IDHOADON;
                ct.SOLUONG = int.Parse(soluong);
                ct.DONGIA = db.DONGHOes.Where(x => x.IDDONGHO == id).FirstOrDefault().GIABAN;
                ct.THANHTIEN = ct.DONGIA * ct.SOLUONG;
                db.CHITIETHOADONs.Add(ct);
                db.SaveChanges();
                response.Content = new StringContent(JsonConvert.SerializeObject(ct));
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
        [Route("lichsumuahang/{email}")]
        public HttpResponseMessage lichsumuahang([FromUri] string email)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                List<LichSuMuaHang> listsp = new List<LichSuMuaHang>();
                var listhd = db.HOADONs.Where(x => x.IDKHACHHANG == email).ToList();
                foreach (var item in listhd)
                {
                    var listDH =  db.CHITIETHOADONs.Where(x=>x.IDHOADON == item.IDHOADON).ToList();
                    foreach (var dh in listDH)
	                {
                        LichSuMuaHang ls = new LichSuMuaHang();
                        ls.NGAYMUA = item.NGAYLAP.Value;
                        ls.IDDONGHO = dh.IDDONGHO;
                        ls.TENDONGHO = db.DONGHOes.Where(x=>x.IDDONGHO == dh.IDDONGHO).FirstOrDefault().TENDONGHO;
                        ls.SOLUONG = dh.SOLUONG.Value;
                        ls.DONGIA= dh.DONGIA.Value;
                        ls.HINH = db.DONGHOes.Where(x => x.IDDONGHO == dh.IDDONGHO).FirstOrDefault().HINH;
                        ls.THANHTIEN = dh.THANHTIEN.Value;
                        listsp.Add(ls);
	                }
                }
                response.Content = new StringContent(JsonConvert.SerializeObject(listsp));
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
    }
}
