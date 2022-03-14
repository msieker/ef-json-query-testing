

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

            var media = CreateBogusData.FakerMedia_Dynamic.Generate(10);
            for (int i = 0; i < media.Count(); i++)
            {
                media[i].Media_DynamicId = i + 1;
            }

            context.Media_Dynamic.AddRange(media);
            context.SaveChanges();

            List<DynamicMediaInformation> info = new()
            {
                new DynamicMediaInformation(1, 1, "1"), // R, list (1-5)
                new DynamicMediaInformation(1, 3, "9"), // R, list (9-15)
                new DynamicMediaInformation(1, 4, "16"), // R, list (16-20)
                new DynamicMediaInformation(1, 6, "2"), // R, int
                new DynamicMediaInformation(1, 8, "111"), // R, int
                new DynamicMediaInformation(1, 10, "my time"), // R, str
                new DynamicMediaInformation(1, 12, "I swear by my pretty floral bonnet, I will end you."), // R. str
                new DynamicMediaInformation(1, 14, "1"), // R, bool
                new DynamicMediaInformation(1, 16, "1"), // R, bool

                //new DynamicMediaInformation(1, 2, ""), // O, list (6-8)
                //new DynamicMediaInformation(1, 5, ""), // O, list (21-25)
                //new DynamicMediaInformation(1, 7, ""), // O, int
                //new DynamicMediaInformation(1, 9, ""), // O, int
                //new DynamicMediaInformation(1, 11, ""), // O, str
                //new DynamicMediaInformation(1, 13, ""), // O, str
                //new DynamicMediaInformation(1, 15, ""), // O, bool
                //new DynamicMediaInformation(1, 17, ""), // O, bool


                new DynamicMediaInformation(2, 1, "2"), // R, list (1-5)
                new DynamicMediaInformation(2, 3, "10"), // R, list (9-15)
                new DynamicMediaInformation(2, 4, "17"), // R, list (16-20)
                new DynamicMediaInformation(2, 6, "222"), // R, int
                new DynamicMediaInformation(2, 8, "555"), // R, int
                new DynamicMediaInformation(2, 10, "my beats"), // R, str
                new DynamicMediaInformation(2, 12, "D'ya know what the chain of command is? It’s the chain I go get and beat you with ’til you understand who’s in ruttin’ command here."), // R. str
                new DynamicMediaInformation(2, 14, "1"), // R, bool
                new DynamicMediaInformation(2, 16, "0"), // R, bool

                //new DynamicMediaInformation(2, 2, ""), // O, list (6-8)
                //new DynamicMediaInformation(2, 5, ""), // O, list (21-25)
                //new DynamicMediaInformation(2, 7, ""), // O, int
                //new DynamicMediaInformation(2, 9, ""), // O, int
                new DynamicMediaInformation(2, 11, "this time it is mine"), // O, str
                //new DynamicMediaInformation(2, 13, ""), // O, str
                //new DynamicMediaInformation(2, 15, ""), // O, bool
                //new DynamicMediaInformation(2, 17, ""), // O, bool


                new DynamicMediaInformation(3, 1, "3"), // R, list (1-5)
                new DynamicMediaInformation(3, 3, "11"), // R, list (9-15)
                new DynamicMediaInformation(3, 4, "18"), // R, list (16-20)
                new DynamicMediaInformation(3, 6, "123"), // R, int
                new DynamicMediaInformation(3, 8, "222"), // R, int
                new DynamicMediaInformation(3, 10, "bears eat beats"), // R, str
                new DynamicMediaInformation(3, 12, "Curse Your Sudden But Inevitable Betrayal!"), // R. str
                new DynamicMediaInformation(3, 14, "0"), // R, bool
                new DynamicMediaInformation(3, 16, "1"), // R, bool

                new DynamicMediaInformation(3, 2, "6"), // O, list (6-8)
                //new DynamicMediaInformation(3, 5, ""), // O, list (21-25)
                new DynamicMediaInformation(3, 7, "333"), // O, int
                //new DynamicMediaInformation(3, 9, ""), // O, int
                new DynamicMediaInformation(3, 11, "it's dangerous to go alone, you can't go without this"), // O, str
                //new DynamicMediaInformation(3, 13, ""), // O, str
                //new DynamicMediaInformation(3, 15, ""), // O, bool
                //new DynamicMediaInformation(3, 17, ""), // O, bool


                new DynamicMediaInformation(4, 1, "4"), // R, list (1-5)
                new DynamicMediaInformation(4, 3, "12"), // R, list (9-15)
                new DynamicMediaInformation(4, 4, "19"), // R, list (16-20)
                new DynamicMediaInformation(4, 6, "444"), // R, int
                new DynamicMediaInformation(4, 8, "888"), // R, int
                new DynamicMediaInformation(4, 10, "Battlestar Galactica"), // R, str
                new DynamicMediaInformation(4, 12, "Ten percent of nothin' is... Let me do the math here... Nothin', and then nothin', Carry the nothing..."), // R. str
                new DynamicMediaInformation(4, 14, "0"), // R, bool
                new DynamicMediaInformation(4, 16, "0"), // R, bool

                new DynamicMediaInformation(4, 2, "6"), // O, list (6-8)
                new DynamicMediaInformation(4, 5, "21"), // O, list (21-25)
                new DynamicMediaInformation(4, 7, "444"), // O, int
                //new DynamicMediaInformation(4, 9, ""), // O, int
                new DynamicMediaInformation(4, 11, "my time to shine in this spotlight"), // O, str
                //new DynamicMediaInformation(4, 13, ""), // O, str
                //new DynamicMediaInformation(4, 15, ""), // O, bool
                //new DynamicMediaInformation(4, 17, ""), // O, bool


                new DynamicMediaInformation(5, 1, "5"), // R, list (1-5)
                new DynamicMediaInformation(5, 3, "13"), // R, list (9-15)
                new DynamicMediaInformation(5, 4, "20"), // R, list (16-20)
                new DynamicMediaInformation(5, 6, "999"), // R, int
                new DynamicMediaInformation(5, 8, "333"), // R, int
                new DynamicMediaInformation(5, 10, "pew pew"), // R, str
                new DynamicMediaInformation(5, 12, "I am a leaf on the wind; watch how I soar"), // R. str
                new DynamicMediaInformation(5, 14, "0"), // R, bool
                new DynamicMediaInformation(5, 16, "1"), // R, bool

                new DynamicMediaInformation(5, 2, "7"), // O, list (6-8)
                new DynamicMediaInformation(5, 5, "21"), // O, list (21-25)
                new DynamicMediaInformation(5, 7, "444"), // O, int
                new DynamicMediaInformation(5, 9, "50"), // O, int
                new DynamicMediaInformation(5, 11, "are you human or are you dancer?"), // O, str
                new DynamicMediaInformation(5, 13, "nothing is true, everything is permitted"), // O, str
                new DynamicMediaInformation(5, 15, "0"), // O, bool
                new DynamicMediaInformation(5, 17, "1"), // O, bool
            };


            context.DynamicMediaInformation.AddRange(info);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Media_Dynamic] OFF");

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Media_Json] ON");
            foreach (var item in media)
            {
                context.Media_Json.Add(item.GetMediaJsonCopy(true));
            }

            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Media_Json] OFF");
        }
    }
}
