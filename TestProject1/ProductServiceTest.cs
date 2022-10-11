using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol;
using ShoesSopAPI.Controllers;
using ShoesSopAPI.Data;
using ShoesSopAPI.Models;
using ShoesSopAPI.Services.Interfaces;

namespace TestProject1
{
    public class ProductServiceTest
    {
      
        [Fact]
        public void GetByIdAsync_ReturnProduct_WhenProductExists()
        {
          /*  //Arrange
            *//*var product = new Mock<DBShop>();
            int id = 1;
            product.Setup(p => p.SanPhams.FindAsync(id));*//*
            var db = new Mock<DBShop>();
            ProductController product = new ProductController(db.Object);

            //Act
            var listsp = product.GetSanPham();
            //Assert
            listsp.Should().NotBeNull();*/
        }
       
    }
       
}