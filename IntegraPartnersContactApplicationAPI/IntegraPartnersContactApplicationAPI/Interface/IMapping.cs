using IntegraPartnersContactApplicationAPI.Model;
using IntegraPartnersContactApplicationAPI.ViewModel;

namespace IntegraPartnersContactApplicationAPI.Interface
{
    public interface IMapping
    {
        UsersViewModel MapEntityToViewModel(Users user);
        Users MapViewModelToEntity(UsersViewModel userViewModel);

    }
}
