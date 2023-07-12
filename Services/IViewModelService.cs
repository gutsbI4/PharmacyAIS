using PharmacyAIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Services
{
    public interface IViewModelService
    {
        IEnumerable<ViewModelBase> GetMainMenuViewModels();
        TViewModel GetViewModel<TViewModel>() where TViewModel : ViewModelBase;
        void ChangeContentViewModel(ViewModelBase newContentViewModel);
    }
}
