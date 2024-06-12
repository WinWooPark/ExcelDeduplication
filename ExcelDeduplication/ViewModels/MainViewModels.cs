using ExcelDeduplication.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace ExcelDeduplication.ViewModels
{
    public class MainViewModels : ObservableObject
    {
        #region 핃드

        RelayCommand _addFile;
        RelayCommand _deleteFile;
        RelayCommand _duplicate;

        ObservableCollection<string> _filePath;

        string _seleteItem;
        #endregion

        #region 속성

        public ObservableCollection<string> FilePath 
        {
            get { return _filePath; }
            set 
            { 
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public string SeleteItem 
        {
            get { return _seleteItem; }
            set
            {
                _seleteItem = value;
                OnPropertyChanged(nameof(SeleteItem));
            }
        }

        public RelayCommand AddFile
        {
            get { return _addFile; }

            set
            {
                _addFile = value;
                OnPropertyChanged(nameof(AddFile));
            }
        }

        public RelayCommand DeleteFile
        {
            get { return _deleteFile; }

            set
            {
                _deleteFile = value;
                OnPropertyChanged(nameof(DeleteFile));
            }
        }

        public RelayCommand Duplicate
        {
            get { return _duplicate; }

            set
            {
                _duplicate = value;
                OnPropertyChanged(nameof(Duplicate));
            }
        }
        #endregion

        #region 생성자

        public MainViewModels()
        {
            FilePath = new ObservableCollection<string>();
            FilePath.Clear();

            CreateCommand();
        }

        #endregion

        #region 함수

        void CreateCommand() 
        {
            AddFile = new RelayCommand(AddFileEventHandler);
            DeleteFile = new RelayCommand(DeleteFileEventHandler);
            Duplicate = new RelayCommand(DuplicateEventHandler);
        }

        #endregion

        #region 이벤트

        public event Action AddFileEvent;
        public event Action<string> DeleteFileEvent;
        public event Action DuplicateEvent;

        void AddFileEventHandler() { AddFileEvent?.Invoke(); }
        void DeleteFileEventHandler() { DeleteFileEvent?.Invoke(SeleteItem); }
        void DuplicateEventHandler() { DuplicateEvent?.Invoke(); }

        #endregion

    }
}
