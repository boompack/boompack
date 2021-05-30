using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.Build.Reporting;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEditor.U2D;



public class EditorBuildSettings
{

    public static GameObject[] panels;
    public GameObject[] gameViews;


    [MenuItem("Build/Build Level Editor")]
    private static void BuildLevelEditor()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/LevelEditor.unity", "Assets/Scenes/LEGameScene.unity", "Assets/Scenes/LevelOrder.unity" };
        buildPlayerOptions.locationPathName = "Level Editor Build";

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    [MenuItem("Build/Build BoomPack")]
    private static void BuildBoomPack()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/GameScene.unity" };
        buildPlayerOptions.locationPathName = "Boom Pack Build";

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }

    }

    [MenuItem("Build/Create Level Buttons")]
    private static void CreateLevelButtons()
    {
        for (int x = 1; x <= 300 ; x++)
        {
            Object originalPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/Game Level UIButton.prefab", typeof(GameObject));
            GameObject objSource = PrefabUtility.InstantiatePrefab(originalPrefab) as GameObject;
            GameLevelUIButton kod = objSource.GetComponent<GameLevelUIButton>();
            kod.levelID = x;
            kod.levelText.text = x.ToString();
            kod.levelScreenshot = (Sprite)AssetDatabase.LoadAllAssetsAtPath($"Assets/Graphics/LevelMiniMaps/Screenshot_{x}.png")[1];
            kod.levelScreenshotBlur = (Sprite)AssetDatabase.LoadAllAssetsAtPath($"Assets/Graphics/LevelMiniMapsBlur/ScreenshotBlur_{x}.png")[1];
            kod.levelImage.sprite = kod.levelScreenshot;
            kod.background.color = StateColorManager.Instance.GetStateBackgroundColor(x);
            kod.border.color = StateColorManager.Instance.GetStateBorderColor(x);
            //objSource.GetComponentInChildren<Text>().text = (x).ToString();
            objSource.name = objSource.name + "Level " + x.ToString();

            int state = ((x - 1)/20) + 1;
            objSource.transform.SetParent(GameObject.Find($"Pack {state} Levels").transform);
        }

    }
}

public class SpriteAtlasPaddingOverride

{
    [MenuItem("Assets/SpriteAtlas Set Padding 32")]

    public static void SpriteAtlasCustomPadding()

    {

        Object[] objs = Selection.objects;

        foreach (var obj in objs)

        {

            SpriteAtlas sa = obj as SpriteAtlas;

            if (sa)

            {

                var ps = sa.GetPackingSettings();

                ps.padding = 32;

                sa.SetPackingSettings(ps);

            }

        }

        AssetDatabase.SaveAssets();

    }

}

