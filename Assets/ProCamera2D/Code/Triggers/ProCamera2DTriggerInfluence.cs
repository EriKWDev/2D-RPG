﻿using UnityEngine;
using System.Collections;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class ProCamera2DTriggerInfluence : BaseTrigger
    {
        public static string TriggerName = "Influence Trigger";

        public Transform FocusPoint;

        public float InfluenceSmoothness = .3f;

        [RangeAttribute(0, 1)]
        public float ExclusiveInfluencePercentage = .25f;

        Vector2 _influence;
        Vector2 _velocity;
        Vector3 _exclusivePointVelocity;

        Vector3 _tempExclusivePoint;

        void Start()
        {
            if (FocusPoint == null)
                FocusPoint = transform.Find("FocusPoint");
            if (FocusPoint == null)
                FocusPoint = transform;
        }

        protected override void EnteredTrigger()
        {
            base.EnteredTrigger();

            StartCoroutine(InsideTriggerRoutine());
        }

        protected override void ExitedTrigger()
        {
            base.ExitedTrigger();

            StartCoroutine(OutsideTriggerRoutine());
        }

        IEnumerator InsideTriggerRoutine()
        {
            yield return ProCamera2D.GetYield();

            _tempExclusivePoint = VectorHV(Vector3H(ProCamera2D.CameraTargetPosition), Vector3V(ProCamera2D.CameraTargetPosition));
            while (_insideTrigger)
            {
                _exclusiveInfluencePercentage = ExclusiveInfluencePercentage;

                var distancePercentage = GetDistanceToCenterPercentage(new Vector2(Vector3H(ProCamera2D.TargetsMidPoint), Vector3V(ProCamera2D.TargetsMidPoint)));
                var vectorFromPointToFocus = new Vector2(Vector3H(ProCamera2D.TargetsMidPoint) + Vector3H(ProCamera2D.TargetsMidPoint) - Vector3H(ProCamera2D.PreviousTargetsMidPoint), Vector3V(ProCamera2D.TargetsMidPoint) + Vector3V(ProCamera2D.TargetsMidPoint) - Vector3V(ProCamera2D.PreviousTargetsMidPoint)) - new Vector2(Vector3H(FocusPoint.position), Vector3V(FocusPoint.position));
                if (distancePercentage == 0)
                {
                    ProCamera2D.ExclusiveTargetPosition = Vector3.SmoothDamp(_tempExclusivePoint, VectorHV(Vector3H(FocusPoint.position), Vector3V(FocusPoint.position)), ref _exclusivePointVelocity, InfluenceSmoothness);
                    _tempExclusivePoint = ProCamera2D.ExclusiveTargetPosition.Value;
                    _influence = -vectorFromPointToFocus * (1 - distancePercentage);
                    ProCamera2D.ApplyInfluence(_influence);
                }
                else
                {
                    _influence = Vector2.SmoothDamp(_influence, -vectorFromPointToFocus * (1 - distancePercentage), ref _velocity, InfluenceSmoothness);
                    ProCamera2D.ApplyInfluence(_influence);
                    _tempExclusivePoint = VectorHV(Vector3H(ProCamera2D.CameraTargetPosition), Vector3V(ProCamera2D.CameraTargetPosition));
                }

                yield return ProCamera2D.GetYield();
            }
        }

        IEnumerator OutsideTriggerRoutine()
        {
            yield return ProCamera2D.GetYield();

            while (!_insideTrigger && _influence != Vector2.zero)
            {
                _influence = Vector2.SmoothDamp(_influence, Vector2.zero, ref _velocity, InfluenceSmoothness);
                ProCamera2D.ApplyInfluence(_influence);

                yield return ProCamera2D.GetYield();
            }
        }

        #if UNITY_EDITOR
        override protected void DrawGizmos()
        {
            _exclusiveInfluencePercentage = ExclusiveInfluencePercentage;

            base.DrawGizmos();

            float cameraDepthOffset = Vector3D(ProCamera2D.transform.localPosition) + Mathf.Abs(Vector3D(ProCamera2D.transform.localPosition)) * Vector3D(ProCamera2D.transform.forward);

            if (FocusPoint != null)
            {
                if (FocusPoint.position != Vector3.zero)
                    Gizmos.DrawLine(VectorHVD(Vector3H(transform.position), Vector3V(transform.position), cameraDepthOffset), VectorHVD(Vector3H(FocusPoint.position), Vector3V(FocusPoint.position), cameraDepthOffset));

                Gizmos.DrawIcon(VectorHVD(Vector3H(FocusPoint.position), Vector3V(FocusPoint.position), cameraDepthOffset), "ProCamera2D/gizmo_icon_influence.png", false);
            }
            else
                Gizmos.DrawIcon(VectorHVD(Vector3H(transform.position), Vector3V(transform.position), cameraDepthOffset), "ProCamera2D/gizmo_icon_influence.png", false);
        }
        #endif
    }
}