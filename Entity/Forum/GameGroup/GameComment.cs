<<<<<<< HEAD
﻿using Slush.Entity.Abstract;

namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameComment : DBRecord
    {
        public String gamePostId { get; set; }
        public String content { get; set; }
        public Author Author { get; set; }

        public GameComment(String id,
                           String gamePostId,
                           String content,
                           DateTime createdAt)
=======
﻿

namespace Slush.Data.Entity.Community.GameGroup
{
    public class GameComment
    {
        public GameComment()
        {
        }

        public GameComment(Guid id, Guid gamePostId, String? content, Guid authorId, DateTime? createdAt)
>>>>>>> development_branch
        {
            this.id = id;
            this.gamePostId = gamePostId;
            this.content = content;
<<<<<<< HEAD
            this.createdAt = createdAt;
        }
=======
            this.authorId = authorId;
            this.createdAt = createdAt;
        }
        public Guid id { get; set; }
        public Guid gamePostId { get; set; }
        public String? content { get; set; }
        public Guid authorId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
