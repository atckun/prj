using System.Linq;
using System.Threading.Tasks;
using AbstractProject.Api.Controllers;
using AbstractProject.BusinessLogic.Items.Commands.Items.CreateItem;
using AbstractProject.BusinessLogic.Items.Commands.Items.DeleteItem;
using AbstractProject.DataAccess;
using AbstractProject.Domain.Tables;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq.AutoMock;
using Xunit;

namespace AbstractProject.Tests.Controllers.Item
{
    public class CreateItemTest
    {
        [Fact]
        public async Task Create_CreateRecordToDatabase()
        {
            var options = new DbContextOptionsBuilder<AbstractProjectDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateRecordToDatabase")
                .Options;

            await using var context = new AbstractProjectDbContext(options);

            // var mocker = new AutoMocker();
            // var instance = mocker.CreateInstance<ItemController>();
            // var result = await instance.Create(new CreateItemCommand
            // {
                // Title = "123",
                // Description = "123"
            // });
           
            // // Arrange

            //
            // const string title = "title";
            // const string description = "description";
            //
            // // Act
            //
            // var mocker = new AutoMocker();
            // mocker.CreateInstance<ItemController>();
            // var instance = mocker.Get<ItemController>();
            // var tt = await instance.Create(new CreateItemCommand
            // {
            //     Title = "123",
            //     Description = "123"
            // });
            //
            // // Assert
            // Assert.Equal(1, tt.Value.Body);
            // Assert.Equal(1, context.Items.Count());
            // Assert.Equal("title", context.Items.Single().Title);
            // Assert.Equal("description", context.Items.Single().Description);
        }


        [Fact]
        public async Task Create_CreateRangeRecordsToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AbstractProjectDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateRangeRecordsToDatabase")
                .Options;

            const string title = "title";
            const string description = "description";

            // Act
            await using var context = new AbstractProjectDbContext(options);

            var entity = new ItemEntity(title, description);
            await context.Items.AddAsync(entity);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(1, context.Items.Count());
            Assert.Equal("title", context.Items.Single().Title);
            Assert.Equal("description", context.Items.Single().Description);
        }
    }
}