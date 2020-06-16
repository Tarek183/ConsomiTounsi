using Solution.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using ConsomiTounsi.Service;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.web.Models;
using Microsoft.AspNet.Identity;
using ConsomiTounsi.service;

namespace ConsomiTounsi.web.Controllers
{
    public class ProducttController : Controller {
        IProductService Service;
        IRayonService ServiceRayon;
        IUserService US;
        //IRayonService postService = null;
        // GET: Productt
        public ProducttController()
        {
            Service = new ProductService();
            ServiceRayon = new RayonService();
            US = new UserService();
            //rayonService = new rayonService();
        }


        public ActionResult Index2()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            var user = US.GetById(userId);
            string UserName = user.UserName;
            string Phone = user.Phone;
            string mail = user.Email;
            string image = user.image;
            string role = user.Role;

            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;

            ViewBag.Role = role;
            ViewBag.authenticated = val1;
            Dictionary<string, int> pieChartName = new Dictionary<string, int>();
            pieChartName = Service.GetChart();
            return View(pieChartName);
        }
        // GET: Productt/Mailing/5
        public ActionResult Mailing(int? id)
        {
            Product product = new Product();
            ProductM p = new ProductM();
            product = Service.GetById((int)id);
            p.IdProduct = product.IdProduct;
            p.Nom = product.Nom;
            p.qte = product.qte;
         



            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("iines4802@gmail.com");
                mail.To.Add("iines4802@gmail.com");
                mail.Subject = "Hello World";
                mail.Body = "<h1>Hello !</h1>" + p.Nom + "veillez nous envoyer svp" + p.qte;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("iines4802@gmail.com", "Password");
                    smtp.EnableSsl = true;
                    //smtp.Send(mail);

                }
            }

            Service.Update(product);
            Service.Commit();
            return View(p);
        }
        public ActionResult sendMail(int? id)
        {
            
            Product product = new Product();
            ProductM p = new ProductM();
            product = Service.GetById((int)id);
            p.IdProduct = product.IdProduct;
            p.qte = product.qte;


            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("iines4802@gmail.com");
                mail.To.Add("iines4802@gmail.com");
                mail.Subject = "Hello World";
                mail.Body = "<h1>Hello !</h1>"+ product.Nom +"veillez nous envoyer svp"+ p.qte;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("iines4802@gmail.com", "Password");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                }
            }

            Service.Update(product);
            Service.Commit();
            return View(p);

        }
        public ActionResult Index(String searchString)
        {

            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            if (!val1)
                return View();
            var userId = User.Identity.GetUserId();
            var user = US.GetById(userId);
            string UserName = user.UserName;
            string Phone = user.Phone;
            string mail = user.Email;
            string image = user.image;
            string role = user.Role;

            ViewBag.Email = mail;
            ViewBag.phone = Phone;
            ViewBag.UserName = UserName;

            ViewBag.Role = role;
            ViewBag.authenticated = val1;

            List<ProductM> Products = new List<ProductM>();
            List<Product> ProductDomain = Service.GetMany().ToList();

            foreach (Product p in ProductDomain)
            {
                Products.Add(new ProductM
                {
                    IdProduct = p.IdProduct,
                    Nom = p.Nom,
                    imageP = p.imageP,
                    description = p.description,
                    barcode = p.barcode,
                    marque = p.marque,
                    price = p.price,
                    dateC = p.dateC,
                    dateF = p.dateF,
                    IdRayon = p.IdRayon


                });

            }
          
            return View(Products);
        }
        public ActionResult Indexid(int id)
        {

            List<ProductM> Products = new List<ProductM>();
            List<Product> ProductDomain = Service.GetMany().ToList();
            var productss = Service.GetMany();
            productss = productss.Where(p => p.IdRayon == id);
            foreach (Product p in productss)
            {
                Products.Add(new ProductM
                {
                    IdProduct = p.IdProduct,
                    Nom = p.Nom,
                    imageP = p.imageP,
                    description = p.description,
                    barcode = p.barcode,
                    marque = p.marque,
                    price = p.price,
                    dateC = p.dateC,
                    dateF = p.dateF,
                    IdRayon = p.IdRayon


                });

            }
            return View(Products);

        }

        // GET: Productt/Details/5
        public ActionResult Details(int? id)
        {
            List<Rayon> Rayons = ServiceRayon.GetMany().ToList();
            ViewBag.MyRayons = new SelectList(Rayons, "IdRayon", "typeR");
            Product product = new Product();
            ProductM p = new ProductM();
            product = Service.GetById((int)id);
            p.IdProduct = product.IdProduct;
            p.Nom = product.Nom;
            p.imageP = product.imageP;
            p.description = product.description;
            p.barcode = product.barcode;
            p.IdRayon = product.IdRayon;
            p.marque = product.marque;
            p.price = product.price;
            p.dateC = product.dateC;
            p.dateF = product.dateF;
            p.qte = product.qte;
            


            return View(p);
        }

        // GET: Productt/Create
        public ActionResult Create()
        {
            List<Rayon> Rayons = ServiceRayon.GetMany().ToList();
            ViewBag.MyRayons = new SelectList(Rayons, "IdRayon", "typeR");
            return View();
        }

        // POST: Productt/Create
        [HttpPost]
        public ActionResult Create(ProductM ProductM, HttpPostedFileBase Image,HttpPostedFileBase  barCodeUpload)
        {
            string strin = string.Empty;
            string localSavePath = "~/Content/Uploads/";
            string str = string.Empty;
            string strImage = string.Empty;
            string strBarCode = string.Empty;




            Product p = new Product()
            {
                IdProduct = ProductM.IdProduct,
                Nom = ProductM.Nom,
                imageP = Image.FileName,
                //imageP = ProductM.imageP,
                description = ProductM.description,
                barcode = barCodeUpload.FileName,
                marque = ProductM.marque,
                price = ProductM.price,
                dateC = ProductM.dateC,
                dateF = ProductM.dateF,
                IdRayon = ProductM.IdRayon,
                qte = ProductM.qte

            };
          
            if (ProductM.dateC > DateTime.Today || ProductM.dateC == DateTime.Today)
            {
                strin = "the date of creation is not valid ";
                ViewBag.ErrorMessage = strin;

            }
            if (ProductM.dateF < DateTime.Today || ProductM.dateF == DateTime.Today)
            {
                strin = "the date of exp is not valid ";
                ViewBag.ErrorMessage = strin;

            }

            else
            {

               
                // Sauvgarde de l'image

                var path1 = Path.Combine(Server.MapPath("~/Content/Uploads/"), Image.FileName);
                Image.SaveAs(path1);
                var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), barCodeUpload.FileName);
                barCodeUpload.SaveAs(path);
                strImage = "http://localhost:" + Request.Url.Port + "~/Content/Uploads/" + p.barcode;
                strBarCode = ReadBarCodeFromFile(path);

                if(strBarCode != "This is the right barcode #tunisia")
                {
                    Response.Write("<script>alert('Data inserted successfully')</script>");

                    Service.Add(p);
                    Service.Commit();

                }
                else
                {
                    Response.Write("<script>alert('Data not inserted! faileds ')</script>");

                    return Content("<script language='javascript' type='text/javascript'>alert('100% tunisian product! #619');</script>");

                }

            }

            ViewBag.ErrorMessage = str;
            ViewBag.BarCode = strBarCode;
            ViewBag.BarImage = strImage;
            return (RedirectToAction("Index"));
        }

        public int CategoriePr(string categorie)
        {   Product product = new Product();
            List<Product> Products = Service.GetMany().ToList();
            int g = Products.Count();
            Products = Products.Where(pr => pr.CategorieP.ToString() == categorie).ToList();
            //Products = Products.Where(pr => pr.CategorieP.ToString() == categorie).ToList();
            int i = Products.Count();
            int k = i * g / 100;
            return k;
        }


        public ActionResult Edit(int id)
        {
            Product product = new Product();
            ProductM productM = new ProductM();
            product = Service.GetById(id);
            productM.description = product.description;
            productM.Nom = product.Nom;
            productM.marque = product.marque;
            productM.barcode = product.barcode;
            productM.price = product.price;
            productM.qte = product.qte;
            return View(productM);
        }

        // POST: Productt/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductM p)
        {
            Product product = new Product();
            product = Service.GetById(id);
            product.description = p.description;
            product.Nom = p.Nom;
            product.marque = p.marque;
            product.barcode = p.barcode;
            product.price = p.price;
            product.qte = p.qte;



            Service.Update(product);
            Service.Commit();
            return RedirectToAction("index");
            //return View();
        }




        // GET: Productt/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = new Product();
            ProductM p = new ProductM();
            product = Service.GetById((int)id);
            p.IdProduct = product.IdProduct;
            p.Nom = product.Nom;
            p.imageP = product.imageP;
            p.description = product.description;
            p.barcode = product.barcode;
            p.marque = product.marque;
            p.price = product.price;
            p.dateC = product.dateC;
            p.dateF = product.dateF;
            p.qte = product.qte;
            return View(p);
        }

        // POST: Productt/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Product product = Service.GetById((int)id);
            Service.Delete(product);
            Service.Commit();
            return RedirectToAction("Index");
        }
        public ActionResult barcode()
        {
            return View();
        }
        // POST: Productt/barcode
        [HttpPost]

        public ActionResult barcode(HttpPostedFileBase  barCodeUpload)
        {
            string localSavePath = "~/Content/Uploads/";
            string str = string.Empty;
            string strImage = string.Empty;
            string strBarCode = string.Empty;

            if (barCodeUpload != null)
            {
                string fileName = barCodeUpload.FileName;
                localSavePath += fileName;
                var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), barCodeUpload.FileName);
                barCodeUpload.SaveAs(path);
                Bitmap bitmap = null;
                try
                {
                    bitmap = new Bitmap(barCodeUpload.InputStream);

                }
                catch (Exception ex)
                {
                    ex.ToString();

                }
                if (bitmap == null)
                {
                    str = "file is not image";
                }
                else
                {
                    strImage = "http://localhost:" + Request.Url.Port + "~/Content/Uploads/" + fileName;
                    strBarCode = ReadBarCodeFromFile(path);

                }
            }
            else
            {
                str = "please upload the barcode image ";
            }
            ViewBag.ErrorMessage = str;

            ViewBag.BarCode = strBarCode;
            ViewBag.BarImage = strImage;
            return View();
        }
        private string ReadBarCodeFromFile(string v)
        {
            //string[] barcodes = BarcodeScanner.Scan(v, BarcodeType.Code39);
            //if (barcodes[0].Substring(0, 3) == "8RE")
            //{
            //    return "This is the right barcode #tunisia";
            //}
            //else
            //{
            //    return " This is not the right Tunisian product "+ barcodes[0].Substring(0, 3) ;
            //}
            return "This is the right barcode #tunisia";

        }
        // GET: Productt/barcodeGenerer
        public ActionResult barcodeGenerer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult barcodeGenerer(string barcode)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(barcode.Length * 40, 80))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        Font oFont = new Font("IDAutomationHC39M", 16);
                        PointF point = new PointF(2f, 2f);
                        SolidBrush whiteBrush = new SolidBrush(Color.White);

                        graphics.FillRectangle(whiteBrush, 0, 0, bitmap.Width, bitmap.Height);
                        SolidBrush blackBrush = new SolidBrush(Color.DarkBlue);
                        graphics.DrawString("*" + barcode + "*", oFont, blackBrush, point);
                    }
                    bitmap.Save(memoryStream, ImageFormat.Jpeg);
                    ViewBag.BarCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());


                }

            }
            return View();
        }
        public ActionResult EnvoyerMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EnvoyerMail(ProductM p)
        {
            //var x = es.GetById(id).mail;
            MailMessage mm = new MailMessage("ines.atia@esprit.tn", "ines.atia@esprit.tn");
            mm.Subject = "we";
            mm.Body = p.Nom;
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("ines.atia@esprit.tn", " Password");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            return View();
        }
    }
}
