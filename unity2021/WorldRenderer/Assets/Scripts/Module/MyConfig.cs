
using System.Xml.Serialization;

namespace XTC.FMP.MOD.WorldRenderer.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class Skybox
        {
            [XmlAttribute("material")]
            public string material { get; set; } = "none";
        }

        public class Ambient
        {
            [XmlAttribute("mode")]
            public string mode { get; set; } = "flat";

            [XmlAttribute("intensity")]
            public float intensity { get; set; } = 0f;

            [XmlAttribute("skyColor")]
            public string skyColor { get; set; } = "#000000";
        }

        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";

            [XmlElement("Skybox")]
            public Skybox skybox { get; set; } = new Skybox();
            [XmlElement("Ambient")]
            public Ambient ambient { get; set; } = new Ambient();
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

