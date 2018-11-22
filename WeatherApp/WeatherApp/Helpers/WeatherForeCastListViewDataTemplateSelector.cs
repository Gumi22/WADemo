using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp.Helpers
{
    class WeatherForeCastListViewDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FirstRowTemplate { get; set; }

        public DataTemplate OtherRowTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return (item is WeatherForeCastListItemViewModel i && i.IsFirst)? FirstRowTemplate : OtherRowTemplate;
        }
    }
}
