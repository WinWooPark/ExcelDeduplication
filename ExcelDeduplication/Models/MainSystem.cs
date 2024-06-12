
using ExcelDeduplication.ViewModels;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace ExcelDeduplication.Models
{
    public class MainSystem
    {
        #region 변수
        const string _dirPath = "D:\\NewExcel\\";

        List<string> _filePath;

        List<List<CellData>> _allFiles;

        MainViewModels _mainViewModels;
        #endregion

        #region 생성자
        public MainSystem(MainViewModels mainViewModels)
        {
            _mainViewModels = mainViewModels;
            _filePath = new List<string>();
            _allFiles = new List<List<CellData>>();

            CreateEvent();
        }
        #endregion

        #region 함수
        public void AddFilePath() 
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                foreach (var fileName in dialog.FileNames)
                    _mainViewModels.FilePath.Add(fileName);
            }
        }

        public void DeleteFilePath(string path)
        {
            _mainViewModels.FilePath.Remove(path);
        }

        public void StartDuplicate()
        {
            if (_mainViewModels.FilePath == null || _mainViewModels.FilePath.Count == 0) 
            {
                MessageBox.Show("파일의 경로가 존제하지 않습니다.");
                return;
            }

            _allFiles.Clear();

            foreach (var path in _mainViewModels.FilePath)
                _allFiles.Add(ParseCsv(path));

            foreach (var file in _allFiles) 
            {
                foreach (var Nextfile in _allFiles)
                {
                    if (Nextfile.Equals(file)) continue;

                    List<CellData> duplicates = FindDuplicates(file, Nextfile);

                    foreach (var delete in duplicates)
                         Nextfile.Remove(delete);
                }

                SaveToCsv(file);

            }

            MessageBox.Show("완료");
        }

        List<CellData> FindDuplicates(List<CellData> Left, List<CellData> Right) 
        {
            var duplicates = new List <CellData>();
            foreach (var LeftItem in Left) 
            {
                foreach (var RightItem in Right)
                {
                    if(LeftItem.PhoneNum == RightItem.PhoneNum) 
                        duplicates.Add(RightItem);
                }
            }

            return duplicates;
        }

        public List<CellData> ParseCsv(string filePath)
        {
            var cellDatas = new List<CellData>();

            try
            {
                //파일의 모든 줄을 읽습니다.
                var lines = System.IO.File.ReadAllText(filePath, Encoding.UTF8);
                string[] stringLine = lines.Split('\n'); // 개행 문자로 분할하여 각 줄을 얻음

                //첫 번째 줄은 헤더이므로 건너뜁니다.
                for (int i = 1; i < stringLine.Length - 1; i++)
                {
                    var line = stringLine[i];
                    var values = line.Split(',');


                    // Person 객체를 생성하고 리스트에 추가합니다.
                    if(values[1] == "\r") continue;

                    var cellData = new CellData(filePath, values[0], values[1]);
                    cellDatas.Add(cellData);
                }
            }
            catch(Exception e) 
            {
                return null;
            }
            

            return cellDatas;
        }

        void SaveToCsv(List<CellData> cellDatas)
        {
            if (cellDatas == null || cellDatas.Count == 0) return;
            string FileName = Path.GetFileName(cellDatas[0].Path);
            FileName = Path.ChangeExtension(FileName, "csv");

            string newPath = string.Format("{0}{1}", _dirPath, FileName);

            if(!Directory.Exists(_dirPath)) Directory.CreateDirectory(_dirPath);

            try
            {
                using (StreamWriter writer = new StreamWriter(newPath, false, System.Text.Encoding.UTF8))
                {
                    foreach (var cellData in cellDatas)
                    {
                        // 속성 값에서 개행 문자 제거
                        string name = cellData?.Name?.Replace("\r", "").Replace("\n", "");
                        string phoneNum = cellData?.PhoneNum?.Replace("\r", "").Replace("\n", "");

                        // CSV 한 줄 작성
                        string line = string.Format("{0},{1}", name, phoneNum);
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex) 
            {
                return;
            }
            
        }

        void CreateEvent() 
        {
            _mainViewModels.AddFileEvent += AddFilePath;
            _mainViewModels.DeleteFileEvent += DeleteFilePath;
            _mainViewModels.DuplicateEvent += StartDuplicate;
        }
        void DeleteEvent()
        {
            _mainViewModels.AddFileEvent -= AddFilePath;
            _mainViewModels.DeleteFileEvent -= DeleteFilePath;
            _mainViewModels.DuplicateEvent -= StartDuplicate;
        }
        #endregion
    }
}
