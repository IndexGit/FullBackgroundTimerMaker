using System;
using System.ComponentModel;
using System.Drawing;
using TimerClases.Objects.Interface;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Serialization;
using TimerClases.Objects.Enums;
using TimerClases.Objects.Helpers;
using TimerClases.Properties;

namespace TimerClases.Objects
{
    [Serializable]
    public class PeriodObject : IPeriodObject, INotifyPropertyChanged
    {

        private string _PeriodName;

        /// <summary>
        /// Получить фоновое изображение из файла
        /// </summary>
        /// <param name="ProjectImagePath"></param>
        /// <returns></returns>
        public Image GetBackGroundImage(string ProjectImagePath)
        {
            if (string.IsNullOrEmpty(BackGroundImage)) return null;
            using (var bmpTemp = new Bitmap(Path.Combine(ProjectImagePath,BackGroundImage)))
            {
                return new Bitmap(bmpTemp);
            }
        }

        [Required]
        public string PeriodName {
            get { return _PeriodName; }
            set
            {
                _PeriodName  = value;
                OnPropertyChanged(nameof(PeriodName));
            }
        }

        /// <summary>
        /// Обманка для сериализации TimeSpan
        /// </summary>
        public long m_PeriodDuration
        {
            get { return _PeriodDuration.Ticks; }
            set
            {
                _PeriodDuration = new TimeSpan(value);
            }
        }
        private TimeSpan _PeriodDuration = new TimeSpan(0,5,0);
        [XmlIgnore]
        public TimeSpan PeriodDuration {
            get { return _PeriodDuration; }
            set
            {
                _PeriodDuration  = value;
                OnPropertyChanged(nameof(PeriodDuration));
            }
        }
        private Color _TimerColor = Color.Black;
        [XmlElement(Type = typeof(XmlColor))]
        public Color TimerColor {
            get { return _TimerColor; }
            set
            {
                _TimerColor = value;
                OnPropertyChanged(nameof(TimerColor));
            }
        }
        private string _BackGroundImage;
        public string BackGroundImage {
            get { return _BackGroundImage; }
            set
            {
                _BackGroundImage = value;
                OnPropertyChanged(nameof(BackGroundImage));
            }
        }
        private int _Order;
        public int Order {
            get { return _Order; }
            set
            {
                _Order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        private EPosition _TimerPosition = EPosition.Center;

        public EPosition TimerPosition
        {
            get { return _TimerPosition; }
            set
            {
                _TimerPosition = value;
                OnPropertyChanged(nameof(TimerPosition));
            }
        }

        private readonly int _PeriodID;
        [Key, Display(AutoGenerateField = false)]
        public int PeriodID => _PeriodID;

        public static string PeriodIDIntCounterName = "PeriodObject_PeriodID";
        public static string OrderIntCounterName = "PeriodObject_Order";
        public PeriodObject()
        {
            _PeriodID = IntCounter.GetNextValue(PeriodIDIntCounterName);
            _Order = IntCounter.GetNextValue(OrderIntCounterName);

            _TimerFont = "Microsoft Sans Serif";
            _NameFont = "Microsoft Sans Serif";
        }

        private readonly BindingList<PeriodEvent> _PeriodEvents = new BindingList<PeriodEvent>();
        [DisplayName("События в периоде")]
        public BindingList<PeriodEvent> PeriodEvents => _PeriodEvents;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Взвести все события
        /// </summary>
        public void ResetEventsProceed()
        {
            foreach (var e in PeriodEvents)
            {
                e.Proceed = false;
            }

            DefaultChangePeriodSoundProceed = false;
        }

        /// <summary>
        /// Был ли проиган звук смены периода по-умолчанию
        /// </summary>
        [XmlIgnore]
        public bool DefaultChangePeriodSoundProceed;


        private Color _NameColor = Color.White;
        [XmlElement(Type = typeof(XmlColor))]
        public Color NameColor
        {
            get { return _NameColor; }
            set
            {
                _NameColor = value;
                OnPropertyChanged(nameof(NameColor));
            }
        }


        private EPosition _NamePosition = EPosition.TopRight;

        public EPosition NamePosition
        {
            get { return _NamePosition; }
            set
            {
                _NamePosition = value;
                OnPropertyChanged(nameof(NamePosition));
            }
        }

        private string _TimerFont;

        public string TimerFont
        {
            get
            {
                return _TimerFont;
            }
            set
            {
                _TimerFont = value;
                OnPropertyChanged(nameof(TimerFont));
            }
        }
        private string _NameFont;

        public string NameFont
        {
            get
            {
                return _NameFont;
            }
            set
            {
                _NameFont = value;
                OnPropertyChanged(nameof(NameFont));
            }
        }
    }
}
