namespace Slush.Entity.Profile
{
    public class Friends
    {
<<<<<<< HEAD
        public String id { get; set; }
        public String userId { get; set; }
        public String friendId { get; set; }

        public Friends(String id,
                       String userId,
                       String friendId)
=======
        public Friends()
        {
        }

        public Friends(Guid id, Guid userId, Guid friendId, DateTime? createdAt)
>>>>>>> development_branch
        {
            this.id = id;
            this.userId = userId;
            this.friendId = friendId;
<<<<<<< HEAD
        }
=======
            this.createdAt = createdAt;
        }

        public Guid id { get; set; }
        public Guid userId { get; set; }
        public Guid friendId { get; set; }

        public DateTime? createdAt { get; set; }
        public DateTime? deleteAt { get; set; }
>>>>>>> development_branch
    }
}
