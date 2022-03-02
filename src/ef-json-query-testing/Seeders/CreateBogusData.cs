﻿using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// DynamicListTypes - types of list dropdowns 
// DynamicListItems - the items available for each list type
// DynamicFields - list of additional fields a media item can have
// Media_Dynamic - media item with no json, linked to use DynamicMediaInformation

// Media_Json - uses a json field to store all additional fields.

// DynamicMediaInformation - each saved field for a given media item


namespace ef_json_query_testing
{
    public static class CreateBogusData
    {
        private const int _ListItemCount_Min = 3;
        private const int _ListItemCount_Max = 20;

        private const int _Media_Dynamic_CreatedDayRange = 10;
        private const int _Media_Dynamic_FileSize_Max = 2097152;
        private const int _Media_Dynamic_FileWidth_Min = 256;
        private const int _Media_Dynamic_FileWidth_Max = 8192;
        private const int _Media_Dynamic_FileHeight_Min = 256;
        private const int _Media_Dynamic_FileHeight_Max = 4096;

        public static void LoadAllData(EfTestDbContext context, int fieldsCount = 50, int mediaItemsCount = 1000, int listTypeCount = 10)
        {
            LoadSharedData(context, fieldsCount, listTypeCount);

            LoadMediaData(context, mediaItemsCount);
        }

        public static void LoadSharedData(EfTestDbContext context, int fieldsCount = 50, int listTypeCount = 10)
        {
            LoadDynamicListTypes(context, listTypeCount);


            context.DynamicFields.AddRange(FakerDynamicField.Generate(fieldsCount));
            context.SaveChanges();
        }

        public static void LoadMediaData(EfTestDbContext context, int mediaItemsCount = 1000)
        {
            context.Media_Dynamic.AddRange(FakerMedia_Dynamic.Generate(mediaItemsCount));
            context.SaveChanges();


            LoadMediaInformation(context);

            LoadMediaJson(context);
        }

        public static Faker<DynamicField> FakerDynamicField => new Faker<DynamicField>()
            .RuleFor(d => d.DisplayName, f => string.Join(" ", f.Lorem.Words()))
            .RuleFor(d => d.JsonName, (f, d) => d.MakeJsonName())
            .RuleFor(d => d.IsQueryable, f => f.Random.Bool())
            .RuleFor(d => d.IsRequired, f => f.Random.Bool())
            .RuleFor(d => d.Description, f => f.Lorem.Paragraph())
            .RuleFor(d => d.DataType, f => f.Random.Enum<DataTypes>());

        public static Faker<Media_Dynamic> FakerMedia_Dynamic => new Faker<Media_Dynamic>()
            .RuleFor(m => m.OriginalFileName, f => f.System.FileName())
            .RuleFor(m => m.FilePath, f => f.System.DirectoryPath())
            .RuleFor(m => m.CreatedDate, f => f.Date.Recent(_Media_Dynamic_CreatedDayRange))
            .RuleFor(m => m.FileSize, f => f.Random.Number(_Media_Dynamic_FileSize_Max))
            .RuleFor(m => m.FileWidth, (f, m) => f.Random.Bool() ? f.Random.Number(_Media_Dynamic_FileWidth_Min, _Media_Dynamic_FileWidth_Max) : null)
            .RuleFor(m => m.FileHeight, (f, m) => m.FileWidth.HasValue ? f.Random.Number(_Media_Dynamic_FileHeight_Min, _Media_Dynamic_FileHeight_Max) : null)
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.Hold, f => f.Random.Bool());

        private static void LoadDynamicListTypes(EfTestDbContext context, int listTypeCount)
        {
            // List types
            var faker = new Faker();
            var cats = faker.Commerce.Categories(listTypeCount);
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
                field.IsRequired = faker.Random.Bool();
                field.Description = faker.Lorem.Paragraph();
                field.DataType = DataTypes.IntValue;
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
            var requiredFields = context.DynamicFields.Where(f => f.IsRequired).ToList();
            var optionalFields = context.DynamicFields.Where(f => !f.IsRequired).ToList();

            var listItemsCounts = GetListItemCounts(context);

            var faker = new Faker();
            var mediaItems = context.Media_Dynamic.ToList();
            foreach (var item in mediaItems)
            {
                var infoItems = new List<DynamicMediaInformation>();
                infoItems.AddRange(GenerateFieldValues(item, requiredFields, faker, listItemsCounts));

                var randomCount = faker.Random.Number(0, optionalFields.Count());
                infoItems.AddRange(GenerateFieldValues(item, faker.PickRandom(optionalFields, randomCount), faker, listItemsCounts));

                item.DynamicMediaInformation = infoItems;
                context.SaveChanges();
            }
        }

        public static void LoadMediaJson(EfTestDbContext context)
        {
            var mediaJson = new List<Media_Json>();
            foreach (var media in context.Media_Dynamic.Include(m=>m.DynamicMediaInformation).ThenInclude(m=>m.Field))
            {
                mediaJson.Add(media.GetMediaJsonCopy());
            }

            context.Media_Json.AddRange(mediaJson);
            context.SaveChanges();
        }

        private static Dictionary<int, int> GetListItemCounts(EfTestDbContext context)
        {
            return context.DynamicListItems.GroupBy(d => d.DynamicListTypeId).Select(g => new { g.Key, Count = g.Count() }).ToDictionary(g => g.Key, g => g.Count);
        }

        private static List<DynamicMediaInformation> GenerateFieldValues(Media_Dynamic item, IEnumerable<DynamicField> fields, Faker faker, Dictionary<int, int> listItemsCounts)
        {
            var infoItems = new List<DynamicMediaInformation>();
            foreach (var field in fields)
            {
                string value;
                if (field.DynamicListTypeId.HasValue)
                {
                    listItemsCounts.TryGetValue(field.DynamicListTypeId.Value, out int maxCount);
                    value = faker.Random.Number(maxCount).ToString();
                }
                else
                {
                    value = GenerateFieldValue(field.DataType, faker);
                }

                infoItems.Add(new DynamicMediaInformation(
                                    item.Media_DynamicId,
                                    field.DynamicFieldId,
                                    value));
            }

            return infoItems;
        }

        private static string GenerateFieldValue(DataTypes dataType, Faker faker) => dataType switch
        {
            DataTypes.IntValue => faker.Random.Number(int.MaxValue).ToString(),
            DataTypes.StringValue => faker.Lorem.Text(),
            DataTypes.BoolValue => faker.Random.Bool().ToString(),
            DataTypes.DateTimeValue => faker.Date.Between(DateTime.MinValue, DateTime.MaxValue).ToString(),
            DataTypes.DecimalValue => faker.Random.Decimal(decimal.MaxValue).ToString(),
            _ => string.Empty
        };
    }
}
