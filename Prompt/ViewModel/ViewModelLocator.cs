/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Prompt.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Prompt.ViewModel
{
    using JetBrains.Annotations;
    using View.Commands;

    /// <summary>
    ///   This class contains static references to all the view models in
    ///   the application and provides an entry point for the bindings.
    ///   <para>See http://www.mvvmlight.net</para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // mvvmRegisterType's
            SimpleIoc.Default.Register<EditCommands>();
            SimpleIoc.Default.Register<TeleprompterViewModel>();
            SimpleIoc.Default.Register<ControllerCommands>();
        }

        /// <summary>
        ///   Gets the edit commands.
        /// </summary>
        /// <value>The edit commands.</value>
        [NotNull]
        public EditCommands EditCommands => ServiceLocator.Current.GetInstance<EditCommands>();

        public TeleprompterViewModel Teleprompter => ServiceLocator.Current.GetInstance<TeleprompterViewModel>();
        public ControllerCommands ControllerCommands => ServiceLocator.Current.GetInstance<ControllerCommands>();
        /// <summary>
        ///   Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}
