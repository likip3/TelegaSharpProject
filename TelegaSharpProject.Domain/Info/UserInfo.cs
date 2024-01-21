using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Info
{
    public class UserInfo : IUserInfo
    {
        public UserInfo(long id, string? userName, DateTime registeredAt, int points)
        {
            Id = id;
            UserName = userName;
            RegisteredAt = registeredAt;
            Points = points;
        }

        public UserInfo(User user)
        {
            Id = user.Id;
            RegisteredAt = user.RegisteredAt;
            UserName = user.UserName;
            Points = user.Points;
        }

        public long Id { get; set; }

        public string? UserName { get; set; }

        public DateTime RegisteredAt { get; set; }

        public int Points { get; set; }
    }
}
