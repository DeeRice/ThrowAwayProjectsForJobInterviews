using IntegraPartnersContactApplicationAPI.Model;
using IntegraPartnersContactApplicationAPI.ViewModel;

namespace IntegraPartnersContactApplicationAPI.Interface
{
    public interface IMapping
    {
        UserViewModel MapEntityToViewModel(Users user);
        Users MapViewModelToEntity(UserViewModel userViewModel);

    }
}
