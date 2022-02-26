using CodeExcercise.Controllers;
using CodeExercise_Model.Interfaces;
using CodeExercise_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeExercise.Test
{
    [Collection("Product")]
    public class ProductRepositoryTest
    {


        

        private readonly Mock<IGenericRepository<Product>> service;
        public ProductRepositoryTest()
        {
            service = new Mock<IGenericRepository<Product>>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProductList_WhenProductExists()
        {
            //arrange
            //var products = GetSampleProducts();
            service.Setup(x => x.GetAllAsync())
                .ReturnsAsync(GetSampleProducts);
            var controller = new ProductsController(service.Object);

            //act
            var actionResult = controller.Get();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<Product>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleProducts().Count(), actual.Count());

        }

        [Fact]
       
        public async Task GetAsync_ShouldReturnProductById_WhenProductExists()
        {
            //arrange
            
            int idtoFind = 1;
            service.Setup(x => x.GetAsync(idtoFind))
                .ReturnsAsync(GetSampleProducts().FirstOrDefault());
            var controller = new ProductsController(service.Object);

            //act
            var actionResult =controller.GetProduct(idtoFind);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as Product;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(GetSampleProductById(idtoFind)), JsonConvert.SerializeObject(actual));

        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAsync_ShouldReturnProductById_WhenProductExistsParameters(int idtoFind)
        {
            //arrange
           
            service.Setup(x => x.GetAsync(idtoFind))
                .ReturnsAsync(GetSampleProducts().FirstOrDefault());
            var controller = new ProductsController(service.Object);

            //act
            var actionResult = controller.GetProduct(idtoFind);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as Product;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(GetSampleProductById(idtoFind)), JsonConvert.SerializeObject(actual));

        }





        private static List<Product>  GetSampleProducts()
        {
            List<Product> entities = new List<Product>() {
                new Product() { Id=1, AgeRestriction=2, Company="Mattel",Description="A new toy from mattel",Name="rex", Price=255, ImageUrl= "https://http2.mlstatic.com/D_NQ_NP_799752-MLM44066560930_112020-O.webp" },
                new Product() { Id=2, AgeRestriction=5, Company="Disney",Description="A new toy from disney",Name="Princess", Price=300, ImageUrl= "https://http2.mlstatic.com/D_NQ_NP_799752-MLM44066560930_112020-O.webp" },
            };

            return entities;
        }

        private static Product GetSampleProductById(int id)
        {
            return GetSampleProducts().FirstOrDefault(x=>x.Id==id);
        }


      

    }
}
