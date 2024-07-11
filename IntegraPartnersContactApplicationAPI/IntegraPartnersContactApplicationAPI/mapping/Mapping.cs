using IntegraPartnersContactApplicationAPI.Interface;
using IntegraPartnersContactApplicationAPI.Model;
using IntegraPartnersContactApplicationAPI.ViewModel;

namespace IntegraPartnersContactApplicationAPI.mapping
{
    public class Mapping : IMapping
    {
        public UserViewModel MapEntityToViewModel(Users user)
        {
            UserViewModel userViewModel = null;
            if (user != null)
            {
                userViewModel = new UserViewModel();
                userViewModel.UserID = user.user_id;
                userViewModel.Username = user.user_name;
                userViewModel.FirstName = user.first_name;
                userViewModel.LastName = user.last_name;
                userViewModel.Email = user.email;
                userViewModel.Department = user.department;
                userViewModel.UserStatus = user.user_status;
                return userViewModel;
            }
            else
            {
                return userViewModel;
            }
        }
        public Users MapViewModelToEntity(UserViewModel userViewModel)
        {
            Users user = null;
            if (userViewModel != null)
            {
                user = new Users();
                user.user_id = userViewModel.UserID;
                user.user_name = userViewModel.Username;
                user.first_name = userViewModel.FirstName;
                user.last_name = userViewModel.LastName;
                user.email = userViewModel.Email;
                user.department = userViewModel.Department;
                user.user_status = userViewModel.UserStatus;
                return user;
            }
            else
            {
                return user;
            }
        }
    }
}
