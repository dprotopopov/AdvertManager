using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RT.Core;
using RT.Domain.Client.Annotations;

namespace RT.Domain.Models
{
    public class TreeLevel<T> : INotifyPropertyChanged where T : class, IEntity, new()
    {
        private ObservableCollection<TreeLevel<T>> _nextLevel;
        private T _current;
        

        public T Current
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<T> Neighborhood { get; set; }
        public ObservableCollection<TreeLevel<T>> NextLevel
        {
            get
            {
                return _nextLevel ?? (_nextLevel = new ObservableCollection<TreeLevel<T>>());
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
                private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}