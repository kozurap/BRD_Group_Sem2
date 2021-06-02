using System.Linq;
using System.Net.Mime;
using BRD_Sem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BRD_Sem.Infrostructure
{
    public class GetPostImage
    {
        private readonly ApplicationContext _dbContext;

        public GetPostImage(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public byte[] GetImage(int postId)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
                return null;
            var data = post.Image;
            if (data == null)
                return null;
            return data;
        }
    }
}