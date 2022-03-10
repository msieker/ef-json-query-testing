using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace ef_json_query_testing.Tests
{
    public class SearchServiceTests
    {
        private readonly ITestOutputHelper output;
        private readonly ISearchService _service;
        private readonly EfTestDbContext _context;

        public SearchServiceTests(ITestOutputHelper output)
        {
            this.output = output;
            _context = EfTestDbContext.Create(true, output.WriteLine);
            _service = new SearchService(_context);

            //CreateBogusData.LoadAllData(_context, 15, 20, 5);
        }


        [Theory]
        [InlineData(0, "asdasdasd")]
        [InlineData(1, "00000000000")]
        public async Task ContainsOrEquals_NoMatch(int i, string str)
        {
            var media = await _service.TableSearch_Media(i, str);

            media.Should().BeEmpty();
        }

        // SearchServiceTests should run once for this class

         //this test refuses to work for whatever reason, but runs when program is run.
        [Theory]
        [InlineData(1, "askkkkkkkkkkkkkkkkkkdasd")]
        public async Task RAW_SqlInterpolated_NoMatch(int i, string str)
        {
            var media = await _service.JsonSearch_Raw(i, str);

            media.Should().BeEmpty();
        }


        [Theory]
        [InlineData(4, "6")]
        [InlineData(34, "voluptate")]
        public async Task RAW_SqlInterpolated_Matches(int i, string str)
        {
            var media = await _service.JsonSearch_Raw(i, str);

            media.Should().NotBeEmpty();
        }
    }
}
