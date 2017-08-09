using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cibertec.RealTime.Hubs
{
    public class NotificationHub: Hub
    {
        public void UpdateProduct(int id)
        {
            //nombre del metodo, pues el WEB API es CAMEL CASE, por eso comienza con MINUSCULA
            Clients.All.updateProduct(id);
        }
    }
}