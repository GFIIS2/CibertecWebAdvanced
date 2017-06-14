using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cibertec.Web.Filter
{
    public class ExceptionLoggerFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(context.Exception);
            //base.OnException(context);
        }
    }
}
