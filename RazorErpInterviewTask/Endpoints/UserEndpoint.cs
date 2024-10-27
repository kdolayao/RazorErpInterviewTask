using RazorErpInterviewTask.Application.Models;
using RazorErpInterviewTask.Application.Services;
using RazorErpInterviewTask.Core.Entities;
using RazorErpInterviewTask.Core.Enums;
using System.Data;
using System.Security.Claims;

namespace RazorErpInterviewTask.WebApi.Endpoints
{
    public static class UserEndpoint
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/users", async (UserService userService, ClaimsPrincipal userClaim) =>
            {
                var role = GetClaimValue(userClaim, 2); //Get role
                var company = GetClaimValue(userClaim, 3); //Get companyname

                var users = await userService.GetAllUsersAsync(role, company);
                return Results.Ok(users);
            }).RequireAuthorization();

            routes.MapGet("/api/users/{id}", async (int id, UserService userService, ClaimsPrincipal userClaim) =>
            {
                var role = GetClaimValue(userClaim, 2); //Get role
                var company = GetClaimValue(userClaim, 3); //Get companyname

                var user = await userService.GetUserByIdAsync(id, role, company);
                return user is not null ? Results.Ok(user) : Results.NotFound();
            }).RequireAuthorization();

            routes.MapPost("/api/users", async (UserAddUpdate user, UserService userService) =>
            {
                await userService.AddUserAsync(user);
                return Results.Ok();
            }).RequireAuthorization(policy => policy.RequireRole(Role.Admin.ToString()));

            routes.MapPut("/api/users/{id}", async (int id, UserAddUpdate user, UserService userService, ClaimsPrincipal userClaim) =>
            {
                var role = GetClaimValue(userClaim, 2); //Get role
                var company = GetClaimValue(userClaim, 3); //Get companyname

                var existingUser = await userService.GetUserByIdAsync(id, role, company);
                if (existingUser is null)
                    return Results.NotFound();

                await userService.UpdateUserAsync(id, user);
                return Results.Ok(user);
            }).RequireAuthorization(policy => policy.RequireRole(Role.Admin.ToString()));

            routes.MapDelete("/api/users/{id}", async (int id, UserService userService) =>
            {
                await userService.DeleteUserAsync(id);
                return Results.NoContent();
            }).RequireAuthorization(policy => policy.RequireRole(Role.Admin.ToString()));

            routes.MapPost("/auth", async (UserLogin user, UserService userService) =>
            {
                var token = await userService.Auth(user);
                if (string.IsNullOrEmpty(token))
                    return Results.Unauthorized();

                return Results.Ok(new { Token = token });
            }).AllowAnonymous();

        }

        private static string GetClaimValue(ClaimsPrincipal userClaim, int index)
        {
            return userClaim.Claims.ElementAtOrDefault(index)?.Value;
        }
    }
}
