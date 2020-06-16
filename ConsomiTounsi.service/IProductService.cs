using ConsomiTounsi.domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.Service
{
    public interface IProductService : IService<Product> {
        Product GetProductByName(string nom);
        string GetproducteById(int idproduct);
        Product HetProduct(int id);
        IEnumerable<Product> GetProductsByCategorie(string CategorieP);
        Product HetProduct(int? produitId);
        double GetPriceProdtuit(int? produitid);
         String GetNameByid(int? id);

        Dictionary<string, int> GetChart();
        IEnumerable<Product> GetProductsByRayon(string typeR);

    }
}
