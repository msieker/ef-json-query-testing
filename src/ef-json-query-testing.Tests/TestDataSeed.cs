

using ef_json_query_testing.Enums;
using ef_json_query_testing.Models;
using ef_json_query_testing.Seeders;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ef_json_query_testing.Tests
{
    static internal class TestDataSeed
    {
        public static void LoadAll(EfTestDbContext context)
        {
            using (context.Database.BeginTransaction())
            {
                if (!context.DynamicListTypes.Any())
                {
                    LoadListTypesAndItems(context);
                }

                // LoadListTypesAndItems loads 5 items into DynamicFields
                if (context.DynamicFields.Count() < 6)
                {
                    LoadFieldsAndValues(context);
                }

                if (!context.Media_Dynamic.Any())
                {
                    LoadMediaItemsAndInfo(context);
                }

                context.Database.CommitTransaction();
            }
        }

        private static void LoadListTypesAndItems(EfTestDbContext context)
        {
            //List Types
            var typeSides = new DynamicListType("Sides");
            var typeSizes = new DynamicListType("Sizes");
            var typeColors = new DynamicListType("Colors");
            var typeShape = new DynamicListType("Count");
            var typeShades = new DynamicListType("Shades");

            context.DynamicListTypes.Add(typeSides);
            context.DynamicListTypes.Add(typeSizes);
            context.DynamicListTypes.Add(typeColors);
            context.DynamicListTypes.Add(typeShape);
            context.DynamicListTypes.Add(typeShades);
            context.SaveChanges();


            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[DynamicFields] ON");
            //Fields for list types
            List<DynamicField> fields = new()
            {
                new DynamicField(typeSides.DisplayName)
                {
                    DynamicFieldId = 1,
                    IsQueryable = true,
                    IsRequired = true,
                    Description = "This thing has a few sides",
                    DataType = DataTypes.IntValue,
                    DynamicListType = typeSides
                },
                new DynamicField(typeSizes.DisplayName)
                {
                    DynamicFieldId = 2,
                    IsQueryable = false,
                    IsRequired = false,
                    Description = "There are many sizes here",
                    DataType = DataTypes.IntValue,
                    DynamicListType = typeSizes
                },
                new DynamicField(typeColors.DisplayName)
                {
                    DynamicFieldId = 3,
                    IsQueryable = false,
                    IsRequired = true,
                    Description = "Display your true colors",
                    DataType = DataTypes.IntValue,
                    DynamicListType = typeColors
                },
                new DynamicField(typeShape.DisplayName)
                {
                    DynamicFieldId = 4,
                    IsQueryable = true,
                    IsRequired = true,
                    Description = "Can't find them all!",
                    DataType = DataTypes.IntValue,
                    DynamicListType = typeShape
                },
                new DynamicField(typeShades.DisplayName)
                {
                    DynamicFieldId = 5,
                    IsQueryable = true,
                    IsRequired = false,
                    Description = "2 many shades 2 count on this thing",
                    DataType = DataTypes.IntValue,
                    DynamicListType = typeShades
                }
            };

            context.DynamicFields.AddRange(fields);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[DynamicFields] OFF");

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[DynamicListItems] ON");
            // List items
            List<DynamicListItem>? itemsList = new()
            {
                new DynamicListItem("Two", typeSides.DynamicListTypeId) { DynamicListItemId = 1 },
                new DynamicListItem("Four", typeSides.DynamicListTypeId) { DynamicListItemId = 2 },
                new DynamicListItem("Six", typeSides.DynamicListTypeId) { DynamicListItemId = 3 },
                new DynamicListItem("Ten", typeSides.DynamicListTypeId) { DynamicListItemId = 4 },
                new DynamicListItem("Twenty", typeSides.DynamicListTypeId) { DynamicListItemId = 5 },

                new DynamicListItem("Small", typeSizes.DynamicListTypeId) { DynamicListItemId = 6 },
                new DynamicListItem("Medium", typeSizes.DynamicListTypeId) { DynamicListItemId = 7 },
                new DynamicListItem("Large", typeSizes.DynamicListTypeId) { DynamicListItemId = 8 },

                new DynamicListItem("Red", typeColors.DynamicListTypeId) { DynamicListItemId = 9 },
                new DynamicListItem("Yellow", typeColors.DynamicListTypeId) { DynamicListItemId = 10 },
                new DynamicListItem("Blue", typeColors.DynamicListTypeId) { DynamicListItemId = 11 },
                new DynamicListItem("Green", typeColors.DynamicListTypeId) { DynamicListItemId = 12 },
                new DynamicListItem("Purple", typeColors.DynamicListTypeId) { DynamicListItemId = 13 },
                new DynamicListItem("Black", typeColors.DynamicListTypeId) { DynamicListItemId = 14 },
                new DynamicListItem("White", typeColors.DynamicListTypeId) { DynamicListItemId = 15 },

                new DynamicListItem("Circle", typeShape.DynamicListTypeId) { DynamicListItemId = 16 },
                new DynamicListItem("Square", typeShape.DynamicListTypeId) { DynamicListItemId = 17 },
                new DynamicListItem("Triangle", typeShape.DynamicListTypeId) { DynamicListItemId = 18 },
                new DynamicListItem("Rombus", typeShape.DynamicListTypeId) { DynamicListItemId = 19 },
                new DynamicListItem("Trapezoid", typeShape.DynamicListTypeId) { DynamicListItemId = 20 },

                new DynamicListItem("Light", typeShades.DynamicListTypeId) { DynamicListItemId = 21 },
                new DynamicListItem("Dark", typeShades.DynamicListTypeId) { DynamicListItemId = 22 },
                new DynamicListItem("Bright", typeShades.DynamicListTypeId) { DynamicListItemId = 23 },
                new DynamicListItem("Dim", typeShades.DynamicListTypeId) { DynamicListItemId = 24 },
                new DynamicListItem("None", typeShades.DynamicListTypeId) { DynamicListItemId = 25 },
            };

            context.DynamicListItems.AddRange(itemsList);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[DynamicListItems] OFF");
        }

        private static void LoadFieldsAndValues(EfTestDbContext context)
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[DynamicFields] ON");
            //LoadListTypesAndItems handles fields for list types
            List<DynamicField> fields = new()
            {
                new DynamicField("We Want Numbers!")
                {
                    DynamicFieldId = 6,
                    IsQueryable = true,
                    IsRequired = true,
                    Description = "Put some numbers with value here",
                    DataType = DataTypes.IntValue
                },
                new DynamicField("One Two Three Please")
                {
                    DynamicFieldId = 7,
                    IsQueryable = true,
                    IsRequired = false,
                    Description = "fish are green and blue",
                    DataType = DataTypes.IntValue
                },
                new DynamicField("Can't Fill This one.")
                {
                    DynamicFieldId = 8,
                    IsQueryable = false,
                    IsRequired = true,
                    Description = "Little did they know.",
                    DataType = DataTypes.IntValue
                },
                new DynamicField("Don't lEt them Take This!")
                {
                    DynamicFieldId = 9,
                    IsQueryable = false,
                    IsRequired = false,
                    Description = "How many times do i need to ask about this thing?",
                    DataType = DataTypes.IntValue
                },


                new DynamicField("I want your Words")
                {
                    DynamicFieldId = 10,
                    IsQueryable = true,
                    IsRequired = true,
                    Description = "We WILL BREAK YOU",
                    DataType = DataTypes.StringValue
                },
                new DynamicField("Cant stop, Wont stop")
                {
                    DynamicFieldId = 11,
                    IsQueryable = true,
                    IsRequired = false,
                    Description = "Sorry, not Sorry. lol",
                    DataType = DataTypes.StringValue
                },
                new DynamicField("Choo Choo")
                {
                    DynamicFieldId = 12,
                    IsQueryable = false,
                    IsRequired = true,
                    Description = "THERE ARE TRAINS HERE. HELP",
                    DataType = DataTypes.StringValue
                },
                new DynamicField("Do not pass Go")
                {
                    DynamicFieldId = 13,
                    IsQueryable = false,
                    IsRequired = false,
                    Description = "But i wanted that money :/",
                    DataType = DataTypes.StringValue
                },


                new DynamicField("End of the Line here")
                {
                    DynamicFieldId = 14,
                    IsQueryable = true,
                    IsRequired = true,
                    Description = "I WILL PASS",
                    DataType = DataTypes.BoolValue
                },
                new DynamicField("The Claw")
                {
                    DynamicFieldId = 15,
                    IsQueryable = true,
                    IsRequired = false,
                    Description = "You are the chosen one",
                    DataType = DataTypes.BoolValue
                },
                new DynamicField("End of the Line here")
                {
                    DynamicFieldId = 16,
                    IsQueryable = false,
                    IsRequired = true,
                    Description = "You cant make me.",
                    DataType = DataTypes.BoolValue
                },
                new DynamicField("The Claw 2")
                {
                    DynamicFieldId = 17,
                    IsQueryable = false,
                    IsRequired = false,
                    Description = "Let Me go!",
                    DataType = DataTypes.BoolValue
                }
            };

            context.DynamicFields.AddRange(fields);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[DynamicFields] OFF");
        }

        private static void LoadMediaItemsAndInfo(EfTestDbContext context)
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Media_Dynamic] ON");

            var media = CreateBogusData.FakerMedia_Dynamic.Generate();
            media.Media_DynamicId = 1;
            context.Media_Dynamic.Add(media);
            context.SaveChanges();

            List<DynamicMediaInformation> info = new()
            {
                new DynamicMediaInformation(media.Media_DynamicId, 1, "3"), // R, list
                new DynamicMediaInformation(media.Media_DynamicId, 3, "13"), // R, list
                new DynamicMediaInformation(media.Media_DynamicId, 4, "18"), // R, list
                new DynamicMediaInformation(media.Media_DynamicId, 6, "456"), // R, int
                new DynamicMediaInformation(media.Media_DynamicId, 8, "789"), // R, int
                new DynamicMediaInformation(media.Media_DynamicId, 10, "my time"), // R, str
                new DynamicMediaInformation(media.Media_DynamicId, 12, "long long time ago in a galaxy far far away"), // R. str
                new DynamicMediaInformation(media.Media_DynamicId, 14, "0"), // R, bool
                new DynamicMediaInformation(media.Media_DynamicId, 16, "1"), // R, bool

                new DynamicMediaInformation(media.Media_DynamicId, 2, ""), // O, list
                new DynamicMediaInformation(media.Media_DynamicId, 5, ""), // O, list
                new DynamicMediaInformation(media.Media_DynamicId, 7, ""), // O, int
                new DynamicMediaInformation(media.Media_DynamicId, 9, ""), // O, int
                new DynamicMediaInformation(media.Media_DynamicId, 11, ""), // O, str
                new DynamicMediaInformation(media.Media_DynamicId, 13, ""), // O, str
                new DynamicMediaInformation(media.Media_DynamicId, 15, ""), // O, bool
                new DynamicMediaInformation(media.Media_DynamicId, 17, ""), // O, bool
            };


            context.DynamicMediaInformation.AddRange(info);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Media_Dynamic] OFF");

            context.Media_Json.Add(media.GetMediaJsonCopy());
            context.SaveChanges();

        }
    }
}
