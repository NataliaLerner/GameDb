using System.ComponentModel;
using GameDb.Desktop.Base;
using Prism.Mvvm;

namespace GameDb.Desktop.Pages.Main.ViewModels
{
    public class PageConfiguration : BindableBase
    {
        public PageConfiguration(PageNames enumValue, string displayName)
        {
            _pageEnumValue = enumValue;
            _displayName = displayName;
        }

        private PageNames _pageEnumValue;
        public PageNames PageEnumValue => _pageEnumValue;

        private string _displayName;
        public string DisplayName => _displayName;
    }
}