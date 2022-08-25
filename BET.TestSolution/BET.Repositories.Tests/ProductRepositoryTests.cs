using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BET.Infrastructure.Common;
using BET.Infrastructure.Models;
using BET.Infrastructure.Repositories;
using BET.Repositories.DataContext;
using BET.Repositories.Repos;

namespace BET.Repositories.Tests
{
	[TestClass]
	public class ProductRepositoryTests
	{
		private IProductRepository _productRepository;
		private Product _testProduct = null;
		private User _testUser = null;

		public ProductRepositoryTests()
		{
			_testUser = new User
			{
				UserId = 1,
				FullName = "Test1",
				Login = "test1@test.com",
				Password = "Test1",
				IsActive = true,
				IsAdmin = true,
				IsDeleted = false
			};			

			_testProduct = new Product
			{
				ProductId = 1,
				LastModifiedUserId = 1,
				LastModifiedDateTime = DateTime.Today,
				Price = 1,
				Quantity = 1,
				Image = "test"
			};
			var context = new BETDataContext();
			
			context.Products = MockRepository.GetQueryableMockDbSet(_testProduct);
				context.Users = MockRepository.GetQueryableMockDbSet(_testUser);
			_productRepository = new ProductRepository(context);
		}

		[TestMethod]
		public async Task GetAllProductsData()
		{
			var result = await _productRepository.GetAllAsync().ConfigureAwait(false);
			Assert.IsTrue(result.ToList().Count > 0);
		}
	}
}
