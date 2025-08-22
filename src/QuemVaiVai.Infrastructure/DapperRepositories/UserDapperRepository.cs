using Dapper;
using Microsoft.EntityFrameworkCore.Query;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;
using QuemVaiVai.Application.DTOs;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class UserDapperRepository : DapperRepository<User>, IUserDapperRepository
    {
        public UserDapperRepository(
            IDbConnection connection,
            DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            var sql = "SELECT EXISTS ( SELECT 1 FROM {table} WHERE email = @Email and deleted = false);";
            var exists = await Get<bool>(sql, new { Email = email });

            return exists;
        }

        public async Task<bool> ExistsByEmailDiferentId(string email, int id)
        {
            var sql = "SELECT EXISTS ( SELECT 1 FROM {table} WHERE email = @Email AND id <> @Id AND deleted = false);";
            var exists = await Get<bool>(sql, new { Email = email, Id = id });

            return exists;
        }

        public async Task<User?> GetByEmail(string email)
        {
            var sql = GetBaseEntityValues + ", name as Name, email as Email, confirmed as Confirmed FROM {table} WHERE email = @Email and deleted = false";
            var user = await Get(sql, new { Email = email });

            return user;
        }

        public async Task<User?> GetSensitiveByEmail(string email)
        {
            var sql = "select id as Id, name as Name, email as Email, confirmed as Confirmed, password_hash as PasswordHash FROM {table} WHERE email = @Email and deleted = false";
            var user = await Get(sql, new { Email = email });

            return user;
        }

        public async Task<User?> GetById(int id)
        {
            var sql = GetBaseEntityValues + ", name as Name, email as Email, confirmed as Confirmed FROM {table} WHERE id = @Id and deleted = false";
            var user = await Get(sql, new { Id = id });

            return user;
        }

        public async Task<User?> GetCompleteForUpdateById(int id)
        {
            var sql = GetBaseEntityValues + ", name as Name, email as Email, confirmed as Confirmed, password_hash as PasswordHash FROM {table} WHERE id = @Id and deleted = false";
            var user = await Get(sql, new { Id = id });

            return user;
        }

        public async Task<IEnumerable<UserMemberDTO>> GetAllByGroupId(int groupId)
        {
            var sql = "select u.id as Id, u.name as Name, gu.role as Role FROM {table} u inner join group_users gu on u.id = gu.user_id and gu.deleted = false where gu.group_id = @GroupId and u.deleted = false";
            var users = await GetAll<UserMemberDTO>(sql, new { GroupId = groupId });

            return users;
        }
    }
}
