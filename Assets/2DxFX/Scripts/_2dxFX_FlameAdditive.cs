//////////////////////////////////////////////
/// 2DxFX - 2D SPRITE FX - by VETASOFT 2018 //
//////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[AddComponentMenu("2DxFX/Standard/Flame Additive")]
[System.Serializable]
public class _2dxFX_FlameAdditive : MonoBehaviour
{
    [HideInInspector] public Material ForceMaterial;
    [HideInInspector] public bool ActiveChange = true;
    private string shader = "2DxFX/Standard/FlameAdditive";
    [HideInInspector] public Texture2D __MainTex2;
    [HideInInspector] [Range(0, 1)] public float _Alpha = 1f;
    [HideInInspector] [Range(-1f, 1f)] public float _Speed = .001f;
    [HideInInspector] [Range(0f, 2f)] public float _Intensity = 1f;

    [HideInInspector] public int ShaderChange = 0;
    Material tempMaterial;
    Material defaultMaterial;
    SpriteRenderer CanvasSpriteRenderer; [HideInInspector] public bool ActiveUpdate = true;
    Image CanvasImage;
   
    void Awake()
    {
        if (this.gameObject.GetComponent<Image>() != null) CanvasImage = this.gameObject.GetComponent<Image>();
        if (this.gameObject.GetComponent<SpriteRenderer>() != null) CanvasSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        __MainTex2 = Resources.Load("_2dxFX_FlameTXT") as Texture2D;
        ShaderChange = 0;
        if (CanvasSpriteRenderer != null)
        {
            CanvasSpriteRenderer.sharedMaterial.SetTexture("_MainTex2", __MainTex2);
        }
        else if (CanvasImage != null)
        {
            CanvasImage.material.SetTexture("_MainTex2", __MainTex2);
        }
        XUpdate();
    }

    public void CallUpdate()
    {
        XUpdate();
    }


    void Update()
    {
        if (ActiveUpdate) XUpdate();
    }

    void XUpdate()
    {

        if (CanvasImage == null)
        {
            if (this.gameObject.GetComponent<Image>() != null) CanvasImage = this.gameObject.GetComponent<Image>();
        }
        if (CanvasSpriteRenderer == null)
        {
            if (this.gameObject.GetComponent<SpriteRenderer>() != null) CanvasSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
       }

        if ((ShaderChange == 0) && (ForceMaterial != null))
        {
            ShaderChange = 1;
            if (tempMaterial != null) DestroyImmediate(tempMaterial);
            if (CanvasSpriteRenderer != null) CanvasSpriteRenderer.sharedMaterial = ForceMaterial;
            else if (CanvasImage != null) CanvasImage.material = ForceMaterial;
            ForceMaterial.hideFlags = HideFlags.None;
            ForceMaterial.shader = Shader.Find(shader);
            ActiveChange = false;

        }
        if ((ForceMaterial == null) && (ShaderChange == 1))
        {
            if (tempMaterial != null) DestroyImmediate(tempMaterial);
            tempMaterial = new Material(Shader.Find(shader));
            tempMaterial.hideFlags = HideFlags.None;
            if (CanvasSpriteRenderer != null)
            {
                CanvasSpriteRenderer.sharedMaterial = tempMaterial;
            }
            else if (CanvasImage != null)
            {
                CanvasImage.material = tempMaterial;
            }
            ShaderChange = 0;
        }

#if UNITY_EDITOR
		string dfname = "";
		if(CanvasSpriteRenderer != null) dfname= CanvasSpriteRenderer.sharedMaterial.shader.name;
		if(CanvasImage != null) 
		{
			Image img = CanvasImage;
			if (img.material==null)	dfname="Sprites/Default";
		}
		if (dfname == "Sprites/Default")
		{
			ForceMaterial.shader=Shader.Find(shader);
			ForceMaterial.hideFlags = HideFlags.None;
			__MainTex2 = Resources.Load ("_2dxFX_FlameTXT") as Texture2D;
			if(CanvasSpriteRenderer != null)
			{
                CanvasSpriteRenderer.sharedMaterial = ForceMaterial;
                CanvasSpriteRenderer.sharedMaterial.SetTexture ("_MainTex2", __MainTex2);
				}
			else if(CanvasImage != null)
			{
				Image img = CanvasImage;
				if (img.material==null)
				{
				CanvasImage.material = ForceMaterial;
				CanvasImage.material.SetTexture ("_MainTex2", __MainTex2);
				}
            }
		}
#endif
        if (ActiveChange)
        {
            if (CanvasSpriteRenderer != null)
            {
                CanvasSpriteRenderer.sharedMaterial.SetFloat("_Alpha", _Alpha);
                CanvasSpriteRenderer.sharedMaterial.SetFloat("_Speed", _Speed);
                CanvasSpriteRenderer.sharedMaterial.SetFloat("_Intensity", _Intensity);
            }
            else if (CanvasImage != null)
            {
                CanvasImage.material.SetFloat("_Alpha", _Alpha);
                CanvasImage.material.SetFloat("_Intensity", _Intensity);
                CanvasImage.material.SetFloat("_Speed", _Speed);
            }

        }

    }

    void OnDestroy()
    {
        /*
	    if (CanvasImage != null) 
		{
			if (CanvasImage==null) CanvasImage = this.gameObject.GetComponent<Image> ();
		}
        */

        if ((Application.isPlaying == false) && (Application.isEditor == true))
        {

            if (tempMaterial != null) DestroyImmediate(tempMaterial);

            if (gameObject.activeSelf && defaultMaterial != null)
            {
                if (CanvasSpriteRenderer != null)
                {
                    CanvasSpriteRenderer.sharedMaterial = defaultMaterial;
                    CanvasSpriteRenderer.sharedMaterial.hideFlags = HideFlags.None;
                }
                else if (CanvasImage != null)
                {
                    CanvasImage.material = defaultMaterial;
                    CanvasImage.material.hideFlags = HideFlags.None;
                }
            }
        }
    }
    void OnDisable()
    {
        /*
	        if (CanvasImage () != null) 
		    {
		     	if (CanvasImage==null) CanvasImage = this.gameObject.GetComponent<Image> ();
	    	} 
        */
        if (gameObject.activeSelf && defaultMaterial != null)
        {
            if (CanvasSpriteRenderer != null)
            {
                CanvasSpriteRenderer.sharedMaterial = defaultMaterial;
                CanvasSpriteRenderer.sharedMaterial.hideFlags = HideFlags.None;
            }
            else if (CanvasImage != null)
            {
                CanvasImage.material = defaultMaterial;
                CanvasImage.material.hideFlags = HideFlags.None;
            }
        }
    }

    void OnEnable()
    {
        /*
		if (CanvasImage != null) 
		{
			if (CanvasImage==null) CanvasImage = this.gameObject.GetComponent<Image> ();
		} 
        */

        if (defaultMaterial == null)
        {
            defaultMaterial = new Material(Shader.Find("Sprites/Default"));
        }

        if (ForceMaterial == null)
        {
            ActiveChange = true;
            tempMaterial = new Material(Shader.Find(shader));
            tempMaterial.hideFlags = HideFlags.None;
            if (CanvasSpriteRenderer != null)
            {
                CanvasSpriteRenderer.sharedMaterial = tempMaterial;
            }
            else if (CanvasImage != null)
            {
                CanvasImage.material = tempMaterial;
            }
            __MainTex2 = Resources.Load("_2dxFX_FlameTXT") as Texture2D;
        }
        else
        {
            ForceMaterial.shader = Shader.Find(shader);
            ForceMaterial.hideFlags = HideFlags.None;
            if (CanvasSpriteRenderer != null)
            {
                CanvasSpriteRenderer.sharedMaterial = ForceMaterial;
            }
            else if (CanvasImage != null)
            {
                CanvasImage.material = ForceMaterial;
            }
            __MainTex2 = Resources.Load("_2dxFX_FlameTXT") as Texture2D;
        }

        if (__MainTex2)
        {
            __MainTex2.wrapMode = TextureWrapMode.Repeat;
            if (CanvasSpriteRenderer != null)
            {
                CanvasSpriteRenderer.sharedMaterial.SetTexture("_MainTex2", __MainTex2);
            }
            else if (CanvasImage != null)
            {
                CanvasImage.material.SetTexture("_MainTex2", __MainTex2);
            }

        }

    }
}




#if UNITY_EDITOR
[CustomEditor(typeof(_2dxFX_FlameAdditive)),CanEditMultipleObjects]
public class _2dxFX_FlameAdditive_Editor : Editor
{
   
    private SerializedObject m_object;
	
	public void OnEnable()
	{
		
		m_object = new SerializedObject(targets);
	}
	
	public override void OnInspectorGUI()
	{
		m_object.Update();
		DrawDefaultInspector();
		
		_2dxFX_FlameAdditive _2dxScript = (_2dxFX_FlameAdditive)target;
	
		Texture2D icon = Resources.Load ("2dxfxinspector") as Texture2D;
		if (icon)
		{
			Rect r;
			float ih=icon.height;
			float iw=icon.width;
			float result=ih/iw;
			float w=Screen.width;
			result=result*w;
			r = GUILayoutUtility.GetRect(ih, result);
			EditorGUI.DrawTextureTransparent(r,icon);
		}
        EditorGUILayout.PropertyField(m_object.FindProperty("ActiveUpdate"), new GUIContent("Active Update", "Active Update, for animation / Animator only"));

        EditorGUILayout.PropertyField(m_object.FindProperty("ForceMaterial"), new GUIContent("Shared Material", "Use a unique material, reduce drastically the use of draw call"));
		
		if (_2dxScript.ForceMaterial == null)
		{
			_2dxScript.ActiveChange = true;
		}
		else
		{
			if(GUILayout.Button("Remove Shared Material"))
			{
				_2dxScript.ForceMaterial= null;
				_2dxScript.ShaderChange = 1;
				_2dxScript.ActiveChange = true;
				_2dxScript.CallUpdate();
			}
		
			EditorGUILayout.PropertyField (m_object.FindProperty ("ActiveChange"), new GUIContent ("Change Material Property", "Change The Material Property"));
		}

		if (_2dxScript.ActiveChange)
		{

			EditorGUILayout.BeginVertical("Box");

			Texture2D icone = Resources.Load ("2dxfx-icon-time") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_Speed"), new GUIContent("Speed", icone, "Change the speed of the flame"));
             icone = Resources.Load ("2dxfx-icon-value") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_Intensity"), new GUIContent("_Intensity", icone, "Change the speed of the flame"));


			EditorGUILayout.BeginVertical("Box");


			icone = Resources.Load ("2dxfx-icon-fade") as Texture2D;
			EditorGUILayout.PropertyField(m_object.FindProperty("_Alpha"), new GUIContent("Fading", icone, "Fade from nothing to showing"));

			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
	

		}
		
		m_object.ApplyModifiedProperties();
		
	}
}
#endif
