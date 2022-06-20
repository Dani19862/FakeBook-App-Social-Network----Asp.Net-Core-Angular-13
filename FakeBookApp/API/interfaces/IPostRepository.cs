using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using System.Collections.Generic;


namespace API.interfaces
{
    public interface IPostRepository
    {
        void AddPost(Post post);

        void DeletePost(int id);

        //Task<Post> EditPostAsync(PostDto postDto);

        void EditPost(Post post);

        Task<Post> GetPostByIdAsync(int id);

        //Task<PagedList<PostDto>> GetAllPostsAsync (PostParams postParams); => with pagination

        //Task <IEnumerable<PostDto>> GetAllPostsAsync(); // old
        
        Task <List<PostDto>> GetAllPostsAsync(PostParams postParams); // new

        //Task<IEnumerable<PostDto>> GetUserPostsAsync(PostDto postDto);

        //Task<PagedList<PostDto>> GetUserPostsAsync(PostParams postParams); => with pagination

        //Task<IEnumerable<PostDto>> GetUserPostsAsync(PostParams postParams);

       Task<IEnumerable<PostDto>> GetUserPostsAsync(string userName);

        //void Update (Post post);

        

        
    }
}
