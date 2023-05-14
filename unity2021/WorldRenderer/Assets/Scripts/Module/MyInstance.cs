

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.WorldRenderer.LIB.Proto;
using XTC.FMP.MOD.WorldRenderer.LIB.MVCS;
using UnityEngine.Rendering;

namespace XTC.FMP.MOD.WorldRenderer.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        public class Snapshot
        {
            public Material skybox;
            public Light sun;
            public AmbientMode ambientMode;
            public float ambientIntensity;
            public Color ambientSkyColor;
            public bool fogActive;
            public FogMode fogMode;
            public float fogDensity;
            public Color fogColor;
        }

        private Snapshot snapshot_ = null;

        public MyInstance(string _uid, string _style, MyConfig _config, MyCatalog _catalog, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _catalog, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        /// <remarks>
        /// 可用于加载主题目录的数据
        /// </remarks>
        public void HandleCreated()
        {
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        /// <remarks>
        /// 可用于加载内容目录的数据

        public void HandleOpened(string _source, string _uri)
        {
            rootUI.gameObject.SetActive(true);
            rootWorld.gameObject.SetActive(true);

            // 保存快照
            snapshot_ = new Snapshot();
            snapshot_.skybox = RenderSettings.skybox;
            //snapshot_.sun = RenderSettings.sun;
            snapshot_.ambientMode = RenderSettings.ambientMode;
            snapshot_.ambientIntensity = RenderSettings.ambientIntensity;
            snapshot_.ambientSkyColor = RenderSettings.ambientSkyColor;
            snapshot_.fogActive = RenderSettings.fog;
            snapshot_.fogMode = RenderSettings.fogMode;
            snapshot_.fogDensity = RenderSettings.fogDensity;
            snapshot_.fogColor = RenderSettings.fogColor;

            // 更改渲染器
            applySkybox();
            applyAmbient();
            applyFog();
            //RenderSettings.sun = null;
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
            rootWorld.gameObject.SetActive(false);

            // 恢复快照
            RenderSettings.skybox = snapshot_.skybox;
            //RenderSettings.sun = snapshot_.sun;
            RenderSettings.ambientMode = snapshot_.ambientMode;
            RenderSettings.ambientIntensity = snapshot_.ambientIntensity;
            RenderSettings.ambientSkyColor = snapshot_.ambientSkyColor;
            RenderSettings.fog = snapshot_.fogActive;
            RenderSettings.fogMode = snapshot_.fogMode;
            RenderSettings.fogDensity = snapshot_.fogDensity;
            RenderSettings.fogColor = snapshot_.fogColor;
        }

        private void applySkybox()
        {
            if ("DefaultSkybox" == style_.skybox.material)
                RenderSettings.skybox = rootAttachments.transform.Find("DefaultSkybox").GetComponent<MeshRenderer>().material;
            else
                RenderSettings.skybox = null;
            logger_.Debug("current skybox is {0}", RenderSettings.skybox);
        }

        private void applyAmbient()
        {
            if ("skybox" == style_.ambient.mode)
                RenderSettings.ambientMode = AmbientMode.Skybox;
            else
                RenderSettings.ambientMode = AmbientMode.Flat;
            logger_.Debug("current ambientMode is {0}", RenderSettings.ambientMode);

            Color skyboxColor;
            if (!ColorUtility.TryParseHtmlString(style_.ambient.skyColor, out skyboxColor))
                skyboxColor = Color.black;
            RenderSettings.ambientSkyColor = skyboxColor;
            logger_.Debug("current ambientSkyColor is {0}", RenderSettings.ambientSkyColor);

            RenderSettings.ambientIntensity = style_.ambient.intensity;
            logger_.Debug("current ambientIntensity is {0}", RenderSettings.ambientIntensity);
        }

        private void applyFog()
        {
            RenderSettings.fog = style_.fog.active;
            if ("Linear" == FogMode.Linear.ToString())
                RenderSettings.fogMode = FogMode.Linear;
            else if ("Exponential" == FogMode.Exponential.ToString())
                RenderSettings.fogMode = FogMode.Exponential;
            else if ("ExponentialSquared" == FogMode.ExponentialSquared.ToString())
                RenderSettings.fogMode = FogMode.ExponentialSquared;
            Color color;
            if (!ColorUtility.TryParseHtmlString(style_.fog.color, out color))
                color = Color.black;
            RenderSettings.fogColor = color;
        }
    }
}
