using Bogus;
using ef_json_query_testing.Data.Enums;
using ef_json_query_testing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// DynamicListTypes - types of list dropdowns 
// DynamicListItems - the items available for each list type
// DynamicFields - list of additional fields a media item can have
// Media_Dynamic - media item with no json, linked to use DynamicMediaInformation

// Media_Json - uses a json field to store all additional fields.

// DynamicMediaInformation - each saved field for a given media item


namespace ef_json_query_testing.Data.Seeders
{
    public static class CreateBogusData
    {
        private const int _ListTypeCount = 10;

        private const int _ListItemCount_Min = 3;
        private const int _ListItemCount_Max = 20;

        private const int _Media_Dynamic_CreatedDayRange = 10;
        private const int _Media_Dynamic_FileWidth_Min = 256;
        private const int _Media_Dynamic_FileWidth_Max = 8192;
        private const int _Media_Dynamic_FileHeight_Min = 256;
        private const int _Media_Dynamic_FileHeight_Max = 4096;

        public static void LoadDate(EfTestDbContext context)
        {
            LoadDynamicListTypes(context);


            context.DynamicFields.AddRange(FakerDynamicField.Generate(50));
            context.SaveChanges();

            context.Media_Dynamic.AddRange(FakerMedia_Dynamic.Generate(1000));
            context.SaveChanges();


            LoadMediaInformation(context);

            LoadJMediaJson(context);
        }

        private static void LoadDynamicListTypes(EfTestDbContext context)
        {
            // List types
            var faker = new Faker();
            var cats = faker.Commerce.Categories(_ListTypeCount);
            var dynamicListTypes = new List<DynamicListType>();
            foreach (var c in cats)
            {
                dynamicListTypes.Add(new DynamicListType(c));
            }

            context.DynamicListTypes.AddRange(dynamicListTypes);
            context.SaveChanges();

            // List items
            LoadDynamicListItems(context);

            // adding list fields for each list type.
            var dynamicFields = new List<DynamicField>();
            foreach (var t in dynamicListTypes)
            {
                var field = new DynamicField(t.DisplayName);
                field.IsQueryable = true;
                field.Description = faker.Lorem.Paragraph();
                field.DataType = Enums.DataTypes.IntValue;
                field.DynamicListTypeId = t.DynamicListTypeId;

                dynamicFields.Add(field);
            }

            context.DynamicFields.AddRange(dynamicFields);
            context.SaveChanges();
        }

        private static void LoadDynamicListItems(EfTestDbContext context)
        {
            var faker = new Faker();
            var dynamicListItems = new List<DynamicListItem>();

            foreach (var t in context.DynamicListTypes)
            {
                var itemCount = faker.Random.Number(_ListItemCount_Min, _ListItemCount_Max);
                for (int j = 0; j < itemCount; j++)
                {
                    dynamicListItems.Add(new DynamicListItem(faker.Commerce.ProductName(), t.DynamicListTypeId));
                }
            }

            context.DynamicListItems.AddRange(dynamicListItems);
            context.SaveChanges();
        }

        private static void LoadMediaInformation(EfTestDbContext context)
        {
            //TODO
        }

        private static void LoadJMediaJson(EfTestDbContext context)
        {
            var mediaJson = new List<Media_Json>();
            foreach (var media in context.Media_Dynamic)
            {
                mediaJson.Add(media.GetMediaJsonCopy());
            }
            context.Media_Json.AddRange(mediaJson);
            context.SaveChanges();
        }



        public static Faker<DynamicField> FakerDynamicField => new Faker<DynamicField>()
            .RuleFor(d => d.DisplayName, f => f.Lorem.Word())
            .RuleFor(d => d.JsonName, (f, d) => d.MakeJsonName())
            .RuleFor(d => d.IsQueryable, f => f.Random.Bool())
            .RuleFor(d => d.IsRequired, f => f.Random.Bool())
            .RuleFor(d => d.Description, f => f.Lorem.Paragraph())
            .RuleFor(d => d.DataType, f => f.Random.Enum<DataTypes>());

        public static Faker<Media_Dynamic> FakerMedia_Dynamic => new Faker<Media_Dynamic>()
            .RuleFor(m => m.OriginalFileName, f => f.System.FileName())
            .RuleFor(m => m.FilePath, f => f.System.DirectoryPath())
            .RuleFor(m => m.CreatedDate, f => f.Date.Recent(_Media_Dynamic_CreatedDayRange))
            .RuleFor(m => m.FileSize, f => f.Random.Number())
            .RuleFor(m => m.FileWidth, (f, m) => f.Random.Bool() ? f.Random.Number(_Media_Dynamic_FileWidth_Min, _Media_Dynamic_FileWidth_Max) : null)
            .RuleFor(m => m.FileHeight, (f, m) => m.FileWidth.HasValue ? f.Random.Number(_Media_Dynamic_FileHeight_Min, _Media_Dynamic_FileHeight_Max) : null)
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.Hold, f => f.Random.Bool());
    }
}
