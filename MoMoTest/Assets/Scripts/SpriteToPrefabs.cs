using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class SpriteToPrefabs : MonoBehaviour
{
   
    static private string OpenPath()//选择路径
    {
        string path=null;
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        dialog.Description = "选择转换的文件";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            if (dialog.SelectedPath == "")
            {
                print("你没有选择任何内容");
            }
            else
            {
                DirectoryInfo wenjianjia = new DirectoryInfo(dialog.SelectedPath);
                 path = dialog.SelectedPath.Replace("\\", "/");
                              
            }
        }      
        return path;
    }
    static private string SavePath()
    {
        string path = null;
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        dialog.Description = "文件保存的目录";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            if (dialog.SelectedPath == "")
            {
                print("你没有选择任何内容");
            }
            else
            {
                DirectoryInfo wenjianjia = new DirectoryInfo(dialog.SelectedPath);
                path = dialog.SelectedPath.Replace("\\", "/");

            }
        }
        return path;
    }

    [UnityEditor.MenuItem("MyMenu/AtlasMaker")]//转换格式
    static private void MakeAtlas()
    {             
        DirectoryInfo rootDirInfo = new DirectoryInfo(OpenPath());
        string spriteDir = SavePath();

        if (!Directory.Exists(spriteDir))
        {
            Directory.CreateDirectory(spriteDir);
        }
        foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
        {
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
            {
                string allPath = pngFile.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                //Sprite sprite = Resources.LoadAssetAtPath<Sprite>(assetPath);
                Sprite sprite=AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                GameObject go = new GameObject(sprite.name);
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                allPath = spriteDir + "/" + sprite.name + ".prefab";
                string prefabPath = allPath.Substring(allPath.IndexOf("Assets"));
                PrefabUtility.CreatePrefab(prefabPath, go);
                GameObject.DestroyImmediate(go);
            }
        }
        Debug.Log("done");
    }
}
