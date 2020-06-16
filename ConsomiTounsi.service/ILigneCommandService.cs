using ConsomiTounsi.domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.Service {
    public interface ILigneCommandService : IService<LigneCommand> {

        int GetByPanier(int id);
        void DeleteALL(int id);
        List<LigneCommand> GetAllPanier(int id);

        int exist(int panier, int produit);
        LigneCommand HetLigneCommand(int id);
        void Increment(int panier, int produit);
        int DecrementLigne(int idl);
        void IncrementLigne(int idl);
        void DeleteLigne(int idl);
        void Decrement(int panier, int produit);
        double PanierPrice(int panier);
        Dictionary<int?, int> GetChart();
    }
}
