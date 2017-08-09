using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cibertec.RealTime.Controllers
{
    public class DefaultController: ApiController
    {
        public IHttpActionResult Get()
        {
            var list = new List<string>();
            for (int i = 0; i < 3000; i++)
            {
                //list.Add("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                list.Add( (new Text()).text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            }
            return Ok(list);
        }
    }

    public class Text
    {
        public string text {get; set;}
    }
}