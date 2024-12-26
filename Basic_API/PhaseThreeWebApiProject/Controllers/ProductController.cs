using System.Collections.Generic;
using System.Web.Http;
using PhaseThreeWebApiProject.DAL;
using PhaseThreeWebApiProject.Models;
namespace PhaseThreeWebApiProject.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductDAL productDAL = new ProductDAL();

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var products = productDAL.GetAllProducts();
            return Ok(products);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var product = productDAL.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public IHttpActionResult Create(Product product)
        {
            productDAL.AddProduct(product);
            return Ok("Product created successfully.");
        }

        [HttpPut]
        public IHttpActionResult Update(Product product)
        {
            productDAL.UpdateProduct(product);
            return Ok("Product updated successfully.");
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            productDAL.DeleteProduct(id);
            return Ok("Product deleted successfully.");
        }
    }
}