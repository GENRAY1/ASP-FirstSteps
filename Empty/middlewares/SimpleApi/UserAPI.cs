using System.Text.RegularExpressions;

namespace Empty.middlewares.SimpleApi
{
    public class User()
    {
        public int id { get; set; }
        public string username { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
    }

    public class UserAPI
    {
        private List<User> _users = new List<User>()
        {
            new() {id = 0, username = "genray", login="admin", pass = "3232"},
            new() {id = 1, username = "elena2", login="el323", pass = "123456"},
            new() {id = 2, username = "kir", login="kir343", pass = "321415"},

        };

        public async Task Handler(HttpContext context)
        {
            var response = context.Response;
            var request = context.Request;
            var path = request.Path;
            string expressionForUserId = "^/([0-9]+)$";

            if (path == "" && request.Method == "GET")
                await GetAllUsers(response);

            else if (path == "" && request.Method == "POST")
                await CreateUser(response, request);

            else if (Regex.IsMatch(path, expressionForUserId) && request.Method == "GET")
            {
                int? id = Convert.ToInt32(path.Value?.Split("/")[1]);
                await GetUser(response, id);
            }
            else if (Regex.IsMatch(path, expressionForUserId) && request.Method == "DELETE")
            {
                int? id = Convert.ToInt32(path.Value?.Split("/")[1]);
                await DeleteUser(response, id);
            }
            
        }

        private async Task GetAllUsers(HttpResponse response)
        {
            await response.WriteAsJsonAsync(_users);
        }
        private async Task GetUser(HttpResponse response, int? id)
        {
            User? currentUser = _users.FirstOrDefault(x => x.id == id);
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

        private async Task DeleteUser(HttpResponse response, int? id)
        {
            User? currentUser = _users.FirstOrDefault(x => x.id == id);
            if (currentUser != null)
            {
                _users.Remove(currentUser);
                await response.WriteAsJsonAsync(currentUser);
            }
            else
            {
                response.StatusCode = 404;
                await response.WriteAsJsonAsync(new { message = "Пользователь не найден" });
            }
        }

        private async Task CreateUser(HttpResponse response, HttpRequest request)
        {
            try
            {
                var user = await request.ReadFromJsonAsync<User>();

                if (user != null)
                {
                    Console.WriteLine(user.id);
                    _users.Add(user);
                    await response.WriteAsJsonAsync(user);
                }
                else
                {
                    throw new Exception("Некорректные данные");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                await response.WriteAsJsonAsync(new { message = "Некорректные данные" });
            }

        }
        private async Task UpdateUser(HttpResponse response, int? id)
        {

        }

    }
}
