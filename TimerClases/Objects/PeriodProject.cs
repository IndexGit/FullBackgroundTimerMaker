using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SevenZip;
using TimerClases.Objects.Helpers;
using TimerClases.Objects.Interface;

namespace TimerClases.Objects
{
    [Serializable]
    public class PeriodProject : IPeriodProject
    {
        private string _Name;

        [XmlElement("Name")]
        public string Name
        {
            get => _Name;
            set => _Name = value;
        }
        /// <summary>
        /// Список периодов в проекте
        /// </summary>
        private BindingList<PeriodObject> _PeriodObjects;
        public BindingList<PeriodObject> PeriodObjects {
            get => _PeriodObjects;
            set => _PeriodObjects = value;
        }

        [XmlIgnore]
        private bool IsModified;
        public PeriodProject()
        {
            PeriodObjects = new BindingList<PeriodObject>();
            ProjectLocalFile = GenerateProjectLocalFile();
        }

        [XmlIgnore]
        public bool Modified => IsModified;
        /// <summary>
        /// Пометить проект как изменённый
        /// </summary>
        /// <param name="SetModified"></param>
        public void SetModified(bool SetModified = true)
        {
            IsModified = SetModified;
        }


        /// <summary>
        /// Обманка для сериализации TimeSpan
        /// </summary>
        public long m_DefaultTimeOutTime
        {
            get => _defaultTimeOutTime.Ticks;
            set => _defaultTimeOutTime = new TimeSpan(value);
        }
        [XmlIgnore]
        private TimeSpan _defaultTimeOutTime = new TimeSpan(0,0,5,0);

        [XmlIgnore]
        public TimeSpan DefaultTimeOutTime
        {
            get => _defaultTimeOutTime;
            set => _defaultTimeOutTime = value;
        }

        private readonly BindingList<PeriodEvent> _timeOutEvents = new BindingList<PeriodEvent>();
        [DisplayName("События в таймауте")]
        public BindingList<PeriodEvent> TimeOutEvents => _timeOutEvents;

        /// <summary>
        /// Сериализация и архивация папки проекта в файл
        /// </summary>
        /// <param name="FileFullPath">Путь до создаваемого файла-архива с проектом</param>
        /// <returns></returns>
        public bool SaveToFile(string FileFullPath)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(PeriodProject));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, this);
                    var xml = sww.ToString();

                    if(File.Exists(ProjectLocalFile))
                        File.Delete(ProjectLocalFile);

                    if (!Directory.Exists(ProjectLocalFolder))
                        Directory.CreateDirectory(ProjectLocalFolder);

                    File.WriteAllText(ProjectLocalFile, xml,Encoding.Unicode);
                }
            }

            CleanUnusedFiles();

            ArchiveProject(ProjectLocalFolder, FileFullPath);

            return File.Exists(FileFullPath);

        }

        /// <summary>
        /// Разархивация файла с проектом в указанную временную папку
        /// </summary>
        /// <param name="FileFullPath">Путь с архивом проекта</param>
        /// <param name="ExctractPath">Путь до временной папки проекта куда разархивировать</param>
        private static void ExctractProjectFile(string FileFullPath, string ExctractPath)
        {

            string library_source = @"Dll\7z.dll"; //Путь к файлу 7zip.dll
            if (File.Exists(library_source))//Если библиотека 7zip существует
            {                
                SevenZipBase.SetLibraryPath(library_source); //Подгружаем библиотеку 7zip

                SevenZipExtractor sze = new SevenZipExtractor(FileFullPath);
                sze.ExtractArchive(ExctractPath);
            }
        }
        /// <summary>
        /// Архивируем временную папку проекта в файл
        /// </summary>
        /// <param name="ProjectTmpPath">Путь до временной папки с проектом</param>
        /// <param name="toFilePath">Путь до файла для сохранения проекта (архив)</param>
        private void ArchiveProject(string ProjectTmpPath, string toFilePath)
        {
            SevenZipBase.SetLibraryPath(@"Dll\7z.dll");

            SevenZipCompressor szc = new SevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.SevenZip,
                TempFolderPath = Path.GetTempPath(),
                CompressionMode = SevenZip.CompressionMode.Create
            };
            szc.CompressDirectory(ProjectTmpPath, toFilePath);
        }

        /// <summary>
        /// Загрузка проекта из файла (разархивация во временную папку)
        /// </summary>
        /// <param name="FileFullPath">Путь до файла архива с проектом</param>
        /// <returns></returns>
        public static PeriodProject LoadFromFile(string FileFullPath)
        {
            var tmpProjectLocalPath = GenerateProjectLocalPath();

            ExctractProjectFile(FileFullPath, tmpProjectLocalPath);

            var tmpProjectLocalFile = GenerateProjectLocalFile(tmpProjectLocalPath);

            var PP = DeserializeHelper.DeserializeXMLFileToObject<PeriodProject>(tmpProjectLocalFile);
            PP.ProjectLocalFile = tmpProjectLocalFile;
            PP.SetPeriodObjectsCounterValues();
            return PP;
        }
        
        /// <summary>
        /// Обработка параметров объекта после десериализации
        /// Установка счётчиков
        /// </summary>
        public void SetPeriodObjectsCounterValues()
        {
            var maxPeriodID = PeriodObjects.Count > 0 ? PeriodObjects.Max(o => o.PeriodID) : 0;
            IntCounter.SetValue(PeriodObject.PeriodIDIntCounterName, maxPeriodID);
            var maxOrder = PeriodObjects.Count > 0 ? PeriodObjects.Max(o => o.Order) : 0;
            IntCounter.SetValue(PeriodObject.OrderIntCounterName, maxOrder);
        }


        /// <summary>
        /// Путь до сериализованного файла проекта во временной папке
        /// </summary>
        public string ProjectLocalFile { get; set; }
        /// <summary>
        /// Путь до папки проекта во временной папке
        /// </summary>
        public string ProjectLocalFolder => (new FileInfo(ProjectLocalFile)).Directory?.FullName;
        /// <summary>
        /// Путь до папки фоновых изображений периодов проекта во временной папке
        /// </summary>
        public string ProjectLocalImageFolder => Path.Combine(ProjectLocalFolder, "PeriodImages");
        /// <summary>
        /// Путь до папки изображений событий периодов во временной папке
        /// </summary>
        public string ProjectLocalEventImageFolder => Path.Combine(ProjectLocalFolder, "PeriodEventImages");
        /// <summary>
        /// Путь до папки аудио файлов событий периодов во временной папке
        /// </summary>
        public string ProjectLocalEventSoundFolder => Path.Combine(ProjectLocalFolder, "PeriodEventSounds");
        /// <summary>
        /// Путь до папки аудио файлов по-умолчанию во временной папке
        /// </summary>
        public string ProjectLocalSoundFolder => Path.Combine(ProjectLocalFolder, "PeriodSounds");
        /// <summary>
        /// Путь до временной папки всех проектов
        /// </summary>
        public static string ProjectLocalPath => Path.Combine(Path.GetTempPath(), "CprocTimer", "Projects");
        /// <summary>
        /// Сгенерировать путь до папки проекта во временной папке
        /// </summary>
        /// <returns></returns>
        /// 



        /// <summary>
        /// Путь до папки изображений TimeOut во временной папке
        /// </summary>
        public string ProjectLocalTimeOutImageFolder => Path.Combine(ProjectLocalFolder, "TimeOutEventImages");
        /// <summary>
        /// Путь до папки аудио файлов TimeOut во временной папке
        /// </summary>
        public string ProjectLocalTimeOutSoundFolder => Path.Combine(ProjectLocalFolder, "TimeOutEventSounds");

        private static string GenerateProjectLocalPath()
        {
            var tmpFullPathFile = Path.Combine(ProjectLocalPath, Guid.NewGuid().ToString("N"));

            return tmpFullPathFile;
        }
        /// <summary>
        /// Имя файла проекта для сериализации
        /// </summary>
        private const string ProjectFileName = "PeriodProject.xml";

        /// <summary>
        /// Получить путь до временной папки проекта
        /// </summary>
        /// <param name="folder">UID папки проекта, если пустой, то сгенерится</param>
        /// <returns></returns>
        private static string GenerateProjectLocalFile(string folder = "")
        {
            if (folder == "") folder = GenerateProjectLocalPath();
            var tmpFullPathFile = Path.Combine(folder, ProjectFileName);

            return tmpFullPathFile;
        }
        #region Move between PeriodObjects

        private int position = 0;
        public bool MoveNext()
        {
            position++;
            if (position >= PeriodObjects.Count)
            {
                position = PeriodObjects.Count-1;
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool MoveBack()
        {
            position--;
            if (position < 0)
            {
                position = 0;
                return false;
            }
            else
            {
                return true;
            }
        }
        public void Add(PeriodObject po)
        {
            PeriodObjects.Add(po);
        }

        public void Reset()
        {
            position = 0;
        }

        public PeriodObject Current
        {
            get
            {
                try
                {
                    return PeriodObjects[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Есть ли следующий период
        /// </summary>
        public bool HasNextPeriod => position < PeriodObjects.Count-1;
        /// <summary>
        /// Есть ли предыдущий период
        /// </summary>
        public bool HasPrevPeriod => position > 0;
        #endregion
        /// <summary>
        /// Удаляем не использованные файлы в проекте для уменьшения его размера
        /// </summary>
        public void CleanUnusedFiles()
        {
            foreach (var filePath in Directory.GetFiles(ProjectLocalFolder, "*.*", SearchOption.AllDirectories))
            {
                var file = new FileInfo(filePath);
                var fileName = file.Name;
                if (!file.Exists ||
                    string.Equals(fileName, ProjectFileName, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                // Это звук по-умолчанию для смены периодов
                var isDefSound = string.Equals(fileName, DefaultSoundChangePeriod,
                    StringComparison.InvariantCultureIgnoreCase);
                if(isDefSound) continue;                

                // Это фон
                var isBgImg = PeriodObjects.Any(
                    p => string.Equals(p.BackGroundImage, fileName, StringComparison.InvariantCultureIgnoreCase));

                if (isBgImg) continue;
                
                // Это файл эвента
                var isEventFile =
                    PeriodObjects.Any(
                        p =>
                            p.PeriodEvents.Any(
                                e =>
                                    string.Equals(e.ObjectPath, fileName,
                                        StringComparison.InvariantCultureIgnoreCase)));

                if(isEventFile) continue;

                // Это файл эвента таймаута
                var isTimeOutEventFile = TimeOutEvents.Any(e => string.Equals(e.ObjectPath, fileName,
                    StringComparison.InvariantCultureIgnoreCase));

                if(isTimeOutEventFile) continue;

                file.Delete();                
            }
            
        }

        private string _DefaultSoundChangePeriod;

        public string DefaultSoundChangePeriod
        {
            get => _DefaultSoundChangePeriod??"";
            set => _DefaultSoundChangePeriod = value;
        }

        private string GetDefaultSoundChangePeriod()
        {
            return Path.Combine(ProjectLocalSoundFolder, DefaultSoundChangePeriod);
        }
        /// <summary>
        /// Играть звук смены периодов по умолчанию
        /// </summary>
        public void PlayDefaultSoundChangePeriod()
        {
            if(!string.IsNullOrEmpty(DefaultSoundChangePeriod))
                WindowsMediaPlayerHelper.PlayAsync(GetDefaultSoundChangePeriod());
        }

        private int _DefaultNameFontSize = 100;

        private int _DefaultTimerFontSize = 100;

        public int DefaultNameFontSize
        {
            get => _DefaultNameFontSize;
            set => _DefaultNameFontSize = value;
        }

        public int DefaultTimerFontSize
        {
            get => _DefaultTimerFontSize;
            set => _DefaultTimerFontSize = value;
        }

        /// <summary>
        /// Сброс событий Таймаута
        /// </summary>
        public void ResetTimeOutEvents()
        {
            foreach (var e in TimeOutEvents)
            {
                e.Proceed = false;                
            }
        }
    }
}
