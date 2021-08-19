using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TimerClases.Objects.Interface
{
    public interface IPeriodProject
    {
        [Required]
        string Name { get; set; }
        string ProjectLocalFile { get; set; }

        BindingList<PeriodObject> PeriodObjects { get; }
        [XmlIgnore]
        bool Modified { get; }

        string DefaultSoundChangePeriod { get; set; }

        int DefaultNameFontSize { get; set; }
        int DefaultTimerFontSize { get; set; }

        void SetModified(bool SetModified = true);

        bool SaveToFile(string FileFullPath);

    }
}
