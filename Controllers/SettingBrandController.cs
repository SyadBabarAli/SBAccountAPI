using SBAccountAPI.Context;
using SBAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SBAccountAPI.Controllers
{
    public class SettingBrandController : ApiController
    {
        public string Get()
        {
            using (Context.Context ctx = new Context.Context())
            {
                try
                {
                    //ctx.Database.Connection.Open();
                    //ctx.Database.Connection.Close();


                    var v1 = ctx.SettingBrands;
                    var v2 = v1.FirstOrDefault();
                    var v3 = v2.BrandName;

                }
                catch (Exception ex)
                {

                }
            }

            //using (GhostIRTypeContext dbContext = new GhostIRTypeContext())
            //{
            //    dbContext.Database.Exists();
            //}

            //using (var ctx = new GhostIRTypeContext())
            //{
            //    var v1 = ctx.GhostIRType;
            //    var v2 = v1.FirstOrDefault();
            //    var v3 = v2.Name;

            //    //var stud = new GhostIRType() { TestName = "Bill" };
            //    //ctx.GhostIRTypes.Add(stud);
            //    //ctx.SaveChanges();
            //}

            return "test";

            //QCTestContext db = new QCTestContext();

            //var v1 = db.QCTest;
            //var v2 = v1.FirstOrDefault();
            //var v3 = v2.TestName;
            //return v3;
            // return Request.CreateResponse(HttpStatusCode.OK, v1);

        }
    }
}
