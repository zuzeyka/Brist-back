using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Entity.Store.Product;
using Slush.Entity.Store.Product.Creators;

namespace Slush.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(DataContext context)
        {
            if (await context.dbGamesInShops.AnyAsync())
            {
                return;
            }

            var now = DateTime.UtcNow;
            string Image(string seed, int w = 640, int h = 360) => $"https://picsum.photos/seed/{seed}/{w}/{h}.jpg";

            var developers = new[]
            {
                new Developer { id = Guid.NewGuid(), name = "CD PROJEKT RED", subscribersCount = 500000, description = "Польський розробник RPG.", avatar = Image("dev-cdpr", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Massive Entertainment", subscribersCount = 120000, description = "Студія Ubisoft.", avatar = Image("dev-massive", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "ConcernedApe", subscribersCount = 80000, description = "Соло-розробник Stardew Valley.", avatar = Image("dev-concernedape", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Larian Studios", subscribersCount = 300000, description = "Розробник Baldur's Gate 3.", avatar = Image("dev-larian", 150, 150), createdAt = now },
            };

            var publishers = new[]
            {
                new Publisher { id = Guid.NewGuid(), name = "CD PROJEKT RED", subscribersCount = 500000, description = "Видавець власних ігор.", avatar = Image("pub-cdpr", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Ubisoft", subscribersCount = 900000, description = "Глобальний видавець ігор.", avatar = Image("pub-ubisoft", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Larian Studios", subscribersCount = 300000, description = "Видавець власних ігор.", avatar = Image("pub-larian", 150, 150), createdAt = now },
            };

            var categoryNames = new[] { "Шутер", "RPG", "Відкритий світ", "Стратегія", "Інді", "Пригоди", "Симулятор" };
            var categories = categoryNames
                .Select(name => new Categories { id = Guid.NewGuid(), name = name, description = "", createdAt = now })
                .ToArray();

            Guid DevId(string name) => developers.First(d => d.name == name).id;
            Guid PubId(string name) => publishers.First(p => p.name == name).id;
            Guid CatId(string name) => categories.First(c => c.name == name).id;

            var games = new[]
            {
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Cyberpunk 2077", price = 1099, discount = 0, previeImage = Image("cyberpunk-2077"), description = "Рольовий бойовик з відкритим світом у Найт-Сіті.", dateOfRelease = new DateTime(2020, 12, 10), developerId = DevId("CD PROJEKT RED"), publisherId = PubId("CD PROJEKT RED"), createdAt = now },
                    Categories = new[] { "RPG", "Відкритий світ", "Шутер" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Відьмак 3: Дикий Гін", price = 729, discount = 50, discountFinish = now.AddDays(10), previeImage = Image("witcher-3"), description = "Епічна RPG про відьмака Ґеральта з Рівії.", dateOfRelease = new DateTime(2015, 5, 19), developerId = DevId("CD PROJEKT RED"), publisherId = PubId("CD PROJEKT RED"), createdAt = now },
                    Categories = new[] { "RPG", "Відкритий світ", "Пригоди" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Avatar: Frontiers of Pandora", price = 1519, discount = 40, discountFinish = now.AddDays(10), previeImage = Image("avatar-pandora"), description = "Пригодницький бойовик від першої особи на Пандорі.", dateOfRelease = new DateTime(2023, 12, 7), developerId = DevId("Massive Entertainment"), publisherId = PubId("Ubisoft"), createdAt = now },
                    Categories = new[] { "Шутер", "Відкритий світ", "Пригоди" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Stardew Valley", price = 229, discount = 0, previeImage = Image("stardew-valley"), description = "Симулятор фермерського життя.", dateOfRelease = new DateTime(2016, 2, 26), developerId = DevId("ConcernedApe"), publisherId = PubId("Ubisoft"), createdAt = now },
                    Categories = new[] { "Симулятор", "Інді" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Baldur's Gate 3", price = 899, discount = 0, previeImage = Image("baldurs-gate-3"), description = "Рольова гра на основі Dungeons & Dragons.", dateOfRelease = new DateTime(2023, 8, 3), developerId = DevId("Larian Studios"), publisherId = PubId("Larian Studios"), createdAt = now },
                    Categories = new[] { "RPG", "Пригоди", "Стратегія" },
                },
            };

            var categoryLinks = games.SelectMany(g => g.Categories.Select(c => new CategoryForGame
            {
                id = Guid.NewGuid(),
                gameId = g.Game.id,
                categoryId = CatId(c),
                createdAt = now,
            }));

            var minRequirements = games.Select(g => new MinimalSystemRequirement
            {
                id = Guid.NewGuid(),
                gameId = g.Game.id,
                OS = "Windows 10 64-bit",
                processor = "Intel Core i5-3570K / AMD FX-8310",
                RAM = "8 GB",
                video = "Nvidia GTX 780 / AMD RX 470",
                freeDiskSpace = "70 GB",
                createdAt = now,
            });

            var maxRequirements = games.Select(g => new MaximumSystemRequirement
            {
                id = Guid.NewGuid(),
                gameId = g.Game.id,
                OS = "Windows 11 64-bit",
                processor = "Intel Core i7-12700 / AMD Ryzen 7 7800X3D",
                RAM = "16 GB",
                video = "Nvidia RTX 4070 / AMD RX 7800 XT",
                freeDiskSpace = "70 GB SSD",
                createdAt = now,
            });

            var gameGroups = games.Select(g => new GameGroup
            {
                id = Guid.NewGuid(),
                gameId = g.Game.id,
                createdAt = now,
            });

            await context.dbDevelopers.AddRangeAsync(developers);
            await context.dbPublishers.AddRangeAsync(publishers);
            await context.dbCategories.AddRangeAsync(categories);
            await context.dbGamesInShops.AddRangeAsync(games.Select(g => g.Game));
            await context.dbCategoriesForGame.AddRangeAsync(categoryLinks);
            await context.dbMinimalSystemRequirements.AddRangeAsync(minRequirements);
            await context.dbMaximumSystemRequirements.AddRangeAsync(maxRequirements);
            await context.dbGameGroups.AddRangeAsync(gameGroups);

            await context.SaveChangesAsync();
        }
    }
}
