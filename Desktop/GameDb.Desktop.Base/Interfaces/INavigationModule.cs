using Prism.Regions;

namespace GameDb.Desktop.Base.Interfaces
{
    public interface INavigationModule
    {
        void NavigateTo(PageNames pageName, string regionName = RegionNames.MainRegion);
        void NavigateTo(PageNames pageName, NavigationParameters parameters, string regionName = RegionNames.MainRegion);
        void ClearNavigationJournal();
        void NavigateForward();
        bool CanNavigateForward();
        void NavigateBack();
        bool CanNavigateBack();
    }
}