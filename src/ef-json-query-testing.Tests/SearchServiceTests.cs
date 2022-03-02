using ef_json_query_testing.Data.Seeders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public class MediaJsonSearch : SearchServiceTests
        {
            // SearchServiceTests should run once for this class

            [Theory]
            [InlineData(0, "asdasdasd")] // field doesnt exist
            [InlineData(1, "00000000000")] // string shouldnt exist
            public void JsonDocument_NoMatch(int i, string str)
            {
                var media = _service.MediaJsonSearch_JsonDocument(i, str);

                Assert.Empty(media);
            }

            [Theory]
            [InlineData(0, "asdasdasd")]
            [InlineData(1, "00000000000")]
            public void JsonDocumentCombo_NoMatch(int i, string str)
            {
                var media = _service.MediaJsonSearch_JsonDocumentCombo(i, str);

                Assert.Empty(media);
            }

            [Theory]
            //[InlineData(0, "asdasdasd")]
            [InlineData(1, "askkkkkkkkkkkkkkkkkkdasd")]
            public void RAW_SqlInterpolated_NoMatch(int i, string str)
            {
                var media = _service.MediaJsonSearch_RAW_SqlInterpolated(i, str);

                Assert.Empty(media);
            }
        }

        public class MediaTableSearch : SearchServiceTests
        {
            // SearchServiceTests should run once for this class

            [Theory]
            [InlineData(0, "asdasdasd")]
            [InlineData(1, "00000000000")]
            public void OnlyContains_NoMatch(int i, string str)
            {
                var media = _service.MediaTableSearch_OnlyContains(i, str);

                Assert.Empty(media);
            }

            [Theory]
            [InlineData(0, "asdasdasd")]
            [InlineData(1, "00000000000")]
            public void ContainsOrEquals_NoMatch(int i, string str)
            {
                var media = _service.MediaTableSearch_ContainsOrEquals(i, str);

                Assert.Empty(media);
            }
        }
    }
}
