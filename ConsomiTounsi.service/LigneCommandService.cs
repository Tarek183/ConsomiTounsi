using ConsomiTounsi.data.Infrastructure;
using ConsomiTounsi.domain.Entities;
using Service.Pattern;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.Service
{
    public class LigneCommandService : Service<LigneCommand>, ILigneCommandService {

        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public LigneCommandService() : base(utk)
        {

        }

        public void DeleteALL(int id)
        {
            Delete(l => l.PanierId.ToString().Equals(id.ToString()));
        }

        public int GetByPanier(int id)
        {

            IEnumerable<LigneCommand> aa = GetMany(l => l.PanierId.ToString().Equals(id.ToString()));

            return aa.Count();
        }
        public List<LigneCommand> GetAllPanier(int id)
        {
            IEnumerable<LigneCommand> aa = GetMany(l => l.PanierId.ToString().Equals(id.ToString()));
            return aa.ToList();
        }

        public int exist(int panier, int produit)
        {
            return GetMany(l => l.PanierId.ToString().Equals(panier.ToString()))
                .Where(l => l.ProduitId.ToString().Equals(produit.ToString())).Count();

        }

        public LigneCommand HetLigneCommand(int id)
        {
            return GetById(id.ToString());
        }
        public void Increment(int panier, int produit)
        {
            List<LigneCommand> aa = GetMany(l => l.PanierId.ToString().Equals(panier.ToString()))
                   .Where(l => l.ProduitId.ToString().Equals(produit.ToString())).ToList();
            LigneCommand ligne = aa.First();
            ligne.quantite++;
            ligne.date = DateTime.Today;
            Update(ligne);
            Commit();

        }
        public void IncrementLigne(int idl)
        {
            LigneCommand Ligne = new LigneCommand();
            Ligne = Get(l => l.idLigneCommand.ToString().Equals(idl.ToString()));
            Ligne.quantite++;
            Update(Ligne);
            Commit();


        }
        public int DecrementLigne(int idl)
        {
            int q = 0;

            LigneCommand Ligne = new LigneCommand();
            Ligne = Get(l => l.idLigneCommand.ToString().Equals(idl.ToString()));
            q = Ligne.quantite;
            if (q == 2)
            {

                Ligne.quantite--;
                Update(Ligne);
                Commit();
                return 0;
            }
            if (q > 2)
            {
                Ligne.quantite--;
                Update(Ligne);
                Commit();
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public void Decrement(int panier, int produit)
        {
            List<LigneCommand> aa = GetMany(l => l.PanierId.ToString().Equals(panier.ToString()))
                   .Where(l => l.ProduitId.ToString().Equals(produit.ToString())).ToList();
            LigneCommand ligne = aa.First();
            if (ligne.quantite == 1)
            {

            }
            else
            {
                ligne.quantite--;
                ligne.date = DateTime.Today;
                Update(ligne);
                Commit();
            }


        }
        public void DeleteLigne(int idl)
        {
            LigneCommand Ligne = Get(l => l.idLigneCommand.ToString().Equals(idl.ToString()));
            Delete(Ligne);
            Commit();
        }


        public double PanierPrice(int panier)
        {
            IProductService productservice = new ProductService();

            List<LigneCommand> ligne = GetMany(l => l.PanierId.ToString().Equals(panier.ToString())).ToList();
            double a = ligne.Sum(l => l.quantite * productservice.GetPriceProdtuit(l.ProduitId));

            return a;

        }
        public Dictionary<int? , int> GetChart()
        {
            IProductService productService = new ProductService();
            IList<int?> listProduit = new List<int?>();
            Dictionary<int?, int> PieChart = new Dictionary<int?, int>();
           
            GetMany().ToList().ForEach(delegate (LigneCommand l)
            {
                

                if (PieChart.ContainsKey(l.ProduitId))
                {               
                    PieChart[l.ProduitId] = PieChart[l.ProduitId] + l.quantite;
                }
                else
                {

                    PieChart.Add(l.ProduitId, l.quantite);
                }
            });

            return PieChart;
        }
        
    }
}