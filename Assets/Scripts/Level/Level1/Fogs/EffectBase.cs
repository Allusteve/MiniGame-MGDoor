using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class datas 
    {
        private static datas nongdu;

        public float nong=2f;

        public static datas GetNongdu()
        {
            if (nongdu == null)
             {
                nongdu = new datas();
             }
            
            return nongdu;

            //datas.GetNongdu().nong;
        }
    }


    public class EffectBase : MonoBehaviour
    {
        public static Dictionary<string, RenderTexture> AlreadyRendered = new Dictionary<string, RenderTexture>();
        public float nongdu;
        private static bool _insiderendering = false;
        public static bool InsideRendering
        {
            get
            {
                return _insiderendering;
            }
            set
            {
                _insiderendering = value;
            }
        }
    }
}