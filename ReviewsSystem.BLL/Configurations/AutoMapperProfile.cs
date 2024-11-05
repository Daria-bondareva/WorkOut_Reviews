using AutoMapper;
using ReviewsSystem.BLL.DTO.Requests;
using ReviewsSystem.BLL.DTO.Responses;
using ReviewsSystem.DAL.Entities;

namespace ReviewsSystem.BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        private void CreateUserMaps()
        {
            CreateMap<UserRequest, User>(); 
            CreateMap<User, UserResponse>();
        }
        private void CreateWorkOutMaps()
        {
            CreateMap<WorkOutRequest, WorkOut>();
            CreateMap<WorkOut, WorkOutResponse>();
        }
        private void CreateReviewMaps()
        {
            CreateMap<ReviewRequest, Review>(); 
            CreateMap<Review, ReviewResponse>() 
                .ForMember(
                    response => response.DatePosted,
                    options => options.MapFrom(review => DateTime.Now)); 
        }

        public AutoMapperProfile() 
        {
            CreateUserMaps(); 
            CreateWorkOutMaps(); 
            CreateReviewMaps();
        }
    }
}
