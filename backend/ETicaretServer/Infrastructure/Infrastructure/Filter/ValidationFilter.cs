using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Filter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        // Bu kısım validator da çarpışan hatalar olup olmadığına bakacak yoksa next diyecek
        public async Task OnActionExecutionAsync(ActionExecutingContext _context,ActionExecutionDelegate next)
        {
            if (!_context.ModelState.IsValid)
            {
                var errors = _context.ModelState.Where(x => x.Value.Errors.Any()).ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage)).ToArray();
                _context.Result = new BadRequestObjectResult(errors);
                return;
            }
            await next();
        }
    }
}
