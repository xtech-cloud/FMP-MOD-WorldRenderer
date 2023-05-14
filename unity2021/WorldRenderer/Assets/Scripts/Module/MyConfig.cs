
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

        public class Fog
        {
            [XmlAttribute("active")]
            public bool active { get; set; } = false;
            [XmlAttribute("mode")]
            public string mode { get; set; } = "";

            [XmlAttribute("density")]
            public float intensity { get; set; } = 0f;

            [XmlAttribute("color")]
            public string color { get; set; } = "#000000";
        }

        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";

            [XmlElement("Skybox")]
            public Skybox skybox { get; set; } = new Skybox();
            [XmlElement("Ambient")]
            public Ambient ambient { get; set; } = new Ambient();
            [XmlElement("Fog")]
            public Fog fog { get; set; } = new Fog();
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}

