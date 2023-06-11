using CrmAssistant.Models.ViewModels;

namespace CrmAssistant.Models.Map
{
    public class UserMap
    {
        public static UserViewModel toUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Role = user.Role,
                Address = user.Address,
                Countries = user.Countries,
                HobbiesIds = user.Hobbies.Select(x => x.Id ).ToList(),
            };
        }

        public static User toUser(UserViewModel userViewModel)
        {
            return new User
            {
                Id = userViewModel.Id,
                Firstname = userViewModel.Firstname,
                Lastname = userViewModel.Lastname,
                Email = userViewModel.Email,
                Role = userViewModel.Role,
                Address = userViewModel.Address,
                Countries = userViewModel.Countries,
                Hobbies = userViewModel.HobbiesIds.Select(x => new Hobbie() { Id = x}).ToList(),
            };
        }
    }
}
