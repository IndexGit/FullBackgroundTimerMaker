using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using TimerClases.Objects.Enums;
using TimerClases.Objects.Helpers;
using TimerClases.Objects.Interface;
using TimerClases.Properties;

namespace TimerClases.Objects
{
    [Serializable]
    public class PeriodEvent : IPeriodEvent, INotifyPropertyChanged
    {

        public Image GetImage(string ProjectEventImagePath)
        {
            if (string.IsNullOrEmpty(ProjectEventImagePath)) return null;
            using (var bmpTemp = new Bitmap(Path.Combine(ProjectEventImagePath, ObjectPath)))
            {
                return new Bitmap(bmpTemp);
            }
        }

        public string GetSoundPath(string ProjectEventSoundPath)
        {
            return Path.Combine(ProjectEventSoundPath, ObjectPath);
        }

        public void PlaySound(string ProjectLocalEventSoundFolder)
        {

            WindowsMediaPlayerHelper.PlayAsync(GetSoundPath(ProjectLocalEventSoundFolder));
        }

        private string _ObjectPath;

        public string ObjectPath
        {
            get { return _ObjectPath; }
            set
            {
                _ObjectPath = value;
                OnPropertyChanged(nameof(ObjectPath));
            }
        }

        /// <summary>
        /// Обманка для сериализации TimeSpan
        /// </summary>
        public long m_ObjectStartTime
        {
            get { return _ObjectStartTime.Ticks; }
            set
            {
                _ObjectStartTime = new TimeSpan(value);
            }
        }

        private TimeSpan _ObjectStartTime = new TimeSpan(0);
        [XmlIgnore]
        public TimeSpan ObjectStartTime
        {
            get { return _ObjectStartTime; }
            set
            {
                _ObjectStartTime = value;
                OnPropertyChanged(nameof(ObjectStartTime));
            }
        }

        public EPeriodEventType _PeriodEventType;

        public EPeriodEventType PeriodEventType
        {
            get { return _PeriodEventType; }
            set
            {
                _PeriodEventType = value;
                OnPropertyChanged(nameof(PeriodEventType));
            }
        }

        private EPeriodEventStartType _PeriodEventStartType;

        public EPeriodEventStartType PeriodEventStartType
        {
            get { return _PeriodEventStartType; }
            set
            {
                _PeriodEventStartType = value;
                OnPropertyChanged(nameof(PeriodEventStartType));
            }
        }

        public PeriodEvent()
        {
            _PeriodEventType = EPeriodEventType.Sound;
            _PeriodEventStartType = EPeriodEventStartType.AfterStart;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [XmlIgnore]
        public bool Proceed { get; set; }
    }
}
