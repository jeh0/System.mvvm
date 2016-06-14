using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;

namespace System.Mvvm.Data
{
    using PeEm.Core;
    using Services;

    [Export, Export(typeof(IFileSystemWatcherService)), PartMetadata(UnitTestMetadata.c_Name, UnitTestMetadata.c_Data)]
    public class FileSystemWatcherService : Disposable, IFileSystemWatcherService
    {
        private readonly TaskScheduler m_TaskScheduler;
        private readonly FileSystemWatcher m_Watcher;


        [ImportingConstructor]
        public FileSystemWatcherService()
        {
            m_TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            m_Watcher = new FileSystemWatcher();
            m_Watcher.Created += this.WatcherCreated;
            m_Watcher.Renamed += this.WatcherRenamed;
            m_Watcher.Deleted += this.WatcherDeleted;
        }


        public NotifyFilters NotifyFilter
        {
            get { return m_Watcher.NotifyFilter; }
            set { m_Watcher.NotifyFilter = value; }
        }

        public string Path
        {
            get { return m_Watcher.Path; }
            set { m_Watcher.Path = value; }
        }

        public bool EnableRaisingEvents
        {
            get { return m_Watcher.EnableRaisingEvents; }
            set { m_Watcher.EnableRaisingEvents = value; }
        }


        public event FileSystemEventHandler Created;

        public event RenamedEventHandler Renamed;

        public event FileSystemEventHandler Deleted;


        protected virtual void OnCreated(FileSystemEventArgs _e) => this.Created?.Invoke(this, _e);

        protected virtual void OnRenamed(RenamedEventArgs _e) => this.Renamed?.Invoke(this, _e);

        protected virtual void OnDeleted(FileSystemEventArgs _e) => this.Deleted?.Invoke(this, _e);

        private void WatcherCreated(object _sender, FileSystemEventArgs _e)
        {
            Logger.Verbose(Logger.GetMemberName());
            TaskHelper.Run(() => OnCreated(_e), m_TaskScheduler);
        }

        private void WatcherRenamed(object _sender, RenamedEventArgs _e)
        {
            Logger.Verbose(Logger.GetMemberName());
            TaskHelper.Run(() => OnRenamed(_e), m_TaskScheduler);
        }

        private void WatcherDeleted(object _sender, FileSystemEventArgs _e)
        {
            Logger.Verbose(Logger.GetMemberName());
            TaskHelper.Run(() => OnDeleted(_e), m_TaskScheduler);
        }

        protected override void Dispose(bool _isDisposing)
        {
            if (_isDisposing)
                m_Watcher.Dispose();
            base.Dispose(_isDisposing);
        }
    }
}
