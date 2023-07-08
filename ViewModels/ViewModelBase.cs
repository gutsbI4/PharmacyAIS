using ReactiveUI;

namespace PharmacyAIS.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        protected string _title;

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
        
    }
}