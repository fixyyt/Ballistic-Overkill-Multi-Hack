using UnityEngine;
using System.Threading;
using Aquiris.Ballistic.Game.Character.PlayerBehaviour;
using System;

namespace Loader.cs
{
    internal class hacks : MonoBehaviour
    {

        private bool ShowMenu = true;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                ShowMenu = !ShowMenu;


            var localPlayer = LocalCharacter.Instance;
            localPlayer.currentLoadout.HeroLevel = 17;
            //localPlayer.currentWeapon.shootController.shootData.damagePerShot = Options.DamagePerShot / 100.0f;
            

            //infinite ammo

            localPlayer.currentWeapon.shootController.shootData.magazineSize = 99999;

            //no recoil

            localPlayer.currentWeapon.aimController.aimData.aimingRecoilPercent = 0.0f;
            localPlayer.currentWeapon.aimController.aimData.crouchRecoilPercent = 0.0f;
            localPlayer.currentWeapon.aimController.aimData.recoilSpeed = 0.0f;
            localPlayer.currentWeapon.aimController.aimData.recoil = 0.0f;
            localPlayer.currentWeapon.aimController.aimData.upRecoil = 0.0f;
            

            //no spread

            localPlayer.currentWeapon.aimController.aimData.precision = 100000.0f;
            localPlayer.currentWeapon.aimController.aimData.aimCrouchPrecision = 100000.0f;
            localPlayer.currentWeapon.aimController.aimData.aimPrecision = 100000.0f;
            localPlayer.currentWeapon.aimController.aimData.crouchPrecision = 100000.0f;
            localPlayer.currentWeapon.aimController.aimData.movePrecisionLossSpeed = 0.0f;
            localPlayer.currentWeapon.aimController.aimData.movePrecisionRecoverySpeed = 1000.0f;
            localPlayer.currentWeapon.aimController.aimData.shootPrecisionLossSpeed = 0.0f;
            localPlayer.currentWeapon.aimController.aimData.SprintAccuracyLoss = 0.0f;
            


            //instant reload

            localPlayer.currentWeapon.animationData1st.ReloadDuration = 0.01f;

            //rate of fire mode

            localPlayer.currentWeapon.shootController.shootData.fireRate = 1.0f; // / Options.RateOfFire;

            //instant bolt action

            localPlayer.currentWeapon.animationData1st.BoltActionDuration = 0.01f;

            //full auto weapons

            //localPlayer.currentWeapon.aimController.shootController.shootData.shootMode = ShootMode.SEMI_AUTOMATIC;


            if (Input.GetKeyUp(KeyCode.Delete))
            {
                Loader.Unload();
            }
        }
        public void OnGUI()
        {
            var local = LocalCharacter.Instance;

            if (!local) return;

            Renderer.Begin();
            foreach (FPSCharacter fpsPlayer in NetworkCharacterList.Instance.GetAllConnectedCharacters())
            {
                if (!local.isDead && fpsPlayer.gameClient.spawned && !fpsPlayer.gameClient.isMe)
                {

                    var screenHead = Aquiris.Ballistic.Game.Utility.CameraUtils.SafeWorldToScreenPoint(local.StageCamera, fpsPlayer.HeadPivot.position);
                    var screenFeet = Aquiris.Ballistic.Game.Utility.CameraUtils.SafeWorldToScreenPoint(local.StageCamera, fpsPlayer.transform.position);

                    float height = Math.Abs(screenFeet.y - screenHead.y);
                    float width = height * 0.6f;

                    var rect = new Rect((screenFeet.x - width / 2) / Screen.width, screenFeet.y / Screen.height, width / Screen.width, height / Screen.height);

                    if (fpsPlayer.IsEnemy)
                    {
                        fpsPlayer.animatedComponent.transform.localScale = new Vector3(1f, 1f, 1f);
                        Color clr = local.IsCharacterVisible(fpsPlayer) ? Color.yellow : Color.red;
                        Renderer.RenderQuad(rect, clr);
                        Renderer.RenderLine(new Vector2(0.5f, 0.0f), new Vector2((screenFeet.x) / Screen.width, screenFeet.y / Screen.height), clr);
                    }
                    /*else
                    {
                        Renderer.RenderQuad(rect, Color.blue);
                        Renderer.RenderLine(new Vector2(0.5f, 0.0f), new Vector2((screenFeet.x) / Screen.width, screenFeet.y / Screen.height), Color.blue);
                    }*/
                }
            }
            Renderer.End();
        }
    }
}
