using UnityEngine;
using System.Collections;
using System;
using FRL.IO;
using UnityEngine.UI;

namespace FRL.IO.FourD
{
    public class ViveSliderInput : GlobalReceiver, IGlobalTriggerPressSetHandler, IGlobalTouchpadPressSetHandler
    {
        public Slider XY, YZ;
        Vector3 pos;

        void IGlobalTouchpadPressHandler.OnGlobalTouchpadPress(ViveControllerModule.EventData eventData)
        {
            module = eventData.module;
            Vector3 tmppos = module.transform.position;
        }

        void IGlobalTouchpadPressDownHandler.OnGlobalTouchpadPressDown(ViveControllerModule.EventData eventData)
        {
            module = eventData.module;
            pos = module.transform.position;
        }

        void IGlobalTouchpadPressUpHandler.OnGlobalTouchpadPressUp(ViveControllerModule.EventData eventData)
        {
            throw new NotImplementedException();
        }

        void IGlobalTriggerPressHandler.OnGlobalTriggerPress(ViveControllerModule.EventData eventData)
        {
            throw new NotImplementedException();
        }

        void IGlobalTriggerPressDownHandler.OnGlobalTriggerPressDown(ViveControllerModule.EventData eventData)
        {
            throw new NotImplementedException();
        }

        void IGlobalTriggerPressUpHandler.OnGlobalTriggerPressUp(ViveControllerModule.EventData eventData)
        {
            throw new NotImplementedException();
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
