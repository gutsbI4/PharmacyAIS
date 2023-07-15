using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Helpers;
using System;

namespace PharmacyAIS.ViewModels
{
    public class ViewModelBase : ReactiveValidationObject
    {
        protected string _title;
        protected readonly ValidationContext _validationContext = new ValidationContext();

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public ValidationContext ValidationContext => _validationContext;
    }
}