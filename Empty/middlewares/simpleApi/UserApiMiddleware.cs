using Empty.dbcontext;
using Empty.models;
using Empty.services.storage.abstractions;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Empty.middlewares.simpleApi
{
    public class UserApiMiddleware
    {
        private readonly RequestDelegate next;
        public UserApiMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository repository)
        {
            var response = context.Response;
            var request = context.Request;
            var path = request.Path;
            string expressionForUserId = "^/([0-9]+)$";

            if (path == "" && request.Method == "GET")
                await GetAllUsers(response, repository);
            else if (path == "" && request.Method == "POST")
                await CreateUser(response, request, repository);
            else if (path == "" && request.Method == "PUT")
                await UpdateUser(response, request, repository);
            else if (Regex.IsMatch(path, expressionForUserId) && request.Method == "GET")
            {
                int? id = Convert.ToInt32(path.Value?.Split("/")[1]);
                await GetUser(response, id, repository);
            }
            else if (Regex.IsMatch(path, expressionForUserId) && request.Method == "DELETE")
            {
                int? id = Convert.ToInt32(path.Value?.Split("/")[1]);
                await DeleteUser(response, id, repository);
            }
            else
            {
                await next.Invoke(context);
            }

        }
        private async Task GetAllUsers(HttpResponse response, IUserRepository repository)
        {
            List<User> users = await repository.GetAllAsync();
            if(users.Count == 0)
            {
                await response.WriteAsJsonAsync(new { message = "Пользователи в системе отсутсвуют" });
            }
            else
            {
                await response.WriteAsJsonAsync(users);
            }
            
        }
        
        private async Task GetUser(HttpResponse response, int? id, IUserRepository repository)
        {
            if (id.HasValue)
            {
                User? currentUser = await repository.GetAsync(id.Value);
                if (currentUser != null)
                {
                    await response.WriteAsJsonAsync(currentUser);
                }
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
                }
            }
            else
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = "Неккоректный параметр id" });
            }
        }

        private async Task DeleteUser(HttpResponse response, int? id, IUserRepository repository)
        {
            if (id.HasValue)
            {
                User? currentUser = await repository.GetAsync(id.Value);
                if (currentUser != null)
                {
                    await repository.DeleteAsync(id.Value);
                    await response.WriteAsJsonAsync(currentUser);
                }
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
                }

            }
            else
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = "Неккоректный параметр id" });
            }
        }

        private async Task CreateUser(HttpResponse response, HttpRequest request, IUserRepository repository)
        {
            try
            {
                var user = await request.ReadFromJsonAsync<User>();

                if (user != null)
                {
                    if(string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Login))
                    {
                        await response.WriteAsJsonAsync(new { message = "Поля: username, login, pass не должны быть пустыми"});
                        return;
                    }

                    if(user.Id == 0)
                    {
                        await repository.AddAsync(user);
                        await response.WriteAsJsonAsync(user);
                    }
                    else
                    {
                       await response.WriteAsJsonAsync(new { message = "Поле id должно быть пустым"});
                    }
                }
                else
                {
                    throw new Exception("Некорректные данные");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = ex.Message});
            }

        }
        private async Task UpdateUser(HttpResponse response, HttpRequest request, IUserRepository repository)
        {
            User? updatedUser = await request.ReadFromJsonAsync<User>();
            if (updatedUser != null)
            {
                User? currentUser = await repository.GetAsync(updatedUser.Id);
                if (currentUser != null)
                {
                    if (!string.IsNullOrEmpty(updatedUser.Username))
                    {
                        currentUser.Username = updatedUser.Username;
                    }

                    if (!string.IsNullOrEmpty(updatedUser.Login))
                    {
                        currentUser.Login = updatedUser.Login;
                    }

                    if (!string.IsNullOrEmpty(updatedUser.Pass))
                    {
                        currentUser.Pass = updatedUser.Pass;
                    }


                    await repository.UpdateAsync(currentUser);
                    await response.WriteAsJsonAsync(updatedUser);
                }
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
                }
            }
            else
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
            }
            

            
        }
        
    }
}
     