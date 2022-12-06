using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Bank.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DefaultConnection")
        {
        }



        

    }
}