using DataAccessObject.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessObject.Test.MockedObject
{
    public class MockedClass
    {
        public Mock<ICalculationDAO> MockCalculationDAO { get; set; }

        public MockedClass()
        {
            createMockObject();
        }

        private void createMockObject()
        {
            MockCalculationDAO = new Mock<ICalculationDAO>() { CallBase = true};
        }



}
}
