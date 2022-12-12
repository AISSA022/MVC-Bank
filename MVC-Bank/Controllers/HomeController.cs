using Microsoft.AspNet.Identity;
using MVC_Bank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Bank.Controllers
{[Authorize]
    public class HomeController : Controller
    {
        /*private SqlConnection update;
        private SqlCommand set;

        private DBContext _Db;*/
        private Entities9 _Db;
        public HomeController()
        {
            _Db = new Entities9();
        }

        
        public ActionResult Index()
        {
           string username=User.Identity.GetUserName();
           var user=_Db.Balances.FirstOrDefault(c=>c.UserName==username);
            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Withdraw(int id)
        {
            var user = _Db.Balances.FirstOrDefault(c => c.Id == id);
            return View(user);
        }
        [HttpPost]
        public ActionResult ConfWithdraw([Bind(Include = "Id,Balance1, UserName")]int id,int amount)
        {
            string username = User.Identity.GetUserName();
            var userz = _Db.Balances.FirstOrDefault(c => c.UserName == username);

            var user =_Db.Balances.FirstOrDefault(c=> c.Id==id);
            if (user.Balance1 > amount)
            {
                Info data = new Info()
                {
                    UserId = user.Id,
                    Date = DateTime.Now,
                    Action = "Withdraw",
                    amount = amount,
                    Balance = user.Balance1 - amount

                   
                };
                Balance balance = new Balance()
                {
                    Id= userz.Id,
                    UserName=userz.UserName,
                    Balance1=userz.Balance1-amount,
                };
                _Db.Infoes.Add(data);
                /*           _Db.Infoes.Add(data);
                           _Db.SaveChanges();*/
                _Db.Entry(balance).State = EntityState.Modified;
                _Db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            else if(user.Balance1< amount) 
            {
                throw new ApplicationException("Please Re-Check the amount you entered, you dont have enough money to withdraw");
            }
            else if (amount <= 0)
            {
                throw new ApplicationException("Invalid Withdraw Amount");
            }
            return RedirectToAction("Index");
        }
        /*[HttpPost]*/
        /*        public ActionResult QuickCash()
                {
                    update = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-MVC-Bank-20221202083652.mdf;Initial Catalog=MVC-Bank;Integrated Security=True");
                    update.Open();
                    set = new SqlCommand("UPDATE Balances SET Balance1=100 WHERE UserID = 1");
                    return RedirectToAction("Index");
                }*/
        /*        [HttpPost]
                public ActionResult QuickCash()
                {

                    return RedirectToAction("Index");
                }*/
        public ActionResult QuickCash(int id)
        {
            var user = _Db.Balances.FirstOrDefault(c => c.Id == id);
            if (user.Balance1 > 100)
            {
                Info data = new Info()
                {
                    UserId = user.Id,
                    Date = DateTime.Now,
                    Action = "QuickWithdraw",
                    amount = 100,
                    Balance = user.Balance1 - 100
                };
                Balance balance = new Balance()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Balance1 = user.Balance1 - 100,
                };
                _Db.Infoes.Add(data);
                /*           _Db.Infoes.Add(data);
                           _Db.SaveChanges();*/
                _Db.Balances.Add(balance);
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }
                return RedirectToAction("Index");
        }


        public ActionResult Balance(int id)
        {

            string username = User.Identity.GetUserName();
            var user = _Db.Balances.FirstOrDefault(c => c.UserName == username);
            return View(user);
        }
        public ActionResult Print(int id)
        {
            var info = _Db.Infoes.Where(c => c.UserId == id).ToList();
            return View(info);
        }
        public ActionResult Deposite(int id)
        {
            var user = _Db.Balances.FirstOrDefault(c => c.Id ==id);

            return View(user);
        }
        [HttpPost]
        public ActionResult ConfDeposite(int id,int amount)
        {
            var user = _Db.Balances.FirstOrDefault(c => c.Id == id);
            if (amount <= 0)
            {
                throw new ApplicationException("Invalid Amount");
            }
            else
            {
                Info data = new Info()
                {
                    UserId = user.Id,
                    Date = DateTime.Now,
                    Action = "Deposite",
                    amount = amount,
                    Balance = user.Balance1 + amount
                };
                Balance balance = new Balance()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Balance1 = user.Balance1 + amount,
                };
                _Db.Infoes.Add(data);
                _Db.Balances.Add(balance);
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Transfer(int id)
        {
            var user = _Db.Balances.FirstOrDefault(c => c.Id == id);
            return View(user);
        }
        [HttpPost]
        public ActionResult ConfTransfer(int id, int accnumber,int amount)
        {
            var user = _Db.Balances.FirstOrDefault(c => c.Id == id);
            var anuser = _Db.Balances.FirstOrDefault(c => c.Id == accnumber);
            if (anuser == null)
            {
                throw new ApplicationException("Please Enter Account Number");
            }
            if (amount <= 0)
            {
                throw new ApplicationException("Please check the Ammount You Entered");
            }
            if (user.Balance1<amount)
            {
                throw new ApplicationException("Invalid Transfer Amount");
                return RedirectToAction("Transfer", new {id=id});

            }
            if (user.Balance1 > amount)
            {
                Info data = new Info()
                {
                    UserId = user.Id,
                    Date = DateTime.Now,
                    Action = "Transfer Funds",
                    amount = amount,
                    Balance = user.Balance1 - amount
                };
                _Db.Infoes.Add(data);
                user.Balance1-=amount;
                anuser.Balance1 += amount;
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}