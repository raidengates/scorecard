using Microsoft.Extensions.Options;
using Scorecard.Applicatioin.Command;
using Scorecard.Applicatioin.Queries;
using Scorecard.Applicatioin.Results;
using Scorecard.Business.Utilitys;
using Scorecard.Core.Contracts.Business;
using Scorecard.Core.Contracts.Config;
using Scorecard.Core.Utilitys;
using Scorecard.Data.Interfaces;
using Scorecard.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Scorecard.Business
{
    internal class UserBusiness : IUserBusiness
    {
        IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly IOptionsMonitor<DefaultServerConfig> _serverOptionsMonitor;

        public UserBusiness(IUnitOfWork uow, IUserRepository userRepository, IOptionsMonitor<DefaultServerConfig> optionsMonitor)
        {
            _userRepository = userRepository;
            _uow = uow;
            _serverOptionsMonitor = optionsMonitor;
        }
        public async Task Save(SignupCommand command)
        {
            Expression<Func<User, bool>> predicate = g => g.Username == command.Username;
            var models = await _userRepository.Predicates(predicate);
            if (models.Any())
            {
                ExceptionHelper.ThrowInvalidValidationException(nameof(command.Username), "Tài khoản đã tồn tại.");
            }
            BC.HashPassword(command.Password);
            var user = command.Username.CreateUser(command.Password, command.Email);
            _userRepository.Add(user);
            await _uow.Commit();
        }

        public async Task<ServiceLoginResult> Login(ServiceLoginQuery query)
        {
            Expression<Func<User, bool>> predicate = g => g.Username == query.Username;
            var models = await _userRepository.Predicates(predicate);
            var throwException = () => ExceptionHelper.ThrowAuthenticationException($"{nameof(query.Username)} or {nameof(query.Password)}", "Tài khoản hoặc mật khẩu không đúng.");
            if (!models.Any())
            {
                throwException();
            }
            var userInfo = models.FirstOrDefault();
            if (!BC.Verify(query.Password, userInfo.Password))
            {
                throwException();
            }
            var token = _serverOptionsMonitor.CurrentValue.Api.JWT.IssuerSigningKey
                .generateJwtToken(userInfo.Id.ToString(), _serverOptionsMonitor.CurrentValue.Api.JWT.Expires);
            var refresh_token = await updateRefresh_token(userInfo);

            ServiceLoginResult serviceLoginResult = new ServiceLoginResult()
            {
                acccess_token = token,
                expires_in = _serverOptionsMonitor.CurrentValue.Api.JWT.Expires.ToString(),
                id_token = token,
                refresh_token = refresh_token,
                refresh_token_expires_in = "99999",
                token_type = "Bearer",
            };
            return serviceLoginResult;
        }

        private async Task<string> updateRefresh_token(User user)
        {
            string randomString = CommonUtility.RandomGenerator();
            user.ResetToken = randomString;
            _userRepository.Update(user);
            await _uow.Commit();
            return randomString;
        }

    }
}
