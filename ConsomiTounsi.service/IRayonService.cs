using ConsomiTounsi.domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.Service
{
    public interface IRayonService : IService<Rayon> {
        string GetrayoneById(int idrayon);

    }
}
