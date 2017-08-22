using log4net;
using NewsPortal.Contracts.Repositories;
using NewsPortal.Core.Model;
using NewsPortal.Dto;
using NewsPortal.Web.Models.Api;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NewsPortal.Web.Controllers.Api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/news")]
    public class NewsController : BaseController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private INewsItemRepository NewsItemRepository
        {
            get
            {
                return this.DependencyResolver.Resolve<INewsItemRepository>();
            }
        }

        private IAuthorRepository AuthorRepository
        {
            get
            {
                return this.DependencyResolver.Resolve<IAuthorRepository>();
            }
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                using (var repository = this.NewsItemRepository)
                {
                    var result = repository.GetAllAs<NewsItemFullInfoDto>().ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetById(Guid id)
        {
            try
            {
                using (var repository = this.NewsItemRepository)
                {
                    var result = repository.GetByIdAs<NewsItemFullInfoDto>(id);

                    if (result == null) return Request.CreateResponse(HttpStatusCode.NoContent);

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddNewsItem([FromBody] NewsItemModel item)
        {
            try
            {
                using (var newsItemRepository = this.NewsItemRepository)
                using (var authorRepository = this.AuthorRepository)
                {
                    var author = authorRepository.GetById(item.AuthorId);

                    if (author == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Can't found author");

                    var newsItem = NewsItem.Create(item.Title, item.Content, author);
                    newsItemRepository.Add(newsItem);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage UpdateNewsItem([FromBody] NewsItemFullInfoDto item)
        {
            try
            {
                using (var newsItemRepository = this.NewsItemRepository)
                {
                    var newsItem = newsItemRepository.GetById(item.Id);

                    if (newsItem == null) return Request.CreateResponse(HttpStatusCode.NoContent);

                    if (newsItem.AuthorId != item.AuthorId)
                    {
                        using (var authorRepository = this.AuthorRepository)
                        {
                            var author = authorRepository.GetById(item.AuthorId);

                            if (author != null)
                            {
                                newsItem.AuthorId = item.AuthorId;
                            }
                            else
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Author not found");
                            }
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(item.Title)) newsItem.Title = item.Title;

                    if (!string.IsNullOrWhiteSpace(item.Content)) newsItem.Content = item.Content;

                    newsItemRepository.Update(newsItem);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteNewsItem([FromUri] Guid id)
        {
            try
            {
                using (var newsItemRepository = this.NewsItemRepository)
                {
                    var newsItem = newsItemRepository.GetById(id);

                    if (newsItem == null) return Request.CreateResponse(HttpStatusCode.NoContent);

                    newsItemRepository.Delete(newsItem);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
