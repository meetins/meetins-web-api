using System;

namespace Meetins.Models.User.Output
{
    public class RefreshTokenOutput
    {
        public Guid TokenId { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
    }
}
