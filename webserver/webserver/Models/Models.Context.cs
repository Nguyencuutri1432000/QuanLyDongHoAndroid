﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webserver.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QL_CUAHANGDONGHOEntities1 : DbContext
    {
        public QL_CUAHANGDONGHOEntities1()
            : base("name=QL_CUAHANGDONGHOEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CHATLIEU> CHATLIEUx { get; set; }
        public DbSet<CHITIETHOADON> CHITIETHOADONs { get; set; }
        public DbSet<DONGHO> DONGHOes { get; set; }
        public DbSet<DONGHOYEUTHICH> DONGHOYEUTHICHes { get; set; }
        public DbSet<HOADON> HOADONs { get; set; }
        public DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public DbSet<LOAIDONGHO> LOAIDONGHOes { get; set; }
        public DbSet<QUANTRI> QUANTRIs { get; set; }
        public DbSet<THUONGHIEU> THUONGHIEUx { get; set; }
    }
}