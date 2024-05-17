using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace CompanyEmployees.Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public ValidationFilterAttribute()
        { }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"]; // retrieve the value on the current routing path since we need the name of the action
            var controller = context.RouteData.Values["controller"];// retrieve the value on the current routing path since we need the name of the controller

            // we use ActionArguments dictionary to extract the Dto paramater that we send to the post and put actions
            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null Controller: {controller} action:{action}");
                return;
            }

            if (!context.ModelState.IsValid )
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
