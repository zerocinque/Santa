using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Santa.Classes;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santa.Tests
{
    [TestClass]
    public class IDataBaseTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateOrder_NullID_ThowException()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();
            mock.Setup(m => m.UpdateOrder(It.Is<Order>(order => order.ID == null))).Throws<ArgumentNullException>();

            MockOrderControllerTest mockController = new MockOrderControllerTest(mock.Object);

            mockController.Edit(new Order { ID = null, Status = OrderStatus.Done, Kid = "pippo", RequestDate = new DateTime(), Toys = new List<Toy>() });
        }
    }
}
