using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class ExportManager : EditorWindow
{
    private string trigramme = "AAA";
    private string path = "Select folder";
    private string miniGameName = "MiniGame name";
    private int miniGameIndex = 1;
    private MiniGameScriptableObject miniGameSO;
    private readonly string[] ignoreNames = {"Resources", "Scripts"};
    
    private bool canExport;

    //List<String> foldersPath = new List<string>();

    enum NameType
    {
        SCENE,
        MAINFOLDER,
        ASSETS,
        SCRIPTABLEOBJECT
    }


    [MenuItem("Assets/Export Micro Game", false, 22)]
    private static void ExportationWindow()
    {
        EditorWindow.GetWindow(typeof(ExportManager));
        Debug.Log(("Exportation"));
    }
    
    //Fonction de validation : Conditions pour activer/désactiver le bouton
    [MenuItem("Assets/Export MiniGame", true)]
    private static bool ExportationWindowValidation()
    {
        return Selection.activeObject != null;
    }

    private bool VerifyNames(string itemName, NameType nt)
    {
        switch (nt)
        {
            case NameType.ASSETS :
                if (itemName.Length >= 5 &&
                    itemName.Substring(0, 5) == trigramme + miniGameIndex + "_") return true;
                break;
            
            case NameType.SCENE :

                if ((itemName.Length == 13 + miniGameName.Length || itemName.Length == 7 + miniGameName.Length) &&
                    itemName.Substring(0, 3) == "MG_" &&
                    itemName.Substring(3, 4) == trigramme + "_" &&
                    ((itemName.Substring(7, 6) == "Scene_" &&
                      itemName.Substring(13, miniGameName.Length) == miniGameName) || 
                     itemName.Substring(7, miniGameName.Length) == miniGameName)) return true;
                break;
            
            case NameType.MAINFOLDER :
                
                if (itemName.Length == 7 + miniGameName.Length &&
                    itemName.Substring(0, 3) == "MG_" &&
                    itemName.Substring(3, 4) == trigramme + "_" &&
                    itemName.Substring(7, miniGameName.Length) == miniGameName) return true;
                break;
            
            case NameType.SCRIPTABLEOBJECT :
                if (itemName.Length == 7 &&
                    itemName.Substring(0, 7) == trigramme + miniGameIndex + "_SO") return true;
                break;
        }
        return false;
    }
    
    

    private void OnGUI()
    {
        GUILayout.Label(("Exportation de Mini-jeux"), EditorStyles.boldLabel);
        
        EditorGUILayout.Space();
        //renseigner le trigramme
        GUILayout.Label("Trigramme");
        trigramme = EditorGUILayout.TextField("", trigramme);
        
        //Renseigner le nom du Mini jeu
        GUILayout.Label("MiniGame name");
        miniGameName = EditorGUILayout.TextField("", miniGameName);
        
        //Renseigner l'index du Mini jeu
        GUILayout.Label("MiniGame index");
        miniGameIndex = EditorGUILayout.IntField("", miniGameIndex);

        GUILayout.Label("Scriptable Object");
        miniGameSO = EditorGUILayout.ObjectField("", miniGameSO, typeof(MiniGameScriptableObject), false, null) as MiniGameScriptableObject;
        
        //Renseigner le path
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        GUILayout.Label(("Mini-Game Folder Path"));

        
        path = EditorGUILayout.TextField("", path);
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Update Folder Path"))
        {
            UpdatePathField();
        }

        if (GUILayout.Button("Check names"))
        {
            CheckExportConditions();
        }
        
        EditorGUILayout.Space();

        if (canExport && GUILayout.Button("Export"))
        {
            ExportMiniGame();
        }
        
    }

    private void UpdatePathField()
    {
        if (Selection.activeObject != null)
        {
            path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
        }
    }


    private void CheckExportConditions()
    {
        //référencer tous les éléments des dossiers
        string[] pathArray = {path};
        string[] scriptsPaths = {path + "/Scripts"};
        var allPathGUIDs = AssetDatabase.FindAssets("", scriptsPaths).ToList();
        var allAssetsNames = new List<string>();

        //Détection des scenes et des scriptables objects
        
        var scenePaths = AssetDatabase.FindAssets("t:scene", pathArray).ToList();
        var soPaths = AssetDatabase.FindAssets("t:MiniGameScriptableObject", pathArray).ToList();

        if (!CheckObjectFolder(scenePaths, "scène") || !CheckObjectFolder(soPaths, "scriptable object"))
        {
            canExport = false;
            return;
        }

        if (miniGameSO.MiniGameScene == null || miniGameSO.MiniGameInput == null ||
            miniGameSO.MiniGameInput.Length == 0 || miniGameSO.MiniGameKeyword == "")
        {
            Debug.LogError("Le scriptable object n'a pas été rempli");
            canExport = false;
            return;
        }

        //Tri des différents assets
        for (int i = 0; i < allPathGUIDs.Count; i++)
        {
            string currentAssetName = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(allPathGUIDs[i]));

            foreach (var t in ignoreNames)
            {
                if (t != currentAssetName) continue;
                
                //Debug.Log("remove asset from list");
                allPathGUIDs.RemoveAt(i);
                i--;
            }
        }

        //Récupération des noms
        
        string sceneName = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(scenePaths[0]));
        string soName = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(soPaths[0]));
        string folderName = Path.GetFileName(path);
        
        foreach (var UIDs in allPathGUIDs)
        {
            string fileName = Path.GetFileName(AssetDatabase.GUIDToAssetPath(UIDs));

            if (fileName.Substring(fileName.Length - 2, 2) == "cs")
            {
                allAssetsNames.Add(Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(UIDs)));
            }
        }
        
        //Vérification des noms
        if (!VerifyNames(folderName, NameType.MAINFOLDER))
        {
            Debug.LogError( $"Le dossier racine {folderName} est mal nommé. \nLe nom doit être : MG_{trigramme}_{miniGameName}");
            canExport = false;
            return;
        }

        if (!VerifyNames(sceneName, NameType.SCENE))
        {
            Debug.LogError($"La scène {sceneName} est mal nommé. \nLe nom doit être : MG_{trigramme}_Scene_{miniGameName}");
            canExport = false;
            return;
        }
        
        if (!VerifyNames(soName, NameType.SCRIPTABLEOBJECT))
        {
            Debug.LogError($"Le scriptable object {soName} est mal nommé. \nLe nom doit être : {trigramme}{miniGameIndex}_SO");
            canExport = false;
            return;
        }

        foreach (var assetName in allAssetsNames.Where(assetName => !VerifyNames(assetName, NameType.ASSETS)))
        {
            Debug.LogError(assetName + $" est mal nommé. \nLe nom doit commencer par : {trigramme}{miniGameIndex}_");
            canExport = false;
            return;
        }

        Debug.Log("Tous les noms sont à jour, l'export est possible !");
        canExport = true;
    }

    private bool CheckObjectFolder(IList<string> objGUIDs, string objName)
    {
        for (var i = objGUIDs.Count - 1; i >= 0; i--)
        {
            if (Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(objGUIDs[i])) != path.Replace('/', Path.DirectorySeparatorChar))
            {
                objGUIDs.RemoveAt(i);
            }
        }

        if (objGUIDs.Count == 0)
        {
            Debug.LogError($"Aucun(e) {objName} à la racine du dossier !");
            return false;
        }
        if (objGUIDs.Count > 1)
        {
            Debug.LogError($"Plusieurs {objName}s à la racine du dossier !");
            return false;
        }
        return true;
    }

    private void ExportMiniGame()
    {
        //if(!Directory.Exists("MG_Packages")) Directory.CreateDirectory("MG_Packages");
        string exportPath = EditorUtility.OpenFolderPanel("Select export location", "", "");
        
        if(exportPath.Length != 0) AssetDatabase.ExportPackage(path, exportPath + "/" + trigramme + "_" +miniGameName + "_MGpackage.unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.Default);
    }
    
}
