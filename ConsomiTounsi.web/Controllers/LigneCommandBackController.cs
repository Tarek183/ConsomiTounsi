using ConsomiTounsi.Service;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsomiTounsi.web.Controllers
{
    public class LigneCommandBackController : Controller
    {
        public ILigneCommandService ligneCommandService;
        public IProductService productService; 
        public LigneCommandBackController()
        {
            ligneCommandService = new LigneCommandService();
            productService = new ProductService();
        }
        // GET: LigneCommandBack
        public ActionResult Index()
        {
           Dictionary<int? , int> piechart =  ligneCommandService.GetChart();
           Dictionary<String, int> pieChartName = new Dictionary<String, int>();
           
            foreach (KeyValuePair<int? , int> entry in piechart )
            {
              
                pieChartName.Add(productService.GetNameByid(entry.Key) , entry.Value) ;
            }


            return View(pieChartName);
        }
    }
}