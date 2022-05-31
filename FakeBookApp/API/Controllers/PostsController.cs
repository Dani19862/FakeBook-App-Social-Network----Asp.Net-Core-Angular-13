using System.Net;
using System.Collections.Generic;
//using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using API.Helpers;
using API.Extensions;

namespace API.Controllers
{
    //[Authorize]
    public class PostController : BaseApiController
    {
        
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PostController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        // Add new Post
        [HttpPost]
        public async Task<ActionResult<PostDto>> AddPost(PostDto postDto)
        {
            
            var post = new Post 
            {
                //Id= postDto.Id,    
                Content = postDto.Content,
                AppUserId = postDto.AppUserId,
                Created = postDto.Created,
                //PhotoUrl = postDto.PhotoUrl,

            };

            _unitOfWork.PostRepository.AddPostAsync(post);

            if (await _unitOfWork.Complete()) return Ok(post); //return Ok(_mapper.Map(post, postDto)); // Exception in Mapper

            return BadRequest ("Could not add post");
            
        }


        // Edit Post
        [HttpPut] 
        public async Task<ActionResult> EditPost(PostDto postDto)
        {
            var post = await _unitOfWork.PostRepository.GetPostByIdAsync(postDto.Id);

            if (post == null) return NotFound();

            _mapper.Map(postDto, post); 
            
            await _unitOfWork.PostRepository.EditPostAsync(postDto);
          
            if (await _unitOfWork.Complete()) return NoContent();   // here have exeptoion with saving because the postDto.Id

            return BadRequest("Could not edit post");

        }

        // Delete Post
        [HttpDelete]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = await _unitOfWork.PostRepository.GetPostByIdAsync(id);

            if (post == null) return NotFound();

             _unitOfWork.PostRepository.DeletePostAsync(id);

            if (await _unitOfWork.Complete()) return Ok(post);

            return BadRequest("Could not delete post");
        }

        // Get all posts of a user
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetUserPostsAsync([FromQuery] PostParams postParams)
        {
            var post = await _unitOfWork.PostRepository.GetUserPostsAsync(postParams);

            Response.AddPaginationHeader(post.CurrentPage, post.PageSize, post.TotalCount, post.TotalPages);

            return Ok(post);

            
        }   


        // Get all posts
        [HttpGet("posts")]
        public async Task <ActionResult<PostDto>> GetAllPostsAsync([FromQuery] PostParams postParams)  
        {
            var post = await _unitOfWork.PostRepository.GetAllPostsAsync(postParams);

            Response.AddPaginationHeader(post.CurrentPage, post.PageSize, post.TotalCount, post.TotalPages);

            return Ok(post);
        } 
    }
}
