using System;
using News;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WAPI.Models;

namespace WAPI.Controllers
{
    public class NewsController : ApiController
    {
        public SantanderN.News[] GET()
        {
            return GetTopContacts();
        }

        private SantanderN.News[] GetTopContacts()
        {
            return SantanderN.News.GetTopStories().ToArray();


        }

    }
}
