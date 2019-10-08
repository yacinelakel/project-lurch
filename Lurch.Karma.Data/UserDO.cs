using Lurch.Karma.Core;

namespace Lurch.Karma.Data
{
    public class UserDo
    {
        public int Id { get; set; }

        public int KarmaAmount { get; set; }

        public static User ToModel(UserDo obj)
        {
            return new User(obj.Id, obj.KarmaAmount);
        }

        public static UserDo ToDo(User user)
        {
            return new UserDo
            {
                Id = user.Id,
                KarmaAmount = user.Karma.Amount
            };
        }
    }
}
