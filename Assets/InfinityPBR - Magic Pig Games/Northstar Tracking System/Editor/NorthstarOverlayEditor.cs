using System.Linq;
using UnityEditor;
using UnityEngine;
using static InfinityPBR.InfinityEditor;
using static MagicPigGames.Northstar.NorthstarEditorUtilities;

namespace MagicPigGames.Northstar
{
    [CustomEditor(typeof(NorthstarOverlay))]
    public class NorthstarOverlayEditor : Editor
    {
        NorthstarOverlay Target => (NorthstarOverlay)target;
        
        private void OnEnable()
        {
            if (Target.overlayIconPrefab == null)
                Target.overlayIconPrefab = FindDefaultScreenOverlayPrefab();
            if (Target.targetCamera == null)
                Target.targetCamera = FindFirstEnabledCamera();
        }
        
        public override void OnInspectorGUI()
        {
            HeaderAndIntro();
            RuntimeData();
            GreyLine();

            Undo.RecordObject(Target, "Screen Required Settings");
            RequiredSettings();
            
            EditorUtility.SetDirty(Target);
        }
        
        private void OptionalSettings()
        {
            Header1("Options");
            StartRow();
            Label($"Screen Distance {symbolInfo}", "For screen icons. This is the min and max distance with which objects can be tracked. Less than the " +
                                                   "minimum, or greater than the maximum, the icons will simply turn off.\n\nUse the Opacity or" +
                                                   " Size curves to gently remove them from the scene by fading out, or reducing " +
                                                   "their size.\n\nOften the min/max values for Screen and Edge will be the same!", 150);
            Label("Min", 30);
            Target.screenDistanceMin = DelayedFloat(Target.screenDistanceMin, 50);
            if (Target.screenDistanceMin < 0)
                Target.screenDistanceMin = 0;
            if (Target.screenDistanceMin > Target.screenDistanceMax)
                Target.screenDistanceMin = Target.screenDistanceMax;
            Gap();
            Label("Max", 30);
            Target.screenDistanceMax = DelayedFloat(Target.screenDistanceMax, 50);
            if (Target.screenDistanceMax < Target.screenDistanceMin)
                Target.screenDistanceMax = Target.screenDistanceMin;
            EndRow();
            
            StartRow();
            Label($"Edge Distance {symbolInfo}", "For edge icons. This is the min and max distance with which objects can be tracked. Less than the " +
                                                   "minimum, or greater than the maximum, the icons will simply turn off.\n\nUse the Opacity or" +
                                                   " Size curves to gently remove them from the scene by fading out, or reducing " +
                                                   "their size.\n\nOften the min/max values for Screen and Edge will be the same!", 150);
            Label("Min", 30);
            Target.edgeDistanceMin = DelayedFloat(Target.edgeDistanceMin, 50);
            if (Target.edgeDistanceMin < 0)
                Target.edgeDistanceMin = 0;
            if (Target.edgeDistanceMin > Target.edgeDistanceMax)
                Target.edgeDistanceMin = Target.edgeDistanceMax;
            Gap();
            Label("Max", 30);
            Target.edgeDistanceMax = DelayedFloat(Target.edgeDistanceMax, 50);
            if (Target.edgeDistanceMax < Target.edgeDistanceMin)
                Target.edgeDistanceMax = Target.edgeDistanceMin;
            if (Button($"{symbolCarrotLeft} Screen Distance", 125))
            {
                Target.edgeDistanceMax = Target.screenDistanceMax;
                Target.edgeDistanceMin = Target.screenDistanceMin;
            }
            EndRow();
        }

        private void RequiredSettings()
        {
            Header2("Required");
            StartRow();
            Label($"Camera", 150);
            Target.targetCamera = Object(Target.targetCamera, typeof(Camera), 200, true) as Camera;
            if (Target.targetCamera == null)
                Label($"<i>{textError}Can not be null!{textColorEnd}</i>", false, false, true);
            EndRow();
            
            StartRow();
            Label($"Overlay Icon {symbolInfo}", "This is the on-screen icon prefabs that will be used. Generally they " +
                                                         "should remain set to the default values, unless you are " +
                                                         "specifically overriding them (and you know what you are doing!).\n\n" +
                                                         "Please do not remove or rename \"Northstar Overlay Icon\"!", 150);
            Target.overlayIconPrefab = Object(Target.overlayIconPrefab, typeof(GameObject), 200, false) as GameObject;
            if (Target.overlayIconPrefab == null)
                Label($"<i>{textError}Can not be null!{textColorEnd}</i>", false, false, true);
            EndRow();
            
            StartRow();
            Label($"Overlay Settings {symbolInfo}", "This contains the default options for this overlay. Individual " +
                                                              "Tracked Target objects can override specific values.", 150);
            Target.northstarOverlaySettings = Object(Target.northstarOverlaySettings, typeof(NorthstarOverlaySettings), 200, false) as NorthstarOverlaySettings;
            if (Target.northstarOverlaySettings == null)
                Label($"<i>{textError}Can not be null!{textColorEnd}</i>", false, false, true);
            EndRow();
            
            StartRow();
            Target.enableScreenOverlay = LeftCheck($"Enable Screen Overlay {symbolInfo}", "If false, screen overlay icons will only appear if " +
                "individual Tracked Targets override this value to " +
                "enable the icons for themselves only.", Target.enableScreenOverlay, 175);
            if (!Target.enableScreenOverlay)
                Label($"{textError}<i>Disabled</i>{textColorEnd}", 100, false, false, true);
            EndRow();
            StartRow();
            Target.enableEdgeOverlay = LeftCheck($"Enable Edge Overlay {symbolInfo}", "If false, screen overlay icons will only appear if " +
                "individual Tracked Targets override this value to " +
                "enable the icons for themselves only.", Target.enableEdgeOverlay, 175);
            if (!Target.enableEdgeOverlay)
                Label($"{textError}<i>Disabled</i>{textColorEnd}", 100, false, false, true);
            EndRow();
            StartRow();
            Target.enableNavigationBar = LeftCheck($"Enable Navigation Bar {symbolInfo}", "When true, and if the navigation bar is " +
                "a child of this object, icons will appear on the horizontal navigation bar.", Target.enableNavigationBar, 175);
            if (!Target.enableNavigationBar)
                Label($"{textError}<i>Disabled</i>{textColorEnd}", 100, false, false, true);
            else
            {
                Label($"{textNormal}Navigation Bar{textColorEnd}", 80, false, false, true);
                Target.navigationBar = Object(Target.navigationBar, typeof(NavigationBar), 95, true) as NavigationBar;
                if (Target.navigationBar == null)
                {
                    Label($"{textError}Required{textColorEnd}", 100, false, false, true);
                }
            }
            EndRow();
            
            if (!Target.enableScreenOverlay && !Target.enableEdgeOverlay)
            {
                Label(
                    $"{textError}<b>Both Screen and Edge Overlay are disabled. This will result in no overlay being " +
                    $"displayed. Individual Tracked Targets would need to override this value to enable screen or " +
                    $"edge overlays for themselves only.</b>{textColorEnd}", false, true, true);
            }
        }

        private void HeaderAndIntro()
        {
            LinkToDocs("https://infinitypbr.gitbook.io/infinity-pbr/northstar-tracking-system/northstar-documentation/northstar-screen-overlay");
            
            Header1($"Northstar Overlay");
            Label($"{textNormal}Any object with a <b>{textHightlight}TrackedTargetOverlay{textColorEnd}</b> component will be " +
                  $"handled by this UI panel. Set default options for display here. Each individual object can optionally " +
                  $"override the options as well.{textColorEnd}", false, true, true);
        }

        private void RuntimeData()
        {
            if (Target.screenOverlayIcons.Count == 0)
                return;
            
            GreyLine();
            Header2("Runtime Data");
            Label($"<i>{textWarning}Only shown at runtime{textColorEnd}</i>", false, false, true);
            
            StartRow();
            ShowRuntimeOverlayIcons = OnOffButton(ShowRuntimeOverlayIcons);
            Label($"Overlay Icons [{Target.screenOverlayIcons.Count}]");
            EndRow();
            RuntimeOverlayIcons();
            Space();
            
            StartRow();
            ShowRuntimeOverlayTypes = OnOffButton(ShowRuntimeOverlayTypes);
            Label($"Overlay Types [{Target.screenOverlayIconsByType.Count}]");
            EndRow();
            RuntimeOverlayTypes();
            Space();
            
        }

        private void RuntimeOverlayIcons()
        {
            if (!ShowRuntimeOverlayIcons)
                return;

            StartRow();
            Label("", 25);
            Label("Target Obj.", 150, true);
            Label("", 25);
            Label("Target Icon", 150, true);
            Label("Type", 100, true);
            Label($"Distance % {symbolInfo}", "1 = Further than max range\n0 = Closer to or at min range", 100, true);
            EndRow();
            foreach (var icon in Target.screenOverlayIcons)
            {
                StartRow();
                PingButton(icon.Key.gameObject);
                Label($"{textNormal}{icon.Key.name}{textColorEnd}", 150, false, false, true);
                PingButton(icon.Value.gameObject);
                Label($"{textNormal}{icon.Value.name}{textColorEnd}", 150, false, false, true);
                Label($"{textNormal}{icon.Key.targetType}{textColorEnd}", 100, false, false, true);
                Label($"{(icon.Value.ScreenDistancePercent >= 1 ? textNormal : textHightlight)}{icon.Value.ScreenDistancePercent}{textColorEnd}", 100, false, false, true);
                EndRow();
            }
        }
        
        private void RuntimeOverlayTypes()
        {
            if (!ShowRuntimeOverlayTypes)
                return;
            
            StartRow();
            Label($"Type String", 150, true);
            Label($"Icon Count", 150, true);
            Label($"# in Range {symbolInfo}", "The number of object within trackable distance.", 100, true);
            EndRow();
            foreach (var icon in Target.screenOverlayIconsByType)
            {
                StartRow();
                Label($"{textNormal}{icon.Key}{textColorEnd}", 150, false, false, true);
                Label($"{textHightlight}{icon.Value.Count} Icons{textColorEnd}", 150, false, false, true);
                var numberInRange = icon.Value.Count(x => x.Value.ScreenDistancePercent < 1);
                Label($"{(numberInRange == 0 ? textNormal : textHightlight)}{numberInRange}{textColorEnd}", 100, false, false, true);
                EndRow();
            }
        }
        
        /*private void ScreenOverlayOptions()
        {
            StartRow();
            Target.enableScreenOverlay = EnabledButton(Target.enableScreenOverlay);
            Header2($"Screen Overlay {symbolInfo}", "When enabled, icons will appear over the screen. Both " +
                                                    "options can be enabled at once; individual tracked objects can " +
                                                    "optionally utilize either, when they are enabled.", 250);
            EndRow();
            if (!Target.enableScreenOverlay)
            {
                Label($"{textWarning}Screen Overlay Disabled{textColorEnd}", false, false, true);
                return;
            }

            BlackLine();
            OverlayMainSettings();
            BlackLine();
            OverlaySizeSettings();
            BlackLine();
            OverlayOpacitySettings();
        }*/

        /*private void OverlayMainSettings()
        {
            StartRow();
            ShowOverlayMainSettings = OnOffButton(ShowOverlayMainSettings);
            Header2($"Overlay Sprite Settings {symbolInfo}", "This is the icon shown over a target when the camera " +
                                                             "is able to see the target (i.e. not the \"Edge\").", 250);
            EndRow();
            if (!ShowOverlayMainSettings)
                return;

            StartRow();
            Label($"Default Sprite {symbolInfo}", "This is the default sprite that will be used, unless the individual " +
                                                  "tracked items override it.", 150);
            Target.overlaySprite = Object(Target.overlaySprite, typeof(Sprite), 150, false) as Sprite;
            if (Target.overlaySprite == null)
                Label($"{textError}<b>Sprite must be populated!</b>{textColorEnd}", false, false, true);
            EndRow();
            
            StartRow();
            Label($"Default Color {symbolInfo}", "Alpha is handled by Opacity settings. Individual items can override this.", 150);
            Target.screenSpriteColor = ColorField(Target.screenSpriteColor, 150);
            EndRow();
            
            StartRow();
            Label($"Distance {symbolInfo}", "Outside of this range, the overlay will not be displayed. Use the size " +
                                                    "and opacity curves to slowly disappear icons.", 150);
            Label($"Min", 25);
            Target.screenDistanceMin = DelayedFloat(Target.screenDistanceMin, 50);
            Label($"Max", 25);
            Target.screenDistanceMax = DelayedFloat(Target.screenDistanceMax, 50);
            EndRow();

            ValidateOverlayMain();
        }*/
        
        /*private void OverlaySizeSettings()
        {
            StartRow();
            ShowOverlaySizeSettings = OnOffButton(ShowOverlaySizeSettings);
            Header2($"Size");
            if (Button("Copy Edge Values", 150))
            {
                Target.screenSpriteSize = Target.edgeSpriteSize;
                Target.overlaySpriteSizeUseCurve = Target.edgeSpriteSizeUseCurve;
                Target.overlaySpriteSizeCurve = Target.edgeSpriteSizeCurve;
            }
            EndRow();
            if (!ShowOverlaySizeSettings)
                return;
            
            StartRow();
            Label($"Size {symbolInfo}", "This is the size of the icon. Optionally use the curve to scale the " +
                                        "size based on the min/max distance settings.", 150);
            Target.screenSpriteSize = DelayedInt(Target.screenSpriteSize, 50);
            EndRow();

            if (Target.screenSpriteSize == 0)
            {
                Label($"{textWarning}Size is 0. Each individual icon will need to override this setting in order " +
                      $"to be drawn on screen.{textColorEnd}", false, true, true);
            }
            
            StartRow();
            Target.overlaySpriteSizeUseCurve = LeftCheck($"Use Size Curve {symbolInfo}",
                "When enabled, the size of the icon will be scaled based on the " +
                "min/max distance settings and the curve.", Target.overlaySpriteSizeUseCurve, 250);
            EndRow();

            if (Target.overlaySpriteSizeUseCurve)
            {
                StartRow();
                StartVertical(150);
                Label($"Size Curve {symbolInfo}",
                    "The left represents the minimum distance, closest to the player. The right is the " +
                    "farthest distance away. Y value 1 = full size, value 0 = 0 size, i.e. not visible.", 150);
                Label($"{textMuted}Closest distance on left{textColorEnd}", 150, false, false, true);
                EndVertical();
                Target.overlaySpriteSizeCurve = Curve(Target.overlaySpriteSizeCurve, -1, 200, 40);
                EndRow();
            }
            
            ValidationsOverlaySize();
        }*/

        /*private void OverlayOpacitySettings()
        {
            StartRow();
            ShowOverlayOpacitySettings = OnOffButton(ShowOverlayOpacitySettings);
            Header2($"Opacity");
            if (Button("Copy Edge Values", 150))
            {
                Target.screenSpriteOpacity = Target.edgeSpriteOpacity;
                Target.overlaySpriteOpacityUseCurve = Target.edgeSpriteOpacityUseCurve;
                Target.overlaySpriteOpacityCurve = Target.edgeSpriteOpacityCurve;
            }
            EndRow();
            if (!ShowOverlayOpacitySettings)
                return;
            
            StartRow();
            Label($"Opacity {symbolInfo}", "This is the default opacity of the icon. Optionally use the curve to scale the " +
                                        "opacity based on the min/max distance settings.", 150);
            Target.screenSpriteOpacity = DelayedFloat(Target.screenSpriteOpacity, 50);
            EndRow();
            
            if (Target.screenSpriteOpacity == 0)
            {
                Label($"{textWarning}Opacity is 0. Each individual icon will need to override this setting in order " +
                      $"to be drawn on screen.{textColorEnd}", false, true, true);
            }
            
            StartRow();
            Target.overlaySpriteOpacityUseCurve = LeftCheck($"Use Opacity Curve {symbolInfo}",
                "When enabled, the opacity of the icon will be scaled based on the " +
                "min/max distance settings and the curve.", Target.overlaySpriteOpacityUseCurve, 250);
            EndRow();

            if (Target.overlaySpriteOpacityUseCurve)
            {
                StartRow();
                StartVertical(150);
                Label($"Opacity Curve {symbolInfo}",
                    "The left represents the minimum distance, closest to the player. The right is the " +
                    "farthest distance away. Y value 1 = full opacity, value 0 = no opacity, i.e. not visible.", 150);
                Label($"{textMuted}Closest distance on left{textColorEnd}", 150, false, false, true);
                EndVertical();
                Target.overlaySpriteOpacityCurve = Curve(Target.overlaySpriteOpacityCurve, -1, 200, 40);
                EndRow();
            }
            
            ValidationsOverlayOpacity();
        }*/
        
        // Validations
        /*private void ValidationsOverlayOpacity()
        {
            if (Target.screenSpriteOpacity < 0 || Target.screenSpriteOpacity > 1)
            {
                Debug.Log($"Opacity must be between 0.01 and 1.");
                Target.screenSpriteOpacity = 1f;
            }
            
            if (Target.edgeSpriteOpacity < 0 || Target.edgeSpriteOpacity > 1)
            {
                Debug.Log($"Opacity must be between 0.01 and 1.");
                Target.edgeSpriteOpacity = 1f;
            }
        }
        
        private void ValidationsOverlaySize()
        {
            if (Target.screenSpriteSize < 0)
            {
                Debug.Log($"Size must be >= 0.");
                Target.screenSpriteSize = 0;
            }
            
            if (Target.edgeSpriteSize < 0)
            {
                Debug.Log($"Size must be >= 0.");
                Target.edgeSpriteSize = 0;
            }
        }
        
        private void ValidateOverlayMain()
        {
            if (Target.screenSpriteColor.a < 1f)
            {
                Target.screenSpriteColor.a = 1f;
                Debug.Log("Opacity handled by opacity settings.");  
            }
            if (Target.screenDistanceMin < 0)
            {
                Debug.Log("Min distance must be >= than 0.");
                Target.screenDistanceMin = 0f;
            }
            if (Target.screenDistanceMin >= Target.screenDistanceMax)
            {
                Debug.Log("Min distance must be < than Max distance.");
                Target.screenDistanceMax = Mathf.Max(1f, Target.screenDistanceMax);
                Target.screenDistanceMin = Target.screenDistanceMax - 1;
            }
        }*/
        
        // EDITOR PREF PROPERTIES
        /*public static bool ShowOverlayMainSettings
        {
            get => EditorPrefs.GetBool("Northstar Overlay Show Overlay Main Settings", true);
            set => EditorPrefs.SetBool("Northstar Overlay Show Overlay Main Settings", value);
        }
        
        public static bool ShowOverlaySizeSettings
        {
            get => EditorPrefs.GetBool("Northstar Overlay Show Overlay Size Settings", true);
            set => EditorPrefs.SetBool("Northstar Overlay Show Overlay Size Settings", value);
        }
        
        public static bool ShowOverlayOpacitySettings
        {
            get => EditorPrefs.GetBool("Northstar Overlay Show Overlay Opacity Settings", true);
            set => EditorPrefs.SetBool("Northstar Overlay Show Overlay Opacity Settings", value);
        }*/
        
        public static bool ShowRuntimeOverlayIcons
        {
            get => EditorPrefs.GetBool("Northstar Overlay Show Runtime Overlay Icons", true);
            set => EditorPrefs.SetBool("Northstar Overlay Show Runtime Overlay Icons", value);
        }
        
        public static bool ShowRuntimeOverlayTypes
        {
            get => EditorPrefs.GetBool("Northstar Overlay Show Runtime Overlay Types", true);
            set => EditorPrefs.SetBool("Northstar Overlay Show Runtime Overlay Types", value);
        }
        
        /*public static bool ShowRuntimeEdgeIcons
        {
            get => EditorPrefs.GetBool("Northstar Overlay Show Runtime Edge Icons", true);
            set => EditorPrefs.SetBool("Northstar Overlay Show Runtime Edge Icons", value);
        }
        
        public static bool ShowRuntimeEdgeTypes
        {
            get => EditorPrefs.GetBool("Northstar Overlay Show Runtime Edge Types", true);
            set => EditorPrefs.SetBool("Northstar Overlay Show Runtime Edge Types", value);
        }*/
    }

}
