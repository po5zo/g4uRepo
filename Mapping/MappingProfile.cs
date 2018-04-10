using AutoMapper;
using g4u.Controllers.Resources;
using g4u.Core.Models;

namespace g4u.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Product, SaveProductResource>();
            CreateMap<Photo, PhotoResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Platform, PlatformResource>();
            CreateMap<User, UserResource>();
            CreateMap<User, SaveUserResource>();
            CreateMap<Message, MessageResource>();
            CreateMap<Product, ProductResource>();
            CreateMap<UserMessages, MessagesWithUsersResource>();
            CreateMap<ChatUser, ChatUserResource>();
            CreateMap<Wishlist, WishListResource>();
            CreateMap<Wishlist, SaveWishlistResource>();

            
            //API Resource to Domain
            CreateMap<WishlistQueryResource, WishlistQuery>();
            CreateMap<ProductQueryResource, ProductQuery>();
            CreateMap<UserQueryResource, UserQuery>();
            CreateMap<SaveProductResource, Product>();
            CreateMap<SaveWishlistResource, Wishlist>();
            CreateMap<SaveUserResource, User>();
        }
    }
}