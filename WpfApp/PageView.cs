using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AssemblyBrowser;
using Microsoft.Win32;

namespace WpfApp
{
    public class PageView : INotifyPropertyChanged
    {
        public string OpenedFileName { get; set; } = "";
        public List<Record> Data { get; private set; }
        public ICommand Openfile => new OpenFile(StartAssembly);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StartAssembly()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Assemblies|*.dll;*.exe",
                Title = "Select assembly",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog().Value)
            {

                OpenedFileName = openFileDialog.FileName;
                OnPropertyChanged(nameof(OpenedFileName));

                Data = new List<Record>();
                Data.Add(AssemblyBrowsr.BrowseFile(OpenedFileName));
                
                OnPropertyChanged(nameof(Data));
            }
        }
    }
}
