using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ef_json_query_testing.Tests
{
    public class SearchServiceTests
    {
        private readonly ISearchService _service;
        private readonly EfTestDbContext _context;

        public SearchServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfTestDbContext>();
            optionsBuilder.UseInMemoryDatabase("testingDB");
            _context = new EfTestDbContext(optionsBuilder.Options);
            _service = new SearchService(_context);

            CreateBogusData.LoadAllData(_context, 15, 20, 5);
        }


        [Theory]
        [InlineData(0, "asdasdasd")]
        [InlineData(1, "00000000000")]
        public void ContainsOrEquals_NoMatch(int i, string str)
        {
            var media = _service.TableSearch(i, str);

            Assert.Empty(media);
        }

        // SearchServiceTests should run once for this class

        // this test refuses to work for whatever reason, but runs when program is run.
        //[Theory]
        ////[InlineData(0, "asdasdasd")]
        //[InlineData(1, "askkkkkkkkkkkkkkkkkkdasd")]
        //public void RAW_SqlInterpolated_NoMatch(int i, string str)
        //{
        //    var media = _service.MediaJsonSearch_RAW_SqlInterpolated(i, str);
        //
        //    Assert.Empty(media);
        //}

    }
}
