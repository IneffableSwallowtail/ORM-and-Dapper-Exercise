using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductsRepositoryInterface
    {
        private readonly IDbConnection _connection;
        //Constructor
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Products> GetAllProducts()
        {
            return _connection.Query<Products>("SELECT * FROM Products;");
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Query<Products>("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @CategoryID);",
             new { Name = name, Price = price, CategoryID = categoryID });
        }
    }
}
