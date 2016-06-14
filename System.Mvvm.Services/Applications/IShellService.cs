using System.ComponentModel;

namespace System.Mvvm.Services
{
    using Properties;

    public partial interface IShellService : INotifyPropertyChanged
    {
        // IReadOnlyCollection<Task> TasksToCompleteBeforeShutdown { get; }
        // bool IsApplicationBusy { get; }

        event CancelEventHandler Closing;
        void ShowError(Exception exception, string displayMessage);

        AppSettings Settings { get; }


        #region -- [View zone] --

        object ShellView { get; }
        object LogView { get; set; }
        
        #endregion --- [View zone] ---

        #region -- [Dxx] --

        Lazy<object> Dxx_ListView { get; set; }
        Lazy<object> Dxx_DetailsView { get; set; }
        bool Dxx_IsEnabled { get; set; }

        #endregion --- [Dxx] ---

        #region -- [Txx] --

        Lazy<object> Txx_ListView { get; set; }
        Lazy<object> Txx_DetailsView { get; set; }
        bool Txx_IsEnabled { get; set; }

        #endregion --- [Txx] ---

        #region -- [Synonym] --

        object Synonym_ListView { get; set; }
        object Synonym_DetailsView { get; set; }

        #endregion --- [Synonym] ---

        #region -- [Report] --

        Lazy<object> Repot_ListView { get; set; }
        Lazy<object> Repot_DetailsView { get; set; }
        bool Reporting_IsEnabled { get; set; }

        #endregion --- [Report] ---

        //IMessageService MessageService { get; }

        TimeSpan SetPeriod(object _object);
    } // --- IShellService : interface ---
}