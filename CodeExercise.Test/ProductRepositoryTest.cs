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

        //[Fact]
        //public async Task GetAllAsync_ShouldReturnProductList_WhenThereAreProducts()
        //{
        //    //arrange
        //    //var products = GetSampleProducts();
        //    service.Setup(x => x.GetAllAsync())
        //        .ReturnsAsync(GetSampleProducts);
        //    var controller = new ProductsController(service.Object);

        //    //act
        //    ActionResult<Product> actionResult = await controller.Get();
        //    var result = actionResult.Result as OkObjectResult;
        //    var actual = result.Value as IEnumerable<Product>;

        //    //assert
        //    Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(GetSampleProducts().Count(), actual.Count());

        //}


        [InlineData(1)]
        [InlineData(2)]
        [Theory]
        public async Task GetAsync_ShouldReturnProductById_WhenProductExistsParametersAsync(int idtoFind)
        {
            //arrange

            service.Setup(x => x.GetAsync(idtoFind))
                .ReturnsAsync(GetSampleProducts().FirstOrDefault(x=>x.Id== idtoFind));
            var controller = new ProductsController(service.Object);

            //act
            ActionResult<Product> actionResult = await controller.GetProduct(idtoFind);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as Product;

            //////assert

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(GetSampleProductById(idtoFind)), JsonConvert.SerializeObject(actual));

        }
        [InlineData(999999999)]
        [InlineData(0)]
        [Theory]
        public async Task GetAsync_ShouldNotReturnProduct_WhenProductExistsParametersAsync(int idtoFind)
        {
            //arrange

            service.Setup(x => x.GetAsync(idtoFind))
                .ReturnsAsync(GetSampleProducts().FirstOrDefault(x => x.Id == idtoFind));
            var controller = new ProductsController(service.Object);

            //act
            ActionResult<Product> actionResult = await controller.GetProduct(idtoFind);
            var result = actionResult.Result as OkObjectResult;
           // var actual = result.Value != null ? result.Value as Product : null;

            //////assert

           
            Assert.Null(result);

        }
        //
        [Fact]
        public async Task AddAsync_ShouldCreateANewProduct()
        {
            //arrange
            var ProdcutToCreate = new Product() { 
                AgeRestriction = 10, 
                Company = "StarWars", 
                Description = "A new toy from StarWars",
                Name = "Baby Yoda", 
                Price = 777,
                ImageUrl = "https://http2.mlstatic.com/D_NQ_NP_799752-MLM44066560930_112020-O.webp" 
            };

            service.Setup(x => x.AddAsync(ProdcutToCreate))
                .ReturnsAsync(GetSampleProducts().FirstOrDefault(x => x.Id == ProdcutToCreate.Id));
            var controller = new ProductsController(service.Object);

            //act
            ActionResult<Product> actionResult = await controller.Post(ProdcutToCreate);
            var result = actionResult.Result as CreatedAtActionResult;
            var actual = result.Value != null ? result.Value as Product : null;

            //////assert


            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(ProdcutToCreate), JsonConvert.SerializeObject(actual));

        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteAProduct()
        {
            //arrange
            const int productIdToDelete = 1;

            service.Setup(x => x.DeleteAsync(productIdToDelete))
                 .ReturnsAsync(GetSampleProducts().FirstOrDefault(x => x.Id == productIdToDelete));

            var controller = new ProductsController(service.Object);

            //act
            ActionResult<Product> actionResult = await controller.Delete(productIdToDelete);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value != null ? result.Value as Product : null;

            //////assert


            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(productIdToDelete, actual.Id);

        }

        [Fact]
        public async Task PutAsync_ShouldUpdateAProduct()
        {
            var ProdcutToUpdate = new Product()
            {
                Id = 1,
                AgeRestriction = 10,
                Company = "StarWars",
                Description = "A new toy from StarWars",
                Name = "Baby Yoda",
                Price = 777,
                ImageUrl = "https://http2.mlstatic.com/D_NQ_NP_799752-MLM44066560930_112020-O.webp"
            };


            service.Setup(x => x.UpdateAsync(ProdcutToUpdate))
                 .ReturnsAsync(GetSampleProducts().FirstOrDefault(x => x.Id == ProdcutToUpdate.Id));

            var controller = new ProductsController(service.Object);

            //act
            ActionResult<Product> actionResult = await controller.Put(ProdcutToUpdate.Id, ProdcutToUpdate);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value != null ? result.Value as Product : null;

            //////assert


            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(ProdcutToUpdate), JsonConvert.SerializeObject(actual));


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
