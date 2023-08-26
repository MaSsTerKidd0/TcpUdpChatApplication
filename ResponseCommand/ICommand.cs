using ChatApp.Models;
using System.Collections.ObjectModel;

namespace ChatApp.ResponseCommand
{
    public interface ICommand
    {
        void Execute(ObservableCollection<InterfaceExample> T);
    }
}
