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
    [RoutePrefix("api/authors")]
    public class AuthorsController : BaseController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                using (var repository = this.AuthorRepository)
                {
                    var result = repository.GetAllAs<AuthorDto>().ToList();

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
        [Route("short")]
        public HttpResponseMessage GetAllAsShort()
        {
            try
            {
                using (var repository = this.AuthorRepository)
                {
                    var result = repository.GetAllAs<AuthorShortInfoDto>().ToList();

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
        public HttpResponseMessage AddAuthor([FromBody] AuthorModel item)
        {
            try
            {
                using (var authorRepository = this.AuthorRepository)
                {
                    var newsItem = Author.Create(item.FirstName, item.LastName, item.BirthDate);

                    authorRepository.Add(newsItem);
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
        public HttpResponseMessage UpdateAuthor([FromBody] AuthorDto item)
        {
            try
            {
                using (var authorRepository = this.AuthorRepository)
                {
                    var author = authorRepository.GetById(item.Id);

                    if (author == null) return Request.CreateResponse(HttpStatusCode.NoContent);

                    if (!string.IsNullOrWhiteSpace(item.FirstName)) author.FirstName = item.FirstName;

                    if (!string.IsNullOrWhiteSpace(item.LastName)) author.LastName = item.LastName;

                    authorRepository.Update(author);
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
        public HttpResponseMessage DeleteAuthor([FromUri] Guid id)
        {
            try
            {
                using (var authorRepository = this.AuthorRepository)
                {
                    var author = authorRepository.GetById(id);

                    if (author == null) return Request.CreateResponse(HttpStatusCode.NoContent);

                    authorRepository.Delete(author);
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
