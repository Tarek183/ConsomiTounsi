
using ConsomiTounsi.data.Infrastructure;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.Service;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service {
    public class ProductService : Service<Product>, IProductService {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ProductService() : base(utk)
        {

        }

        public Product GetProductByName(string name)
        {
            return Get(f => f.Nom.Equals(name));
        }

        public string GetproducteById(int idproduct)
        {
            return GetById(idproduct).Nom;
        }
        public Product HetProduct(int id)
        {
            return GetById(id);
        }
        public IEnumerable<Product> GetProductsByCategorie(string CategorieP)
        {
            throw new NotImplementedException();
        }
        public Product HetProduct(int? id)
        {
            int a = id ?? 2;
            return GetById(a);
        }

        public double GetPriceProdtuit(int? produitid)
        {
            int a = produitid ?? 2;
            return GetById(a).price;
        }
        public String GetNameByid(int? id)
        {
            return Get(p => p.IdProduct.ToString().Equals(id.ToString())).Nom;
        }
        public void function()
        {
            Dictionary<String, int> PieChart = new Dictionary<String, int>();
            GetMany().ToList().ForEach(delegate (Product p)
            {
                if (PieChart.ContainsKey(p.CategorieP.ToString()))
                {
                    PieChart[p.CategorieP.ToString()] = PieChart[p.CategorieP.ToString()] + 1;
                }
                else
                {
                    PieChart.Add(p.CategorieP.ToString() , 1);
                }

            });
        }
        public Dictionary<string, int> GetChart()
        {
            Dictionary<string, int> PieChart = new Dictionary<string, int>();
            GetMany().ToList().ForEach(delegate (Product p)
            {
                if (PieChart.ContainsKey(p.CategorieP.ToString()))
                {
                    PieChart[p.CategorieP.ToString()] = PieChart[p.CategorieP.ToString()] + 1;
                }
                else
                {
                    PieChart.Add(p.CategorieP.ToString(), 1);
                }

            });

            return PieChart;
        }

        public IEnumerable<Product> GetProductsByRayon(string typeR)
        {
            throw new NotImplementedException();
        }
    } 
}
