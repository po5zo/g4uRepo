using System.Collections;
using System.Threading.Tasks;
using AutoMapper;
using g4u.Controllers.Resources;
using g4u.Core;
using g4u.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace g4u.Controllers
{
    [Route("/api/wishlist")]
    public class WishlistController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IWishlistRepository wishlistRepository;
        public WishlistController(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository, IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] SaveWishlistResource wishlistResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var wishlistItem = mapper.Map<SaveWishlistResource, Wishlist>(wishlistResource);
            wishlistItem.ProductId = wishlistResource.ProductId;
            wishlistItem.ProductIsExist = true;
            var isAddedBefore = wishlistRepository.SingleOrDefault(w => w.ProductId == wishlistItem.ProductId) != null;
            if (!isAddedBefore) 
                wishlistRepository.Add(wishlistItem);
            var user = await userRepository.SingleOrDefault(x => x.AuthSub == wishlistResource.AuthSub);
            if (!user.WishList.Contains(wishlistItem)) 
                user.WishList.Add(wishlistItem);
            await unitOfWork.CompleteAsync();
            wishlistItem = await wishlistRepository.Get(wishlistItem.Id);
            var result = mapper.Map<Wishlist, WishListResource>(wishlistItem);
            
            return Ok(result);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int productId)
        //{
        //    return null;
        //}

        //[Authorize]
        [HttpGet]
        public async Task<QueryResultResource<WishListResource>> GetProduct(WishlistQueryResource filterQuery)
        {
            var filter = mapper.Map<WishlistQueryResource, WishlistQuery>(filterQuery);
            var queryResult = await wishlistRepository.GetWishlistAsync(filter);

            return mapper.Map<QueryResult<Wishlist>, QueryResultResource<WishListResource>>(queryResult);
        }
    }
}