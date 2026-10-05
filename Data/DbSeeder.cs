using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity.Profile;
using Slush.Entity.Store.Product;
using Slush.Entity.Store.Product.Creators;
using Slush.Services.Hash;

namespace Slush.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(DataContext context, IHashPasswordService hashService)
        {
            if (await context.dbGamesInShops.AnyAsync())
            {
                return;
            }

            var now = DateTime.UtcNow;
            string Image(string seed, int w = 640, int h = 360) => $"https://picsum.photos/seed/{seed}/{w}/{h}.jpg";

            // GameNews/GameGuide/Screenshot.authorId references a real User, not a
            // Developer — a seed "staff" account owns all seeded editorial content.
            var editorialAuthor = new Slush.Data.Entity.Profile.User(
                Guid.NewGuid(),
                "Slush Team",
                hashService.Generate(Guid.NewGuid().ToString()),
                "team@slush.local",
                "Офіційна редакція Slush.",
                Image("author-slush-team", 150, 150),
                true,
                0,
                0,
                now);

            var developers = new[]
            {
                new Developer { id = Guid.NewGuid(), name = "CD PROJEKT RED", subscribersCount = 500000, description = "Польський розробник RPG.", avatar = Image("dev-cdpr", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Massive Entertainment", subscribersCount = 120000, description = "Студія Ubisoft.", avatar = Image("dev-massive", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "ConcernedApe", subscribersCount = 80000, description = "Соло-розробник Stardew Valley.", avatar = Image("dev-concernedape", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Larian Studios", subscribersCount = 300000, description = "Розробник Baldur's Gate 3.", avatar = Image("dev-larian", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Rockstar North", subscribersCount = 950000, description = "Студія Rockstar Games, автори GTA.", avatar = Image("dev-rockstarnorth", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Naughty Dog", subscribersCount = 610000, description = "Студія Sony, автори The Last of Us.", avatar = Image("dev-naughtydog", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "FromSoftware", subscribersCount = 430000, description = "Автори Elden Ring та Dark Souls.", avatar = Image("dev-fromsoftware", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Supergiant Games", subscribersCount = 95000, description = "Інді-студія, автори Hades.", avatar = Image("dev-supergiant", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Valve", subscribersCount = 780000, description = "Розробник Portal, CS2 та Dota 2.", avatar = Image("dev-valve", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Playground Games", subscribersCount = 210000, description = "Автори серії Forza Horizon.", avatar = Image("dev-playground", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "EA Vancouver", subscribersCount = 340000, description = "Студія EA Sports.", avatar = Image("dev-eavancouver", 150, 150), createdAt = now },
                new Developer { id = Guid.NewGuid(), name = "Team Cherry", subscribersCount = 150000, description = "Інді-студія, автори Hollow Knight.", avatar = Image("dev-teamcherry", 150, 150), createdAt = now },
            };

            var publishers = new[]
            {
                new Publisher { id = Guid.NewGuid(), name = "CD PROJEKT RED", subscribersCount = 500000, description = "Видавець власних ігор.", avatar = Image("pub-cdpr", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Ubisoft", subscribersCount = 900000, description = "Глобальний видавець ігор.", avatar = Image("pub-ubisoft", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Larian Studios", subscribersCount = 300000, description = "Видавець власних ігор.", avatar = Image("pub-larian", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Rockstar Games", subscribersCount = 1200000, description = "Видавець серії GTA та Red Dead Redemption.", avatar = Image("pub-rockstar", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Sony Interactive Entertainment", subscribersCount = 1100000, description = "Видавець ексклюзивів PlayStation.", avatar = Image("pub-sony", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Bandai Namco", subscribersCount = 540000, description = "Видавець Elden Ring, Tekken, Dark Souls.", avatar = Image("pub-bandainamco", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Valve", subscribersCount = 780000, description = "Видавець власних ігор та платформи Steam.", avatar = Image("pub-valve", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Xbox Game Studios", subscribersCount = 620000, description = "Видавець ексклюзивів Xbox.", avatar = Image("pub-xbox", 150, 150), createdAt = now },
                new Publisher { id = Guid.NewGuid(), name = "Electronic Arts", subscribersCount = 980000, description = "Один з найбільших видавців у світі.", avatar = Image("pub-ea", 150, 150), createdAt = now },
            };

            var genreNames = new[]
            {
                "Шутер", "RPG", "Відкритий світ", "Стратегія", "Інді", "Пригоди", "Симулятор",
                "Хоррор", "Гонки", "Спорт", "Платформер", "Паззл", "MOBA",
            };
            var platformNames = new[] { "PC", "PlayStation", "Xbox", "Switch" };
            var typeNames = new[] { "Одногравець", "Багатокористувацька", "Кооператив" };
            var featureNames = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" };

            var categories = genreNames
                .Select(name => new Categories { id = Guid.NewGuid(), name = name, description = "", kind = "genre", createdAt = now })
                .Concat(platformNames.Select(name => new Categories { id = Guid.NewGuid(), name = name, description = "", kind = "platform", createdAt = now }))
                .Concat(typeNames.Select(name => new Categories { id = Guid.NewGuid(), name = name, description = "", kind = "type", createdAt = now }))
                .Concat(featureNames.Select(name => new Categories { id = Guid.NewGuid(), name = name, description = "", kind = "feature", createdAt = now }))
                .ToArray();

            Guid DevId(string name) => developers.First(d => d.name == name).id;
            Guid PubId(string name) => publishers.First(p => p.name == name).id;
            Guid CatId(string name) => categories.First(c => c.name == name).id;

            var games = new[]
            {
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Cyberpunk 2077", price = 1099, discount = 0, previeImage = Image("cyberpunk-2077"), description = "Рольовий бойовик з відкритим світом у Найт-Сіті.", dateOfRelease = new DateTime(2020, 12, 10), developerId = DevId("CD PROJEKT RED"), publisherId = PubId("CD PROJEKT RED"), createdAt = now },
                    Categories = new[] { "RPG", "Відкритий світ", "Шутер" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox" },
                    Types = new[] { "Одногравець" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Відьмак 3: Дикий Гін", price = 729, discount = 50, discountFinish = now.AddDays(10), previeImage = Image("witcher-3"), description = "Епічна RPG про відьмака Ґеральта з Рівії.", dateOfRelease = new DateTime(2015, 5, 19), developerId = DevId("CD PROJEKT RED"), publisherId = PubId("CD PROJEKT RED"), createdAt = now },
                    Categories = new[] { "RPG", "Відкритий світ", "Пригоди" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox", "Switch" },
                    Types = new[] { "Одногравець" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Avatar: Frontiers of Pandora", price = 1519, discount = 40, discountFinish = now.AddDays(10), previeImage = Image("avatar-pandora"), description = "Пригодницький бойовик від першої особи на Пандорі.", dateOfRelease = new DateTime(2023, 12, 7), developerId = DevId("Massive Entertainment"), publisherId = PubId("Ubisoft"), createdAt = now },
                    Categories = new[] { "Шутер", "Відкритий світ", "Пригоди" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox" },
                    Types = new[] { "Одногравець" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Stardew Valley", price = 229, discount = 0, previeImage = Image("stardew-valley"), description = "Симулятор фермерського життя.", dateOfRelease = new DateTime(2016, 2, 26), developerId = DevId("ConcernedApe"), publisherId = PubId("Ubisoft"), createdAt = now },
                    Categories = new[] { "Симулятор", "Інді" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox", "Switch" },
                    Types = new[] { "Одногравець", "Кооператив" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Baldur's Gate 3", price = 899, discount = 0, previeImage = Image("baldurs-gate-3"), description = "Рольова гра на основі Dungeons & Dragons.", dateOfRelease = new DateTime(2023, 8, 3), developerId = DevId("Larian Studios"), publisherId = PubId("Larian Studios"), createdAt = now },
                    Categories = new[] { "RPG", "Пригоди", "Стратегія" },
                    Platforms = new[] { "PC", "PlayStation" },
                    Types = new[] { "Одногравець", "Кооператив" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Grand Theft Auto V", price = 799, discount = 60, discountFinish = now.AddDays(14), previeImage = Image("gta-5"), description = "Відкритий світ Лос-Сантоса очима трьох злочинців.", dateOfRelease = new DateTime(2015, 4, 14), developerId = DevId("Rockstar North"), publisherId = PubId("Rockstar Games"), createdAt = now },
                    Categories = new[] { "Відкритий світ", "Шутер", "Пригоди" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox" },
                    Types = new[] { "Одногравець", "Багатокористувацька" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "The Last of Us Part I", price = 1399, discount = 0, previeImage = Image("tlou-1"), description = "Історія Джоела та Еллі у постапокаліптичній Америці.", dateOfRelease = new DateTime(2022, 9, 2), developerId = DevId("Naughty Dog"), publisherId = PubId("Sony Interactive Entertainment"), createdAt = now },
                    Categories = new[] { "Пригоди", "Хоррор" },
                    Platforms = new[] { "PC", "PlayStation" },
                    Types = new[] { "Одногравець" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Elden Ring", price = 1199, discount = 20, discountFinish = now.AddDays(7), previeImage = Image("elden-ring"), description = "Фентезійний відкритий світ від FromSoftware та Джорджа Р. Р. Мартіна.", dateOfRelease = new DateTime(2022, 2, 25), developerId = DevId("FromSoftware"), publisherId = PubId("Bandai Namco"), createdAt = now },
                    Categories = new[] { "RPG", "Відкритий світ" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox" },
                    Types = new[] { "Одногравець", "Кооператив" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Hades", price = 399, discount = 0, previeImage = Image("hades"), description = "Рогалик про втечу з підземного царства Аїда.", dateOfRelease = new DateTime(2020, 9, 17), developerId = DevId("Supergiant Games"), publisherId = PubId("Valve"), createdAt = now },
                    Categories = new[] { "Інді", "RPG" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox", "Switch" },
                    Types = new[] { "Одногравець" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Portal 2", price = 349, discount = 0, previeImage = Image("portal-2"), description = "Кооперативна головоломка від Valve з портальною гарматою.", dateOfRelease = new DateTime(2011, 4, 19), developerId = DevId("Valve"), publisherId = PubId("Valve"), createdAt = now },
                    Categories = new[] { "Паззл", "Пригоди" },
                    Platforms = new[] { "PC" },
                    Types = new[] { "Одногравець", "Кооператив" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Counter-Strike 2", price = 0, discount = 0, previeImage = Image("cs2"), description = "Командний тактичний шутер від першої особи.", dateOfRelease = new DateTime(2023, 9, 27), developerId = DevId("Valve"), publisherId = PubId("Valve"), createdAt = now },
                    Categories = new[] { "Шутер" },
                    Platforms = new[] { "PC" },
                    Types = new[] { "Багатокористувацька" },
                    Features = new[] { "Досягнення", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Forza Horizon 5", price = 999, discount = 30, discountFinish = now.AddDays(5), previeImage = Image("forza-horizon-5"), description = "Гоночна пісочниця у відкритому світі Мексики.", dateOfRelease = new DateTime(2021, 11, 9), developerId = DevId("Playground Games"), publisherId = PubId("Xbox Game Studios"), createdAt = now },
                    Categories = new[] { "Гонки", "Відкритий світ" },
                    Platforms = new[] { "PC", "Xbox" },
                    Types = new[] { "Одногравець", "Багатокористувацька" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "EA Sports FC 24", price = 1299, discount = 25, discountFinish = now.AddDays(12), previeImage = Image("eafc-24"), description = "Футбольний симулятор з ліцензованими клубами та лігами.", dateOfRelease = new DateTime(2023, 9, 29), developerId = DevId("EA Vancouver"), publisherId = PubId("Electronic Arts"), createdAt = now },
                    Categories = new[] { "Спорт", "Симулятор" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox", "Switch" },
                    Types = new[] { "Багатокористувацька" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Hollow Knight", price = 269, discount = 0, previeImage = Image("hollow-knight"), description = "Атмосферний метроідванія-платформер у підземному королівстві комах.", dateOfRelease = new DateTime(2017, 2, 24), developerId = DevId("Team Cherry"), publisherId = PubId("Valve"), createdAt = now },
                    Categories = new[] { "Інді", "Платформер", "Пригоди" },
                    Platforms = new[] { "PC", "PlayStation", "Xbox", "Switch" },
                    Types = new[] { "Одногравець" },
                    Features = new[] { "Досягнення", "Підтримка контролера", "Хмарні збереження" },
                },
                new {
                    Game = new GameInShop { id = Guid.NewGuid(), name = "Dota 2", price = 0, discount = 0, previeImage = Image("dota-2"), description = "Командна MOBA з понад сотнею унікальних героїв.", dateOfRelease = new DateTime(2013, 7, 9), developerId = DevId("Valve"), publisherId = PubId("Valve"), createdAt = now },
                    Categories = new[] { "MOBA", "Стратегія" },
                    Platforms = new[] { "PC" },
                    Types = new[] { "Багатокористувацька" },
                    Features = new[] { "Досягнення", "Хмарні збереження" },
                },
            };

            Guid GameId(string name) => games.First(g => g.Game.name == name).Game.id;

            var categoryLinks = games.SelectMany(g =>
                g.Categories.Concat(g.Platforms).Concat(g.Types).Concat(g.Features).Select(c => new CategoryForGame
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
            }).ToArray();

            Guid GameGroupId(Guid gameId) => gameGroups.First(gg => gg.gameId == gameId).id;

            // DLC
            var dlcs = new[]
            {
                new DLCInShop { id = Guid.NewGuid(), gameId = GameId("Cyberpunk 2077"), name = "Cyberpunk 2077: Phantom Liberty", price = 799, discount = 0, previeImage = Image("dlc-phantom-liberty"), description = "Шпигунський трилер-доповнення із новим районом Догтаун.", dateOfRelease = new DateTime(2023, 9, 26), developerId = DevId("CD PROJEKT RED"), publisherId = PubId("CD PROJEKT RED"), createdAt = now },
                new DLCInShop { id = Guid.NewGuid(), gameId = GameId("Відьмак 3: Дикий Гін"), name = "Відьмак 3: Кров і вино", price = 399, discount = 0, previeImage = Image("dlc-blood-and-wine"), description = "Велике доповнення з новим регіоном Туссент.", dateOfRelease = new DateTime(2016, 5, 31), developerId = DevId("CD PROJEKT RED"), publisherId = PubId("CD PROJEKT RED"), createdAt = now },
                new DLCInShop { id = Guid.NewGuid(), gameId = GameId("Відьмак 3: Дикий Гін"), name = "Відьмак 3: Кам'яні серця", price = 299, discount = 0, previeImage = Image("dlc-hearts-of-stone"), description = "Доповнення про угоду з таємничим Пана Спритником.", dateOfRelease = new DateTime(2015, 10, 13), developerId = DevId("CD PROJEKT RED"), publisherId = PubId("CD PROJEKT RED"), createdAt = now },
                new DLCInShop { id = Guid.NewGuid(), gameId = GameId("Elden Ring"), name = "Elden Ring: Shadow of the Erdtree", price = 699, discount = 0, previeImage = Image("dlc-shadow-erdtree"), description = "Масштабне доповнення із землями Тіньового Царства.", dateOfRelease = new DateTime(2024, 6, 21), developerId = DevId("FromSoftware"), publisherId = PubId("Bandai Namco"), createdAt = now },
                new DLCInShop { id = Guid.NewGuid(), gameId = GameId("Forza Horizon 5"), name = "Forza Horizon 5: Rally Adventure", price = 499, discount = 0, previeImage = Image("dlc-rally-adventure"), description = "Нова раллі-локація з екстремальними трасами.", dateOfRelease = new DateTime(2023, 3, 29), developerId = DevId("Playground Games"), publisherId = PubId("Xbox Game Studios"), createdAt = now },
            };

            Guid DlcId(string name) => dlcs.First(d => d.name == name).id;

            // Bundles
            var bundles = new[]
            {
                new GameBundle { id = Guid.NewGuid(), name = "CD PROJEKT RED: Повна колекція", description = "Cyberpunk 2077 та Відьмак 3 разом з усіма доповненнями за вигідною ціною.", price = 1599, discount = 15, discountFinish = now.AddDays(20), createdAt = now },
                new GameBundle { id = Guid.NewGuid(), name = "Elden Ring: Deluxe Edition", description = "Elden Ring разом з доповненням Shadow of the Erdtree.", price = 1699, discount = 10, discountFinish = now.AddDays(20), createdAt = now },
            };

            Guid BundleId(string name) => bundles.First(b => b.name == name).id;

            var bundleCollections = new[]
            {
                new GameBundleCollection { id = Guid.NewGuid(), gameId = GameId("Cyberpunk 2077"), dlcId = DlcId("Cyberpunk 2077: Phantom Liberty"), bundleId = BundleId("CD PROJEKT RED: Повна колекція"), createdAt = now },
                new GameBundleCollection { id = Guid.NewGuid(), gameId = GameId("Відьмак 3: Дикий Гін"), dlcId = DlcId("Відьмак 3: Кров і вино"), bundleId = BundleId("CD PROJEKT RED: Повна колекція"), createdAt = now },
                new GameBundleCollection { id = Guid.NewGuid(), gameId = GameId("Відьмак 3: Дикий Гін"), dlcId = DlcId("Відьмак 3: Кам'яні серця"), bundleId = BundleId("CD PROJEKT RED: Повна колекція"), createdAt = now },
                new GameBundleCollection { id = Guid.NewGuid(), gameId = GameId("Elden Ring"), dlcId = DlcId("Elden Ring: Shadow of the Erdtree"), bundleId = BundleId("Elden Ring: Deluxe Edition"), createdAt = now },
            };

            // Events — time-bound, so unlike genre/platform/type/feature they can't
            // reuse the Categories/CategoryForGame shape; a real start/end window.
            var events = new[]
            {
                new GameEvent { id = Guid.NewGuid(), name = "Осінній розпродаж", description = "Знижки на обрані хіти.", startAt = now.AddDays(-3), endAt = now.AddDays(7), createdAt = now },
                new GameEvent { id = Guid.NewGuid(), name = "Новинки тижня", description = "Нещодавні релізи, які варто спробувати.", startAt = now.AddDays(-14), endAt = now.AddDays(14), createdAt = now },
                new GameEvent { id = Guid.NewGuid(), name = "Безкоштовні вихідні", description = "Грайте безкоштовно протягом обмеженого часу.", startAt = now.AddDays(2), endAt = now.AddDays(4), createdAt = now },
            };

            Guid EventId(string name) => events.First(e => e.name == name).id;

            var saleGames = new[] { "Відьмак 3: Дикий Гін", "Avatar: Frontiers of Pandora", "Grand Theft Auto V", "Elden Ring", "Forza Horizon 5", "EA Sports FC 24" };
            var newReleaseGames = new[] { "Avatar: Frontiers of Pandora", "EA Sports FC 24", "Counter-Strike 2", "Baldur's Gate 3" };
            var freeWeekendGames = new[] { "Hades", "Portal 2" };

            var eventLinks =
                saleGames.Select(name => new GameEventForGame { id = Guid.NewGuid(), gameId = GameId(name), eventId = EventId("Осінній розпродаж"), createdAt = now })
                .Concat(newReleaseGames.Select(name => new GameEventForGame { id = Guid.NewGuid(), gameId = GameId(name), eventId = EventId("Новинки тижня"), createdAt = now }))
                .Concat(freeWeekendGames.Select(name => new GameEventForGame { id = Guid.NewGuid(), gameId = GameId(name), eventId = EventId("Безкоштовні вихідні"), createdAt = now }));

            // News, guides and screenshots — rotated templates per game so the
            // catalog pages have real-looking content without hand-authoring
            // hundreds of near-duplicate posts.
            var newsTemplates = new (string Title, string Description, string Content)[]
            {
                ("Великий патч для {0} вже доступний", "Розробники випустили оновлення з виправленням багів та покращенням балансу.", "Команда розробників {0} опублікувала черговий патч. Виправлено низку помилок, покращено продуктивність та збалансовано кілька ігрових механік за фідбеком спільноти."),
                ("{0}: інтерв'ю з розробниками про майбутнє гри", "Творча команда поділилась планами подальшої підтримки проєкту.", "У новому інтерв'ю розробники {0} розповіли про плани на наступний рік: нові локації, покращення UI та підтримку спільноти через регулярні оновлення."),
                ("Рекордна кількість гравців у {0}", "Гра встановила новий пік одночасних гравців за останній тиждень.", "{0} продовжує залучати нову аудиторію. За останній тиждень кількість одночасних гравців сягнула рекордного рівня, а спільнота активно обговорює останні зміни."),
                ("Розпродаж {0} триває обмежений час", "Скористайтеся знижкою, поки пропозиція активна.", "У рамках сезонного розпродажу {0} доступна зі знижкою. Пропозиція діє обмежений час, тож не варто зволікати з покупкою."),
                ("Нова дорожня карта контенту для {0}", "Опубліковано план оновлень на найближчі місяці.", "Студія представила дорожню карту {0} з переліком майбутнього контенту: нові завдання, косметичні предмети та покращення ігрового балансу."),
            };

            var guideTemplates = new (string Title, string Description, string Content)[]
            {
                ("Гайд для початківців: {0}", "Все, що потрібно знати перед першим запуском.", "Цей гайд допоможе новим гравцям {0} швидко розібратися в основних механіках, уникнути типових помилок та ефективно провести перші години гри."),
                ("{0}: найкращі білди та стратегії", "Підбірка перевірених спільнотою тактик.", "У цьому гайді зібрані найефективніші білди та стратегії для {0}, перевірені спільнотою та оптимізовані під останнє оновлення гри."),
                ("Приховані секрети та колекційні предмети {0}", "Повний перелік локацій з бонусним контентом.", "Детальний путівник по всіх прихованих локаціях, колекційних предметах та easter eggs, які можна знайти в {0}."),
            };

            var screenshotCaptions = new[]
            {
                "Атмосферний краєвид з {0}",
                "Бойова сцена з {0}",
                "Дослідження світу {0}",
                "Кінематографічний момент з {0}",
                "Улюблена локація спільноти в {0}",
            };

            var gameNews = new List<GameNews>();
            var gameGuides = new List<GameGuide>();
            var screenshots = new List<Screenshot>();

            for (int i = 0; i < games.Length; i++)
            {
                var game = games[i].Game;
                var authorId = editorialAuthor.id;
                var groupId = GameGroupId(game.id);

                var news1 = newsTemplates[i % newsTemplates.Length];
                var news2 = newsTemplates[(i + 2) % newsTemplates.Length];
                gameNews.Add(new GameNews { id = Guid.NewGuid(), title = string.Format(news1.Title, game.name), description = string.Format(news1.Description, game.name), content = string.Format(news1.Content, game.name), contentUrl = Image($"news-{i}-a"), likesCount = 10 + i * 7, gameId = game.id, gameGroupId = groupId, authorId = authorId, createdAt = now.AddDays(-i) });
                gameNews.Add(new GameNews { id = Guid.NewGuid(), title = string.Format(news2.Title, game.name), description = string.Format(news2.Description, game.name), content = string.Format(news2.Content, game.name), contentUrl = Image($"news-{i}-b"), likesCount = 5 + i * 3, gameId = game.id, gameGroupId = groupId, authorId = authorId, createdAt = now.AddDays(-i - 1) });

                var guide = guideTemplates[i % guideTemplates.Length];
                gameGuides.Add(new GameGuide { id = Guid.NewGuid(), title = string.Format(guide.Title, game.name), description = string.Format(guide.Description, game.name), content = string.Format(guide.Content, game.name), contentUrl = Image($"guide-{i}"), likesCount = 15 + i * 5, gameId = game.id, gameGroupId = groupId, authorId = authorId, createdAt = now.AddDays(-i) });

                for (int s = 0; s < 3; s++)
                {
                    var caption = screenshotCaptions[(i + s) % screenshotCaptions.Length];
                    screenshots.Add(new Screenshot { id = Guid.NewGuid(), title = string.Format(caption, game.name), description = $"Скріншот гри {game.name}.", contentUrl = Image($"screenshot-{i}-{s}", 1280, 720), likesCount = 3 + s * 2 + i, gameId = game.id, authorId = authorId, createdAt = now.AddDays(-i) });
                }
            }

            await context.dbUsers.AddAsync(editorialAuthor);
            await context.dbDevelopers.AddRangeAsync(developers);
            await context.dbPublishers.AddRangeAsync(publishers);
            await context.dbCategories.AddRangeAsync(categories);
            await context.dbGamesInShops.AddRangeAsync(games.Select(g => g.Game));
            await context.dbCategoriesForGame.AddRangeAsync(categoryLinks);
            await context.dbMinimalSystemRequirements.AddRangeAsync(minRequirements);
            await context.dbMaximumSystemRequirements.AddRangeAsync(maxRequirements);
            await context.dbGameGroups.AddRangeAsync(gameGroups);
            await context.dbDLCsInShop.AddRangeAsync(dlcs);
            await context.dbGameBundles.AddRangeAsync(bundles);
            await context.dbGameBundleCollections.AddRangeAsync(bundleCollections);
            await context.dbGameEvents.AddRangeAsync(events);
            await context.dbGameEventsForGame.AddRangeAsync(eventLinks);
            await context.dbGameNews.AddRangeAsync(gameNews);
            await context.dbGameGuides.AddRangeAsync(gameGuides);
            await context.dbScreenshots.AddRangeAsync(screenshots);

            await context.SaveChangesAsync();
        }
    }
}
