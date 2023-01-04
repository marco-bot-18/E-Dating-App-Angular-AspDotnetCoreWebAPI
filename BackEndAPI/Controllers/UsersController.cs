using System.Security.Claims;
using AutoMapper;
using BackEndAPI.Data;
using BackEndAPI.DTOs;
using BackEndAPI.Entities;
using BackEndAPI.Extensions;
using BackEndAPI.Helpers;
using BackEndAPI.Interfaces;
using BackEndAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAPI.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
            _uow = uow;
            _mapper = mapper;
        }

        //[AllowAnonymous] //- test the API in postman without this data annotation
        // [Authorize(Roles = "Admin")]
        [HttpGet] //All users
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            var gender = await _uow.UserRepository.GetUserGender(User.GetUsername());
            userParams.CurrentUsername = User.GetUsername();

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = gender == "male" ? "female" : "male";
            }

            var users = await _uow.UserRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(users);
        }

        // [Authorize(Roles = "Member")]
        [HttpGet("{username}")] //Single user
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var currentUsername = User.GetUsername();
            return await _uow.UserRepository.GetMemberAsync(username,
            isCurrentUser: currentUsername == username
            );
            //return await _uow.UserRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null)
                return NotFound();

            _mapper.Map(memberUpdateDto, user);

            if (await _uow.Complete())
                return NoContent(); //status code: 204
            else
                return BadRequest("Nothing must be changed on your profile."); //status code: 400
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            user.Photos.Add(photo);
            if (await _uow.Complete())
            {
                return CreatedAtRoute("GetUser", new
                {
                    username = user.UserName
                },
                 _mapper.Map<PhotoDto>(photo));
            }
            return BadRequest("Problem addding photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());
            if (user == null)
                return NotFound();

            var photo = await _uow.PhotoRepository.GetPhotoById(photoId);
            if (photo == null)
                return NotFound();

            if (photo.IsMain)
                return BadRequest("This is already your main photo!");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null)
                currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _uow.Complete())
                return NoContent();
            else
                return BadRequest("There's a problem setting the main photo.");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null)
                return NotFound();

            if (photo.IsMain)
                return BadRequest("You cannot delete your main photo!");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null)
                    return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _uow.Complete())
                return Ok();
            else
                return BadRequest("There's a problem deleting the photo!");
        }
    }
}