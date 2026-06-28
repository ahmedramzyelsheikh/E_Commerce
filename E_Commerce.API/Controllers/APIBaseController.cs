using E_Commerce.API.Common;
using E_Commerce.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController : ControllerBase
    {
        public  static ActionResult<T> ToActionResult<T>(Results<T> result)
        {
            

            if (result.IsSuccessed) return new OkObjectResult(result);
            
                return TOProblem(result.Error);
        
        }
        public static ActionResult ToActionResult(Common.Results result)
        {

            if (result.IsSuccessed) return new OkObjectResult(result);

            return TOProblem(result.Error);

        }

        protected  static ObjectResult TOProblem(IReadOnlyList<Errors> error)
        {

            //  to make problem details status code   
            var firsterror = error[0];

            var statuscode = firsterror.Error switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Conflict => StatusCodes.Status409Conflict  ,
                ErrorType.Validation =>  StatusCodes.Status400BadRequest ,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized, 
                _ => StatusCodes.Status500InternalServerError



            };
            var problem = new ProblemDetails()
            {
                Status = statuscode,
                Title = firsterror.code,
                Detail = firsterror.description,
                Extensions = { ["errors"] = error }

            };
            return new ObjectResult(problem); 

        }
    }
}
