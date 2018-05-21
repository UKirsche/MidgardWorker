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
		bool lastWasOption = DialogTypeChecker.LastWasOption (populateDialog as PopulateVerticalToggle);
		if (lastWasOption) {
			PopulateVerticalToggle populateToggle = populateDialog as PopulateVerticalToggle;
			int seletedIndex = populateToggle.GetSelectedToggleID ();
			playerDialogManager.SetChosenOptionIndex (seletedIndex);
		}
		bool isOptionDialog = DialogTypeChecker.NextIsOption (playerDialogManager);

		List<string> dialogRows = playerDialogManager.GetNextDialogPackageFromNPC ();
		if (dialogRows!=null && dialogRows.Count > 0) {
			if (isOptionDialog) {
				DialogDisplayManager.DisplayDialogOption (dialogRows, populateDialog);
			} else {
				DialogDisplayManager.DisplayDialogText(dialogRows, populateDialog);
			}
		}
	}

}
