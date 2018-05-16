﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButtonWrapper : MonoBehaviour {

	public GameObject dialogView;
	private PopulateVertical populateDialog;


	// Hole populateDialog-Skript
	void Start () {
		populateDialog = dialogView.GetComponentsInChildren<PopulateVertical> ()[0];
	}


	/// <summary>
	/// Gets the next dialog für den PC vom NPC.
	/// </summary>
	public void GetNextDialog(){
		GameObject playerObject = GameObject.FindGameObjectWithTag (DemoRPGMovement.PLAYER_NAME);
		PlayerDialogManager playerDialogManager = playerObject.GetComponent<PlayerDialogManager> ();
		List<string> dialogRows = playerDialogManager.GetNextDialogPackageFromNPC ();
		if (dialogRows!=null && dialogRows.Count > 0) {
			DisplayDialog(dialogRows, populateDialog);
		}
	}

	/// <summary>
	/// Displays a simple Dialog in Rows
	/// </summary>
	/// <param name="infos">Infos.</param>
	public static void DisplayDialog(List<string> dialogRows, PopulateVertical populateDialog){
		populateDialog.ClearDialogBox ();
		if (dialogRows.Count > 0) {
			foreach (var dialogRow in dialogRows) {
				populateDialog.addDialogText (dialogRow);

			}
		} 
	}

	/// <summary>
	/// Displays a Dialog Page 
	/// </summary>
	/// <param name="infos">Infos.</param>
	public static void DisplayDialogOption(List<string> dialogRows, PopulateVertical populateDialog){
		populateDialog.ClearDialogBox ();
		if (dialogRows.Count > 0) {
			foreach (var dialogRow in dialogRows) {
				populateDialog.addDialogText (dialogRow);

			}
		} 
	}

}
