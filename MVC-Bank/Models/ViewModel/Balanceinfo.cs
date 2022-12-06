using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Bank.Models.ViewModel
{
    public class Balanceinfo
    {
        IEnumerable<Balance>Balance { get; set; }
        IEnumerable<Info>infos { get; set; }
    }
}