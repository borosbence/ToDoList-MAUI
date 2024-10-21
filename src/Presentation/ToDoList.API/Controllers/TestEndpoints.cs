namespace ToDoList.API.Controllers;

public static class TestEndpoints
{
    public static void MapTestEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Test");

        group.MapGet("/", () =>
        {
            return DateTime.Now;
        })
        .WithName("Test")
        .Produces(StatusCodes.Status200OK);
    }
}
