using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Santa.Classes;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Santa.Tests
{
    [TestClass]
    public class MockIDataBaseTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MockUpdateOrder_NullOrEmptyID_ThowException()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.UpdateOrder(It.Is<Order>(order => string.IsNullOrEmpty(order.ID) == true))).Throws<ArgumentNullException>();

            MockControllerTest mockController = new MockControllerTest(mock.Object);

            mockController.OrderEdit(new Order { ID = null, Status = OrderStatus.Done, Kid = "pippo", RequestDate = new DateTime(), Toys = new List<Toy>() });
            mockController.OrderEdit(new Order { ID = "", Status = OrderStatus.Done, Kid = "pippo", RequestDate = new DateTime(), Toys = new List<Toy>() });
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MockUpdateOrder_InvalidID_ThowException()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            Regex regex = new Regex("/[ ]+/");
            mock.Setup(m => m.UpdateOrder(It.Is<Order>(order => order.ID.Length != 24))).Throws<ArgumentException>();
            mock.Setup(m => m.UpdateOrder(It.Is<Order>(order => regex.Match(order.ID).Success == true))).Throws<ArgumentException>();

            MockControllerTest mockController = new MockControllerTest(mock.Object);

            mockController.OrderEdit(new Order { ID = "123456789", Status = OrderStatus.Done, Kid = "pippo", RequestDate = new DateTime(), Toys = new List<Toy>() });
            mockController.OrderEdit(new Order { ID = "123456789789456123789456123", Status = OrderStatus.Done, Kid = "pippo", RequestDate = new DateTime(), Toys = new List<Toy>() });
            mockController.OrderEdit(new Order { ID = "  ", Status = OrderStatus.Done, Kid = "pippo", RequestDate = new DateTime(), Toys = new List<Toy>() });
        }
    }
}
