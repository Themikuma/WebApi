using LHCRUD.Data.Common;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LHCRUD.Data.Tests
{
    public class Tests
    {

        [Test]
        public async Task TestGetRandomData()
        {
            int count = 20;
            var books = await DataRetrieval.GetBooks(count);
            Assert.AreEqual(count, books.Count);
        }

        [Test]
        public async Task TestGetRandomDataWithNoQuantity()
        {
            int count = 0;
            var books = await DataRetrieval.GetBooks(count);
            //Default quantity is 10
            Assert.AreEqual(10, books.Count);
        }
        [Test]
        public async Task TestGetRandomDataWithBiggerThanAllowed()
        {
            int count = 123456;
            var books = await DataRetrieval.GetBooks(count);
            //Max quantity is 10
            Assert.AreEqual(1000, books.Count);
        }


    }
}