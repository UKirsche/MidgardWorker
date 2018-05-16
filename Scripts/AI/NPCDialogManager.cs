﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogManager : ArtifactDialogManager {

	private NPC npcDialogs;
	private DialogParser dialogParser;
	private StandardNPCInfos standardInfos;
	private bool wasInformand;

	// Use this for initialization
	public override void Start () {
		artifactName = gameObject.name;
		LoadDialog ();
		InitializeDialogParser();
		standardInfos = new StandardNPCInfos (artifactName);
		wasInformand = HasMissionPakets() || HasInfoPakets();
		
	}

	/// <summary>
	/// Überschreibt die Methode
	/// </summary>
	protected override void LoadDialog(){
		GameObject scripts = GameObject.Find ("Scripts");
		DialogLoader dialogLoader = scripts.GetComponent<DialogLoader> ();
		npcDialogs = dialogLoader.GetDialog<NPC> (artifactName) as NPC;
	}


	/// <summary>
	/// Initializes the dialog parser with the first Mission. There is always min 1 Mission
	/// </summary>
	private void InitializeDialogParser(){
		dialogParser = new DialogParser ();
		dialogParser.StartNode = new DialogNode<object> ();
		Mission mission = npcDialogs.missionen [0];
		dialogParser.StartNode.nodeElement = mission;
		dialogParser.StartNode.typeNodeElement = typeof(Mission);
		dialogParser.StartNode.typeParentNodeElement = null;
		dialogParser.StartNode.parentNode = null;
	}

	protected bool HasMissionPakets(){
		bool retVal = (npcDialogs != null && npcDialogs.missionen.Count > 0) ? true : false;
		return retVal;
	}


	/// <summary>
	/// Gets the next info package from Dialogpack for NPC and removes it from the list
	/// </summary>
	public List<string> GetNextDialog(){
		List<string> returnList = null;
		bool isOption = dialogParser.IsOption;
		returnList = GetNextInfos ();
		if (isOption) {
			returnList = FormatOptions ();
		}
		return returnList;
	}

	/// <summary>
	/// Ermittelt, ob der nächste Dialog ein Optionsdialog ist
	/// </summary>
	/// <returns><c>true</c>, if dialog option was nexted, <c>false</c> otherwise.</returns>
	public bool NextDialogOption(){
		return dialogParser.IsOption;
	}

	#region private hilfsmethoden
	/// <summary>
	/// Gets the next infos: 
	/// Liefert die nächsten NPC-Infos als Liste von strings
	/// </summary>
	/// <returns>The next infos.</returns>
	private List<string> GetNextInfos(){
		List<string> infoStrings = new List<string> ();
		List<Info> infos = dialogParser.GetInfos ();
		foreach (var info in infos) {
			infoStrings.Add (info.content);
		}

		return infoStrings;
	}


	/// <summary>
	/// Get Standard Info for a Character
	/// </summary>
	public List<string> GetStandardInfo(){
		if (HasMissionPakets ()) {
			return standardInfos.StandardInfoTalker;
		} else {
			if (wasInformand) {
				return standardInfos.StandardInfoFinish;
			}
			return standardInfos.StandardInfoName;
		}
	}


	/// <summary>
	/// Liefert die Optionen (Auswahlmöglichkeiten) als Liste von strings
	/// </summary>
	/// <returns>The next options.</returns>
	private List<string> FormatOptions(){
		List<string> optionStrings = new List<string> ();
		List<DialogNode<object>> optionNodes = dialogParser.optionalStartNodes;
		foreach (var optionNode in optionNodes) {
			Option nodeElement = optionNode.nodeElement as Option;
			optionStrings.Add (nodeElement.Beschreibung);
		}

		return optionStrings;
	}

	#endregion

}
