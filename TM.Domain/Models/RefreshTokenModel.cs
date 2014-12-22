using System;
using AutoMapper;
using TM.Database.Models;

namespace TM.Domain.Models
{
    public class RefreshTokenModel
    {
        public string Id { get; set; }

        public string Subject { get; set; }

        public string ClientId { get; set; }

        public DateTime IssuedUtc { get; set; }

        public DateTime ExpiresUtc { get; set; }

        public string ProtectedTicket { get; set; }

        public static RefreshTokenModel Map(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                return new RefreshTokenModel();

            return Mapper.Map<RefreshToken, RefreshTokenModel>(refreshToken);
        }

        public static RefreshToken Map(RefreshTokenModel model)
        {
            if (model == null)
                return new RefreshToken();

            return Mapper.Map<RefreshTokenModel, RefreshToken>(model);
        }
    }
}
