using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetImporter : EditorWindow {
	
	public static AssetImporter instance { get; private set; }
	
	[MenuItem ("3Dception/Import")]
	static void Init () {
		// Get existing open window or if none, make a new one:		
		instance = (AssetImporter) EditorWindow.GetWindow (typeof(AssetImporter));
	}
	
	void OnGUI ()
	{	
		if (GUILayout.Button ("Import")) 
		{
			import ();
		}
	}
	
	static void import () 
	{
		string[] args = System.Environment.GetCommandLineArgs ();
		string TYPE = args [7];
		string VERSION = args [8];
		string UNITY = args [9];
		AssetDatabase.ImportPackage("../../3Dception_" + VERSION + "_Win_" + TYPE + "_" + UNITY + ".unitypackage", false);
		AssetDatabase.ImportPackage("../../3Dception_" + VERSION + "_OSX_" + TYPE + "_" + UNITY + ".unitypackage", false);
	}
	
}
