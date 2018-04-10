using System;
using System.Threading.Tasks;
using AutoMapper;
using g4u.Controllers.Resources;
using g4u.Core;
using g4u.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace g4u.Controllers
{
    [Route("/api/users")]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        public UserController(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] SaveUserResource userResource)
        {
            var user = mapper.Map<SaveUserResource, User>(userResource);
            user.CreateDate = DateTime.Now;
            user.DeleteDate = null;

            var isUserExist = userRepository.SingleOrDefault(u => u.AuthSub == user.AuthSub) != null;

            if (!isUserExist)
            {
                userRepository.Add(user);
                await unitOfWork.CompleteAsync();
                user = await userRepository.Get(user.Id);
                var result = mapper.Map<User, UserResource>(user);
                return Ok(result);
            }
            else return Ok();
        }

        [HttpGet]
        public async Task<QueryResultResource<UserResource>> GetUser(UserQueryResource filterQuery)
        {
            var filter = mapper.Map<UserQueryResource, UserQuery>(filterQuery);
            var queryResult = await userRepository.GetUser(filter);

            return mapper.Map<QueryResult<User>, QueryResultResource<UserResource>>(queryResult);
        }
    }
}