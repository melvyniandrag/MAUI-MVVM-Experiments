using MVVM_Experiments.ViewModels;

namespace MVVM_Experiments
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private ViewModelInherited vm = new ViewModelInherited() { 
            FirstName = "Florencia",
            LastName = "Cienfuegos"
        };


        public MainPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }


    }

}
