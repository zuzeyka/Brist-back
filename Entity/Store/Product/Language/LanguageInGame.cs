<<<<<<< HEAD
﻿using Slush.Entity.Abstract;

namespace Slush.Data.Entity
{
    public class LanguageInGame : DBRecord
    {
        public String gameId { get; set; }
        public String languageId { get; set; }

        public LanguageInGame(String id,
                              String gameId,
                              String languageId,
                              DateTime createdAt)
=======
﻿

namespace Slush.Data.Entity
{
    public class LanguageInGame
    {
        public LanguageInGame()
        {
        }

        public LanguageInGame(Guid id, Guid gameId, Guid languageId, DateTime? createdAt)
>>>>>>> development_branch
        {
            this.id = id;
            this.gameId = gameId;
            this.languageId = languageId;
            this.createdAt = createdAt;
        }
<<<<<<< HEAD
=======



        public Guid id { get; set; }
        public Guid gameId { get; set; }
        public Guid languageId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
