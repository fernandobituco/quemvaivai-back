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

        public async Task<int?> GetIdByEmail(string email)
        {
            var sql = "select id as Id FROM {table} WHERE email = @Email and deleted = false";
            var userId = await Get<int>(sql, new { Email = email });

            return userId;
        }

        public async Task<int?> DeleteCascadeById(int id)
        {
            var sql = @"
                BEGIN;

                -- 1. Tokens do usuário
                DELETE FROM tb_email_confirmation_tokens
                WHERE user_id = @Id;

                DELETE FROM tb_password_reset_token
                WHERE user_id = @Id;

                DELETE FROM tb_refresh_tokens
                WHERE user_id = @Id;

                -- 2. Votos realizados pelo usuário
                DELETE FROM tb_votes
                WHERE user_id = @Id;

                -- 3. Comentários realizados pelo usuário
                DELETE FROM tb_comments
                WHERE user_id = @Id;

                -- 4. Relação usuário/evento
                DELETE FROM tb_user_events
                WHERE user_id = @Id;

                -- 5. Tarefas atribuídas ao usuário
                DELETE FROM tb_task_items
                WHERE assigned_user_id  = @Id;

                -- 6. Relação usuário/grupo
                DELETE FROM tb_group_users
                WHERE user_id = @Id;

                -- 7. Finalmente, usuário
                DELETE FROM tb_users
                WHERE id = @Id;

                COMMIT;
            ";
            var userId = await Get<int>(sql, new { Id = id });

            return userId;
        }
    }
}
