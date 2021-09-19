using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        void UpdatePost(Post post);
        void RemovePost(int id);
        void AddPost(Post post);
        Task<bool> SaveChangesAsync();

    }
}
