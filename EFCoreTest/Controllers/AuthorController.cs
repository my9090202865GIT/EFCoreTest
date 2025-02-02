using EFCoreTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDBContext appDBcontext;

        public AuthorController(AppDBContext appDBcontext)
        {
            this.appDBcontext = appDBcontext;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author model)
        {
            appDBcontext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //manually changing entity state.
            appDBcontext.SaveChanges();
            return Ok(model);
        }
        [HttpPut("/[action]")]
        public async Task<IActionResult> UpdateAuthorInBulk([FromBody] List<Author> models)
        {
            //var Authors = await appDBcontext.Authors.ToListAsync();
            //foreach (var item in models)
            //{
            //    var authorFound = Authors.FirstOrDefault(aut => aut.Id == item.Id);
            //    if (authorFound != null)
            //    {
            //        authorFound.Name = item.Name;
            //        authorFound.Email = item.Email;
            //    }
            //    else
            //    {
            //        return NotFound(item.Id);
            //    }
            //}
            //await appDBcontext.SaveChangesAsync();
            // savechanges() not required in the below way to update in bulk. single query generated, one time DB hit. It is .net 7 feature.
            await appDBcontext.Authors
                .Where(A => A.Name.Length >= 2)
                .ExecuteUpdateAsync(Auth => Auth
            .SetProperty(auth => auth.Name, auth => auth.Email + "Name") // can access other columns data in the table to modify another column.
            .SetProperty(auth => auth.Email, auth => auth.Email + "Email")
            ); // ExecuteUpdateAsync(). Since this is a direct SQL operation that doesn’t need to involve the DbContext’s change tracker
            return Ok(models);
        }
    }
}