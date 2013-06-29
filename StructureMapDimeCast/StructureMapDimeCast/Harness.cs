using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StructureMap;

namespace StructureMapDimeCast
{
    [TestFixture]
    public class Harness
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void HarnessRunner()
        {
            ObjectFactory.Initialize(x =>
                {
                    x.For<ISoda>().Use<DrPepper>();
                    //x.For<Consumer>().Use<Consumer>();
                });

            var soda = ObjectFactory.GetInstance<ISoda>();

            var consumer = ObjectFactory.GetInstance<Consumer>();

            Assert.That(consumer, Is.Not.Null);
        }
    }
}
