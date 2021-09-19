using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Post GetPost(int id)
        {
            return _appDbContext.Posts.FirstOrDefault(p => p.Id == id);
        }

        public List<Post> GetAllPosts()
        {
            return _appDbContext.Posts.ToList();
        }

        public void UpdatePost(Post post)
        {
            _appDbContext.Posts.Update(post);
        }

        public void RemovePost(int id)
        {
            _appDbContext.Posts.Remove(GetPost(id));
        }

        public void AddPost(Post post)
        {
            _appDbContext.Posts.Add(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _appDbContext.SaveChangesAsync()>0)
            {
                return true;
            }
            return false;
        }
    }
}
