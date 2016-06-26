﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.IO;

/*
Source code copyrighgt 2015, by Michael Billard (Angel-125)
License: GNU General Public License Version 3
License URL: http://www.gnu.org/licenses/
If you want to use this code, give me a shout on the KSP forums! :)
Wild Blue Industries is trademarked by Michael Billard and may be used for non-commercial purposes. All other rights reserved.
Note that Wild Blue Industries is a ficticious entity 
created for entertainment purposes. It is in no way meant to represent a real entity.
Any similarity to a real entity is purely coincidental.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace WildBlueIndustries
{
    class AsteroidScannerInfo : Window<AsteroidScannerInfo>
    {
        public ModuleAsteroid asteroid = null;
        public ModuleAsteroidInfo asteroidInfo = null;
        public Part potato = null;

        Vector2 scrollPosition =  new Vector2();
        List<ModuleAsteroidResource> astroResources = new List<ModuleAsteroidResource>();

        public AsteroidScannerInfo() :
            base("Asteroid Analysis", 300, 400)
        {
            Resizable = false;
        }

        public override void SetVisible(bool newValue)
        {
            base.SetVisible(newValue);
            astroResources.Clear();
            astroResources = this.potato.FindModulesImplementing<ModuleAsteroidResource>();
        }

        protected override void DrawWindowContents(int windowId)
        {
            GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter;

            string resourceString = null;
            if (asteroid == null || asteroidInfo == null)
                return;

            GUILayout.BeginVertical();
            GUILayout.BeginScrollView(new Vector2(0, 0), new GUILayoutOption[]{GUILayout.Height(120)});
            GUILayout.Label("<color=white><b>Name: </b>" + asteroid.AsteroidName + "</color>");
            GUILayout.Label(string.Format("<color=white><b>Density: </b>{0:f2}t/L</color>", asteroid.density));
            GUILayout.Label("<color=white><b>Mass: </b>" + asteroidInfo.displayMass + "</color>");
            GUILayout.Label("<color=white><b>Resources Mass: </b>" + asteroidInfo.resources + "</color>");
            GUILayout.EndScrollView();

            GUILayout.Label("<color=white><b>Resource Composition</b></color>", centeredStyle);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            foreach (ModuleAsteroidResource resource in astroResources)
            {
                if (resource.abundance > 0.0f)
                {
                    resourceString = string.Format("{0:f2}%", resource.displayAbundance * 100f);
                    GUILayout.Label("<color=white><b>" + resource.resourceName + ": </b> " + resourceString + "</color>");
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
        }
    }
}