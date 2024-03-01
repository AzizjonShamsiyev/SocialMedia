using Application.DTOs.CommentDto;
using Application.DTOs.FollowerDto;
using Application.DTOs.PostDto;
using Application.DTOs.UserDto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        UserMapping();
        FollowerMapping();
        CommentMapping();
        PostMapping();
    }

    private void UserMapping()
    {
        CreateMap<User, UserGetDto>()
            .ForMember(dest => dest.CommentsId, opt =>
                opt.MapFrom(src =>
                    src.Comments.Select(x => x.Id)))
            .ForMember(dest => dest.PostsId, opt =>
                opt.MapFrom(src =>
                    src.Posts.Select(x => x.Id)))
            .ForMember(dest => dest.FollowedId, opt =>
                opt.MapFrom(src =>
                    src.Followed.Select(x => x.Id)))
            .ForMember(dest => dest.FollowedId, opt =>
                opt.MapFrom(src =>
                    src.Followed.Select(x => x.Id)));

        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Followed, opt => opt.Ignore())
            .ForMember(dest => dest.Followers, opt => opt.Ignore())
            .ForMember(dest => dest.Posts, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ReverseMap();

        CreateMap<UserUpdateDto,User>()
            .ForMember(dest => dest.Followed, opt => opt.Ignore())
            .ForMember(dest => dest.Followers, opt => opt.Ignore())
            .ForMember(dest => dest.Posts, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ReverseMap();
    }

    private void FollowerMapping()
    {
        CreateMap<FollowerDto, Follower>().ReverseMap();

        CreateMap<FollowerCreateDto, Follower>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FollowedUserId, opt => opt.MapFrom(src => src.FollowedUserId))
            .ForMember(dest => dest.FollowerUserId, opt => opt.MapFrom(src => src.FollowerUserId));
        
    }

    private void CommentMapping()
    {
        CreateMap<CommentCreateDto, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<CommentUpdateDto, Comment>()
            .ForMember(dest => dest.PostId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime ,opt => opt.Ignore());

        CreateMap<Comment, CommentGetDto>();
    }

    private void PostMapping()
    {
        CreateMap<Post, PostGetDto>()
            .ForMember(dest => dest.CommentsId,
                opt => opt.MapFrom(
                    src => src.Comments.Select(x => x.Id)));

        CreateMap<PostUpdateDto, Post>()
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());

        CreateMap<PostCreateDto, Post>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
    }

}
