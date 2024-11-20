using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NullpointerAPI.Data;
using NullpointerAPI.Models;
using NullpointerAPI.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace NullpointerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PostsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/Posts")]
        public async Task<IActionResult> GetPosts()
        {
            var postDatas = await _context.Posts
                .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(postDatas);
        }

        [HttpGet]
        [Route("/Post/{Id}")]
        public async Task<IActionResult> GetPost(int Id)
        {
            var postData = await _context.Posts
                .ProjectTo<PostViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.PostId == Id);
            return Ok(postData);
        }

        [HttpPost]
        [Route("/CreatePost")]
        public async Task<IActionResult> CreatePost(PostViewModel postData)
        {
            try
            {
                Post post = _mapper.Map<Post>(postData);
                post.CreationDateTime = DateTime.UtcNow;
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }

            return Ok();
        }
    }
}
