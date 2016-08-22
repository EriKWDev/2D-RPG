 using UnityEngine;
 using UnityEditor;
 
 public class TexturePostProcessor : AssetPostprocessor
 {
     void OnPostprocessTexture(Texture2D texture)
     {
        TextureImporter importer = assetImporter as TextureImporter;
		importer.isReadable = true;
        importer.anisoLevel = 0;
		importer.filterMode = FilterMode.Point;
		importer.mipmapEnabled = false;
		importer.spritePixelsPerUnit = 16;
 
		Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Sprite));
         if (asset)
         {
             EditorUtility.SetDirty(asset);
         }
         else
         {
             texture.anisoLevel = 0;
			texture.filterMode = FilterMode.Point;          
         } 
     }
 }
     