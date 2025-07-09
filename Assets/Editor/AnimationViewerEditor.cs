using UnityEditor;
using UnityEngine;

// Custom Inspector拡張
[CustomEditor(typeof(AnimationViewer))]
public class AnimationViewerEditor : Editor
{
    private AnimationViewer animViewer;
    private bool showAnimationList = true;
    private bool showPreviewControls = true;
    private double lastTime;
    private bool isPlaying = false;

    // プレビュー用の変数
    private float previewTime = 0f;
    private int selectedClipIndex = 0;

    void OnEnable()
    {
        animViewer = (AnimationViewer)target;
        lastTime = EditorApplication.timeSinceStartup;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // ヘッダー
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("アニメーションビューワー", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // 基本設定
        DrawBasicSettings();

        // アニメーションリスト
        DrawAnimationList();

        // プレビューコントロール
        DrawPreviewControls();

        // 実行時コントロール
        DrawRuntimeControls();

        serializedObject.ApplyModifiedProperties();

        // プレビューモードの更新
        if (animViewer.showPreview && !Application.isPlaying)
        {
            UpdatePreview();
        }
    }

    void DrawBasicSettings()
    {
        EditorGUILayout.LabelField("基本設定", EditorStyles.boldLabel);

        animViewer.playSpeed = EditorGUILayout.FloatField("再生速度", animViewer.playSpeed);
        animViewer.loop = EditorGUILayout.Toggle("ループ", animViewer.loop);

        EditorGUILayout.Space();
    }

    void DrawAnimationList()
    {
        showAnimationList = EditorGUILayout.Foldout(showAnimationList, "アニメーションクリップ");

        if (showAnimationList)
        {
            EditorGUI.indentLevel++;

            SerializedProperty clipsProperty = serializedObject.FindProperty("animationClips");
            EditorGUILayout.PropertyField(clipsProperty, true);

            if (animViewer.animationClips != null)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField($"登録されているクリップ数: {animViewer.animationClips.Length}");

                // 各クリップの情報表示
                for (int i = 0; i < animViewer.animationClips.Length; i++)
                {
                    if (animViewer.animationClips[i] != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField($"  {i}: {animViewer.animationClips[i].name}");
                        EditorGUILayout.LabelField($"({animViewer.animationClips[i].length:F2}s)", GUILayout.Width(60));
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();
    }

    void DrawPreviewControls()
    {
        showPreviewControls = EditorGUILayout.Foldout(showPreviewControls, "プレビューコントロール");

        if (showPreviewControls)
        {
            EditorGUI.indentLevel++;

            animViewer.showPreview = EditorGUILayout.Toggle("プレビュー表示", animViewer.showPreview);

            if (animViewer.showPreview && animViewer.animationClips != null && animViewer.animationClips.Length > 0)
            {
                // クリップ選択
                string[] clipNames = new string[animViewer.animationClips.Length];
                for (int i = 0; i < animViewer.animationClips.Length; i++)
                {
                    clipNames[i] = animViewer.animationClips[i] != null ?
                        animViewer.animationClips[i].name : $"Empty {i}";
                }

                selectedClipIndex = EditorGUILayout.Popup("プレビュークリップ", selectedClipIndex, clipNames);

                if (selectedClipIndex < animViewer.animationClips.Length &&
                    animViewer.animationClips[selectedClipIndex] != null)
                {
                    AnimationClip selectedClip = animViewer.animationClips[selectedClipIndex];

                    // タイムスライダー
                    float maxTime = selectedClip.length;
                    previewTime = EditorGUILayout.Slider("時間", previewTime, 0f, maxTime);

                    // 再生コントロール
                    EditorGUILayout.BeginHorizontal();

                    if (GUILayout.Button("▶", GUILayout.Width(30)))
                    {
                        isPlaying = !isPlaying;
                        if (isPlaying)
                        {
                            EditorApplication.update += UpdatePreviewTime;
                        }
                        else
                        {
                            EditorApplication.update -= UpdatePreviewTime;
                        }
                    }

                    if (GUILayout.Button("⏹", GUILayout.Width(30)))
                    {
                        isPlaying = false;
                        previewTime = 0f;
                        EditorApplication.update -= UpdatePreviewTime;
                    }

                    if (GUILayout.Button("⏪", GUILayout.Width(30)))
                    {
                        previewTime = 0f;
                    }

                    if (GUILayout.Button("⏩", GUILayout.Width(30)))
                    {
                        previewTime = maxTime;
                    }

                    EditorGUILayout.EndHorizontal();

                    // 現在時間の情報
                    EditorGUILayout.LabelField($"現在時間: {previewTime:F2}s / {maxTime:F2}s");
                }
            }

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();
    }

    void DrawRuntimeControls()
    {
        EditorGUILayout.LabelField("実行時コントロール", EditorStyles.boldLabel);

        if (Application.isPlaying)
        {
            EditorGUILayout.BeginHorizontal();

            if (animViewer.animationClips != null)
            {
                for (int i = 0; i < animViewer.animationClips.Length; i++)
                {
                    if (animViewer.animationClips[i] != null)
                    {
                        if (GUILayout.Button($"再生 {i}"))
                        {
                            animViewer.PlayAnimation(i);
                        }
                    }
                }
            }

            if (GUILayout.Button("停止"))
            {
                animViewer.StopAnimation();
            }

            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.HelpBox("実行時のみ使用可能", MessageType.Info);
        }
    }

    void UpdatePreview()
    {
        if (selectedClipIndex < animViewer.animationClips.Length &&
            animViewer.animationClips[selectedClipIndex] != null)
        {
            AnimationClip clip = animViewer.animationClips[selectedClipIndex];

            // アニメーションをサンプリング
            if (clip != null)
            {
                clip.SampleAnimation(animViewer.gameObject, previewTime);
                SceneView.RepaintAll();
            }
        }
    }

    void UpdatePreviewTime()
    {
        if (isPlaying && selectedClipIndex < animViewer.animationClips.Length &&
            animViewer.animationClips[selectedClipIndex] != null)
        {
            double currentTime = EditorApplication.timeSinceStartup;
            float deltaTime = (float)(currentTime - lastTime);
            lastTime = currentTime;

            AnimationClip clip = animViewer.animationClips[selectedClipIndex];
            previewTime += deltaTime * animViewer.playSpeed;

            if (previewTime >= clip.length)
            {
                if (animViewer.loop)
                {
                    previewTime = 0f;
                }
                else
                {
                    previewTime = clip.length;
                    isPlaying = false;
                    EditorApplication.update -= UpdatePreviewTime;
                }
            }

            Repaint();
        }
    }

    void OnDisable()
    {
        EditorApplication.update -= UpdatePreviewTime;
    }
}