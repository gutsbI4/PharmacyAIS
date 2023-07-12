using PharmacyAIS.Events;
using PharmacyAIS.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Services
{
    public class ViewModelService:IViewModelService
    {
        private List<Lazy<ViewModelBase>> _mainMenuViewModels;
        public ViewModelService()
        {
            _mainMenuViewModels = new List<Lazy<ViewModelBase>>
            {
                new Lazy<ViewModelBase>(() => new HomeViewModel()),
                new Lazy<ViewModelBase>(() => new ClientsOrderViewModel()),
                new Lazy<ViewModelBase>(() => new ProductsViewModel()),
                new Lazy<ViewModelBase>(() => new SuppliersOrderViewModel()),
                new Lazy<ViewModelBase>(() => new UsersViewModel())
            };
        }

        public void ChangeContentViewModel(ViewModelBase newContentViewModel)
        {
            MessageBus.Current.SendMessage(new ContentViewModelChanged { NewContentViewModel = newContentViewModel });
        }

        public IEnumerable<ViewModelBase> GetMainMenuViewModels()
        {
            return _mainMenuViewModels.Select(lazyViewModel => lazyViewModel.Value);
        }

        public TViewModel GetViewModel<TViewModel>() where TViewModel : ViewModelBase
        {
            return _mainMenuViewModels.Select(lazyViewModel => lazyViewModel.Value).OfType<TViewModel>().FirstOrDefault();
        }
      
    }
}
