using GrapecityAssignment.DataContext;
using GrapecityAssignment.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace GrapecityAssignment.Service
{
    public class Blogging
    {
        
        public string GetAllBlogs()
        {
            string result = string.Empty;
            try
            {
                var options = new DbContextOptions<Context>();
                using (var context = new Context(options))
                    if (context != null)
                    {
                        var contextData = context.Post.Include(it => it.Comments).AsNoTracking().Select(it => new { it.Id, it.Title, it.Content, it.CreatedDate, it.ModifiedDate, it.Comments }).ToList();
                        result = JsonConvert.SerializeObject(contextData);
                    }
            }
            catch (Exception) { }
            return JsonConvert.SerializeObject(result);
        }

        public string PostBlogs(PostModel postModel)
        {
            string result = string.Empty;
            try
            {
                var options = new DbContextOptions<Context>();
                using (var context = new Context(options))
                    if (context != null)
                    {
                        if(postModel.CrudOperationType == CrudOperationType.Create)
                            context.Post.Add(postModel);
                        if (postModel.CrudOperationType == CrudOperationType.Delete)
                        {
                            if (context.Post.Find(postModel.Id) != null)
                                context.Post.Remove(context.Post.FirstOrDefault(it => it.Id == postModel.Id));
                        }
                        if(postModel.CrudOperationType == CrudOperationType.Update)
                        {
                                if (context.Post.Find(postModel.Id) != null)
                                {
                                    var listDBComments = context.Comments.AsNoTracking().Where(it => it.PostId == postModel.Id).ToList();
                                    if (listDBComments.Count == postModel.Comments.Count())
                                    {
                                        if (postModel.Comments.FirstOrDefault(it => it.CrudOperationType == CrudOperationType.Update) != null)
                                        {
                                            var data = postModel.Comments.FirstOrDefault(it => it.CrudOperationType == CrudOperationType.Update);
                                            context.Entry(postModel).State = EntityState.Detached;
                                            context.Entry(data).State = EntityState.Detached;
                                            data.ModifiedDate = DateTime.Now;
                                            context.Comments.Update(data);
                                            context.SaveChanges();
                                        }
                                        if (postModel.Comments.FirstOrDefault(it => it.CrudOperationType == CrudOperationType.Delete) != null)
                                        {
                                            var deleteComment = postModel.Comments.FirstOrDefault(it => it.CrudOperationType == CrudOperationType.Delete);
                                            if (context.Comments.Find(deleteComment.Id) != null)
                                                context.Comments.Remove(context.Comments.FirstOrDefault(it => it.Id == deleteComment.Id));
                                        }
                                        if (postModel.Comments.FirstOrDefault(it => it.CrudOperationType == CrudOperationType.Create) != null)
                                        {
                                            var newComment = postModel.Comments.FirstOrDefault(it => it.CrudOperationType == CrudOperationType.Create);
                                            context.Comments.Add(newComment);
                                        }
                                    }
                                    else
                                    {
                                        if (postModel.Comments.FirstOrDefault(it => it.CrudOperationType == CrudOperationType.Create) != null)
                                        {
                                            var newComment = postModel.Comments.FirstOrDefault(it => it.CrudOperationType == CrudOperationType.Create);
                                            context.Comments.Add(newComment);
                                        }
                                    }
                                }
                                else
                                {
                                    context.Post.Add(postModel);
                                }
                        }
                        context.SaveChanges();
                    }
            }
            catch (Exception ex) { }
            return JsonConvert.SerializeObject(result);
        }
    }
}
