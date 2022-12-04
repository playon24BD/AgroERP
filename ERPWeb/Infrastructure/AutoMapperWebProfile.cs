
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPWeb.Infrastructure
{
    public class AutoMapperWebProfile:AutoMapper.Profile
    {
        public static void Run() {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutoMapperWebProfile>();
            });
        }
        public AutoMapperWebProfile() {

        }
    }
}