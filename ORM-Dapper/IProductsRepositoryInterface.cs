using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public interface IProductsRepositoryInterface
    {
        IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }
        void CreateProduct(string name, double price, int categoryID)
        {
            throw new NotImplementedException();
        }
    }
}
