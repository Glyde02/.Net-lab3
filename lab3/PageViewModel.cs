using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace WPF
{
    public class PageViewModel : INotifyPropertyChanged
    {
        private OpenFile openSelectAssemblyDialog;
        public List<Record> Data { get; private set; }

        public OpenFile OpenSelectAssemblyDialog
        {
            get
            {
                return openSelectAssemblyDialog ??= new OpenFile(obj =>
                {
                    var openFileDialog = new OpenFileDialog();

                    if (openFileDialog.ShowDialog() ?? false)
                    {
                        Data = new List<Node>();
                        Data.Add(AssemblyBrowser.Browse(openFileDialog.FileName));
                        OnPropertyChanged(nameof(Data));
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PageViewModel()
        {
            Data = new();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
